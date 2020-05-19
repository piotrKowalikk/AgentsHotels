using HotelsLogic;
using System;
using System.Windows;


namespace HotelsGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!DateFrom.SelectedDate.HasValue || !DateTo.SelectedDate.HasValue || !Int32.TryParse(AdultsNumberText.Text, out int adultsNumber))
            {
                return;
            }

            UserPreference userPreference = new UserPreference()
            {
                City = CityTextbox.Text,
                DateTo = DateTo.SelectedDate.Value,
                DateFrom = DateFrom.SelectedDate.Value,
                NumberOfAdults = adultsNumber
            };

            SearchService.SearchServiceInstance.Search(userPreference);
        }
    }
}
