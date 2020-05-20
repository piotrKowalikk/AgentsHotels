using HotelsLogic;
using System;
using System.Collections.ObjectModel;
using System.IO;
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
                ValidationTextBlock.Text = "Enter valid input";
                return;
            }

            ValidationTextBlock.Text = string.Empty;

            UserPreference userPreference = new UserPreference()
            {
                City = CityTextbox.Text,
                DateTo = DateTo.SelectedDate.Value,
                DateFrom = DateFrom.SelectedDate.Value,
                NumberOfAdults = adultsNumber
            };

            SearchService.SearchServiceInstance.Search(userPreference);

            await ResultService.ResultServiceInstance.WaitForResult(new FileSystemEventHandler(OnResultCreated));
        }

        private void OnResultCreated(object source, FileSystemEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
                Results.Add(new SearchResult("Trivago", "Tani Hotel", "www.hotel.pl"))
            );
        }
    }
}
