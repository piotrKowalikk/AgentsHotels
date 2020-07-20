using HotelsLogic;
using HotelsLogic.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace HotelsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<SearchedHotel> Results { get; set; }
        public ObservableCollection<SavedPreference> Preferences { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Results = new ObservableCollection<SearchedHotel>();
            ResultListView.ItemsSource = Results;

            Preferences = new ObservableCollection<SavedPreference>(PreferencesRepository.PreferencesRepositoryInstance.GetAll());
            AddInitialInput();
            PreferencesCombobox.ItemsSource = Preferences;

            InitializeFiltersComboboxes();
        }

        private void AddInitialInput()
        {
            string defaultPrefName = PreferencesRepository.PreferencesRepositoryInstance.GetDefaultPreferenceName();
            SavedPreference defaultPref = PreferencesRepository.PreferencesRepositoryInstance.GetPreference(defaultPrefName);
            if (defaultPref != null)
            {
                FillInputs(defaultPref);
            }
            else
            {
                FillWithDefaultInput();
            }
        }

        private void FillWithDefaultInput()
        {
            CityTextbox.Text = "Warszawa";
            DateFrom.SelectedDate = DateTime.Today;
            DateTo.SelectedDate = DateTime.Today.AddDays(1);
            AdultsNumberCombobox.SelectedIndex = 1;
            DelayCombobox.SelectedIndex = 1;
        }

        private void FillInputs(SavedPreference savedPreference)
        {
            CityTextbox.Text = savedPreference.City;
            DateFrom.SelectedDate = DateTime.Today;
            DateTo.SelectedDate = DateTime.Today.AddDays(1);
            AdultsNumberCombobox.SelectedIndex = savedPreference.NumberOfAdults - 1;
            DelayCombobox.SelectedIndex = GetDelayComboIndexFromSeconds(savedPreference.Delay);
            PreferenceNameTextbox.Text = savedPreference.PreferenceName;
            IsDefaultPreferenceCheckbox.IsChecked = string.Equals(
                savedPreference.PreferenceName,
                PreferencesRepository.PreferencesRepositoryInstance.GetDefaultPreferenceName());
        }

        private int GetDelayComboIndexFromSeconds(int delay) => delay / 3 - 1;
        private int GetDelaySecondsFromComboIndex(int index) => (index + 1) * 3;

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputIsValid(out string message))
            {
                OutputTextBlock.Text = message;
                return;
            }

            ResultService.ResultServiceInstance.CleanResultsFolder();
            SearchService.SearchServiceInstance.CleanServiceFolder();
            await Task.Delay(2000);

            OutputTextBlock.Text = string.Empty;

            UserPreference userPreference = GetUserPreferenceFromInputs();
            SearchService.SearchServiceInstance.Search(userPreference);

            await ResultService.ResultServiceInstance.WaitForResult(new FileSystemEventHandler(OnResultFileCreated));
            OutputTextBlock.Text = "Waiting for results";
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPreferenceNameValid(PreferenceNameTextbox.Text, out string nameMessage))
            {
                OutputTextBlock.Text = nameMessage;
                return;
            }
            if (!InputIsValid(out string message))
            {
                OutputTextBlock.Text = message;
                return;
            }

            SavedPreference pref = GetSavedPreferenceFromInputs();

            PreferencesRepository.PreferencesRepositoryInstance.Add(pref);
            if (IsDefaultPreferenceCheckbox.IsChecked == true)
            {
                PreferencesRepository.PreferencesRepositoryInstance.SetDefaultPreference(pref.PreferenceName);
            }
            RefreshGUI();
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
            if (PreferencesCombobox.SelectedItem != null)
            {
                FillInputs(PreferencesCombobox.SelectedItem as SavedPreference);
            }

            Preferences = new ObservableCollection<SavedPreference>(PreferencesRepository.PreferencesRepositoryInstance.GetAll());
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = e.Uri.AbsoluteUri;
            proc.Start();
        }

        private async void OnResultFileCreated(object source, FileSystemEventArgs e)
        {
            List<SearchResult> newResults = await ResultService.ResultServiceInstance.GetResultsFromFile(e.FullPath);

            App.Current.Dispatcher.Invoke(() =>
            {
                foreach (SearchResult item in newResults)
                {
                    foreach (SearchedHotel hotel in item.HotelsList)
                    {
                        if (!Results.Any(x => x == hotel))
                        {
                            Results.Add(hotel);
                            SoundPlayer wowSound = new SoundPlayer("../../../../Resources/Sounds/success.wav");
                            wowSound.Play();
                        }
                    }
                }
            }
            );
        }

        private void RefreshGUI()
        {
            Preferences = new ObservableCollection<SavedPreference>(PreferencesRepository.PreferencesRepositoryInstance.GetAll());
            PreferencesCombobox.ItemsSource = Preferences;
            OutputTextBlock.Text = string.Empty;
        }

        private SavedPreference GetSavedPreferenceFromInputs() =>
            new SavedPreference
            {
                PreferenceName = PreferenceNameTextbox.Text,
                City = CityTextbox.Text,
                DateTo = DateTo.SelectedDate.Value,
                DateFrom = DateFrom.SelectedDate.Value,
                NumberOfAdults = AdultsNumberCombobox.SelectedIndex + 1,
                Delay = GetDelaySecondsFromComboIndex(DelayCombobox.SelectedIndex)
            };

        private UserPreference GetUserPreferenceFromInputs() =>
            new UserPreference()
                .WithCity(CityTextbox.Text)
                .WithDateTo(DateTo.SelectedDate.Value)
                .WithDateFrom(DateFrom.SelectedDate.Value)
                .WithNumberOfAdults(AdultsNumberCombobox.SelectedIndex + 1)
                .WithNumberOfChildren(ChildrenNumberCombobox.SelectedIndex)
                .WithDelay((DelayCombobox.SelectedIndex + 1) * 3)
                .WithAirConditioning((FiltersChoice)AirConditioningCombo.SelectedItem)
                .WithFreeCancelation((FiltersChoice)FreeCancelationCombo.SelectedItem)
                .WithWifi((FiltersChoice)WiFiCombo.SelectedItem)
                .WithBar((FiltersChoice)BarCombo.SelectedItem)
                .WithPool((FiltersChoice)PoolCombo.SelectedItem)
                .WithFridge((FiltersChoice)FridgeCombo.SelectedItem)
                .WithMicrowave((FiltersChoice)MicrowaveCombo.SelectedItem)
                .WithSafe((FiltersChoice)SafeCombo.SelectedItem)
                .WithTv((FiltersChoice)TVCombo.SelectedItem)
                .WithMassage((FiltersChoice)MassageCombo.SelectedItem)
                .WithSauna((FiltersChoice)SaunaCombo.SelectedItem)
                .WithGym((FiltersChoice)GymCombo.SelectedItem)
                .WithSpa((FiltersChoice)SpaCombo.SelectedItem)
                .WithStars((StarsChoice)StarsCombo.SelectedItem);

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

        private bool IsPreferenceNameValid(string name, out string message)
        {
            if (string.IsNullOrWhiteSpace(PreferenceNameTextbox.Text))
            {
                message = "Select name for your preference";
                return false;
            }
            if (string.Equals(PreferenceNameTextbox.Text, PreferencesRepository.DefaultPreferenceFileName))
            {
                message = "Please select different name";
                return false;
            }
            message = string.Empty;
            return true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ResultService.ResultServiceInstance.CleanSearchOrders();
        }

        private void InitializeFiltersComboboxes()
        {
            var combos = new List<object>()
            {
                AirConditioningCombo,
                FreeCancelationCombo,
                WiFiCombo,
                BarCombo,
                PoolCombo,
                FridgeCombo,
                MicrowaveCombo,
                SafeCombo,
                TVCombo,
                MassageCombo,
                SaunaCombo,
                GymCombo,
                SpaCombo
            };

            combos.ForEach(c => InitializeComboBoxWithFiltersChoice(c as ComboBox));
            InitializeComboBoxWithStarsChoice(StarsCombo);
        }

        private void InitializeComboBoxWithFiltersChoice(ComboBox box)
        {
            box.ItemsSource = Enum.GetValues(typeof(FiltersChoice)).Cast<FiltersChoice>();
            box.SelectedIndex = 0;
        }

        private void InitializeComboBoxWithStarsChoice(ComboBox box)
        {
            box.ItemsSource = Enum.GetValues(typeof(StarsChoice)).Cast<StarsChoice>();
            box.SelectedIndex = 0;
        }
    }
}
