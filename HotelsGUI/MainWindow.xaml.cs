using HotelsLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Navigation;

namespace HotelsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<SearchResult> Results { get; set; }
        public ObservableCollection<SavedPreference> Preferences { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Results = new ObservableCollection<SearchResult>();
            ResultListView.ItemsSource = Results;

            Preferences = new ObservableCollection<SavedPreference>(PreferencesRepository.PreferencesRepositoryInstance.GetAll());
            AddInitialInput();
            PreferencesCombobox.ItemsSource = Preferences;
        }

        private void AddInitialInput()
        {
            CityTextbox.Text = "Warszawa";
            DateFrom.SelectedDate = DateTime.Today;
            DateTo.SelectedDate = DateTime.Today.AddDays(1);
            AdultsNumberCombobox.SelectedIndex = 1;
            DelayCombobox.SelectedIndex = 1;
        }

        private bool InputIsValid(out string message)
        {
            if (!DateFrom.SelectedDate.HasValue)
            {
                message = "Select value of DateFrom";
                return false;
            }

            if (!DateTo.SelectedDate.HasValue)
            {
                message = "Select value of DateTo";
                return false;
            }

            if (DateFrom.SelectedDate.Value.CompareTo(DateTo.SelectedDate.Value) >= 0)
            {
                message = "Date From cannot be greater than Date To";
                return false;
            }

            if (DateFrom.SelectedDate.Value.CompareTo(DateTime.Today) < 0)
            {
                message = "Date From cannot be before today";
                return false;
            }

            message = "";
            return true;
        }

        private void FillInputs(SavedPreference savedPreference)
        {
            CityTextbox.Text = savedPreference.City;
            DateFrom.SelectedDate = savedPreference.DateFrom;
            DateTo.SelectedDate = savedPreference.DateTo;
            AdultsNumberCombobox.SelectedIndex = savedPreference.NumberOfAdults - 1;
            DelayCombobox.SelectedIndex = GetDelayComboIndexFromSeconds(savedPreference.Delay);
            PreferenceNameTextbox.Text = savedPreference.PreferenceName;
        }

        private int GetDelayComboIndexFromSeconds(int delay) => delay / 3 - 1;
        private int GetDelaySecondsFromComboIndex(int index) => (index + 1 )* 3;

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputIsValid(out string message))
            {
                OutputTextBlock.Text = message;
                return;
            }

            ResultService.ResultServiceInstance.CleanResultsFolder();
            SearchService.SearchServiceInstance.CleanServiceFolder();

            OutputTextBlock.Text = string.Empty;

            UserPreference userPreference = new UserPreference()
            {
                City = CityTextbox.Text,
                DateTo = DateTo.SelectedDate.Value,
                DateFrom = DateFrom.SelectedDate.Value,
                NumberOfAdults = AdultsNumberCombobox.SelectedIndex + 1,
                Delay = (DelayCombobox.SelectedIndex + 1) * 3
            };

            SearchService.SearchServiceInstance.Search(userPreference);

            await ResultService.ResultServiceInstance.WaitForResult(new FileSystemEventHandler(OnResultFileCreated));
            OutputTextBlock.Text = "Waiting for results";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PreferenceNameTextbox.Text))
            {
                OutputTextBlock.Text = "Select name for your preference";
                return;
            }
            if (!InputIsValid(out string message))
            {
                OutputTextBlock.Text = message;
                return;
            }

            SavedPreference pref = new SavedPreference
            {
                PreferenceName = PreferenceNameTextbox.Text,
                City = CityTextbox.Text,
                DateTo = DateTo.SelectedDate.Value,
                DateFrom = DateFrom.SelectedDate.Value,
                NumberOfAdults = AdultsNumberCombobox.SelectedIndex + 1,
                Delay = GetDelaySecondsFromComboIndex(DelayCombobox.SelectedIndex)
            };

            PreferencesRepository.PreferencesRepositoryInstance.Add(pref);
            OutputTextBlock.Text = string.Empty;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PreferencesRepository.PreferencesRepositoryInstance.Delete(PreferencesCombobox.SelectedItem as SavedPreference))
            {
                OutputTextBlock.Text = "Could not delete preference";
                return;
            }

            OutputTextBlock.Text = string.Empty;
        }

        private void PreferencesCombobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            FillInputs(PreferencesCombobox.SelectedItem as SavedPreference);

            Preferences = new ObservableCollection<SavedPreference>(PreferencesRepository.PreferencesRepositoryInstance.GetAll());
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = e.Uri.AbsoluteUri;
            proc.Start();
        }

        private void OnResultFileCreated(object source, FileSystemEventArgs e)
        {
            List<SearchResult> newResults = ResultService.ResultServiceInstance.GetResultsFromFile(e.FullPath);

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (SearchResult item in newResults)
                {
                    if (!Results.Any(x => x.HotelName == item.HotelName))
                    {
                        Results.Add(item);
                        SoundPlayer wowSound = new SoundPlayer("../../../../Resources/Sounds/success.wav");
                        wowSound.Play();
                    }
                }
            }
            );
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ResultService.ResultServiceInstance.CleanSearchOrders();
        }
    }
}
