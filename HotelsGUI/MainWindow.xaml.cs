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
        public MainWindow()
        {
            InitializeComponent();

            AddInitialInput();

            Results = new ObservableCollection<SearchResult>();
            ResultListView.ItemsSource = Results;
        }

        private void AddInitialInput()
        {
            CityTextbox.Text = "Warszawa";
            DateFrom.SelectedDate = DateTime.Today;
            DateTo.SelectedDate = DateTime.Today.AddDays(1);
            AdultsNumberCombobox.SelectedIndex = 1;
            DelayCombobox.SelectedIndex = 1;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if(!InputIsValid(out string message))
            {
                OutputTextBlock.Text = message;
                return;
            }

            ResultService.ResultServiceInstance.CleanResultsFolder();

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
                        SoundPlayer wowSound = new SoundPlayer("./success.wav");
                        wowSound.Play();
                    }
                }
            }
            );
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = e.Uri.AbsoluteUri;
            proc.Start();
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
    }
}
