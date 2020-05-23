using HotelsLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

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

            Results = new ObservableCollection<SearchResult>();
            ResultListView.ItemsSource = Results;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DateFrom.SelectedDate.HasValue || !DateTo.SelectedDate.HasValue || !Int32.TryParse(AdultsNumberText.Text, out int adultsNumber))
            {
                OutputTextBlock.Text = "Enter valid input";
                return;
            }

            OutputTextBlock.Text = string.Empty;

            UserPreference userPreference = new UserPreference()
            {
                City = CityTextbox.Text,
                DateTo = DateTo.SelectedDate.Value,
                DateFrom = DateFrom.SelectedDate.Value,
                NumberOfAdults = adultsNumber
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
                        Results.Add(item);
                }
            }
            );
        }
    }
}
