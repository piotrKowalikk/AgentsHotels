using HotelsLogic;
using HotelsLogic.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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
    public delegate void CreatePreferenceEventHandler(string name);
    public partial class MainWindow : Window
    {
        private int numberOfReturnOffers = 3;
        private string defaultPreferenceName = "--default preference--";
        private event CreatePreferenceEventHandler preferenceCreationEvent;
        public ObservableCollection<SearchedHotel> Results { get; set; }
        public ObservableCollection<SavedPreference> Preferences { get; set; }

        public MainWindow()
        {
            preferenceCreationEvent += AddNewPreference;
            InitializeComponent();
            Results = new ObservableCollection<SearchedHotel>();
            ResultListView.ItemsSource = Results;

            Preferences = new ObservableCollection<SavedPreference>(PreferencesRepository.PreferencesRepositoryInstance.GetAll());
            if (Preferences.Count == 0)
            {
                AddDefaultPreference();
                Preferences = new ObservableCollection<SavedPreference>(PreferencesRepository.PreferencesRepositoryInstance.GetAll());
            }

            PreferencesCombobox.ItemsSource = Preferences;
            StarsComboBox.ItemsSource = Enum.GetValues(typeof(StarsChoice)).Cast<StarsChoice>();
            AddInitialInput();
        }

        #region GUI elements events
        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputIsValid(out string message))
            {
                MessageBox.Show(message);
                return;
            }

            ResultService.ResultServiceInstance.CleanResultsFolder();
            SearchService.SearchServiceInstance.CleanServiceFolder();
            await Task.Delay(2000);

            PreferencesRepository.PreferencesRepositoryInstance.DefaultPreferenceName = ((SavedPreference)PreferencesCombobox.SelectedItem).PreferenceName;

            UserPreference userPreference = GetUserPreferenceFromInputs();
            SearchService.SearchServiceInstance.Search(userPreference);

            await ResultService.ResultServiceInstance.WaitForResult(new FileSystemEventHandler(OnResultFileCreated));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!InputIsValid(out string message))
            {
                MessageBox.Show(message);
            }

            NewPreferenceWindow newPreferenceWindow = new NewPreferenceWindow(preferenceCreationEvent);
            newPreferenceWindow.Show();
        }

        private void AddNewPreference(string name)
        {
            SavedPreference newPreference = GetSavedPreferenceFromInputs(name);
            PreferencesRepository.PreferencesRepositoryInstance.Add(newPreference);

            RefreshGUI();
            int index = 0;
            foreach (var pref in PreferencesRepository.PreferencesRepositoryInstance.GetAll())
            {
                if (pref.PreferenceName == name)
                {
                    PreferencesCombobox.SelectedIndex = index;
                    break;
                }
                ++index;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            PreferencesRepository prefRepo = PreferencesRepository.PreferencesRepositoryInstance;
            SavedPreference pref = (SavedPreference)PreferencesCombobox.SelectedItem;
            prefRepo.Delete(pref);

            if (prefRepo.GetAll().Count() == 0)
            {
                AddDefaultPreference();
            }
            else if (prefRepo.DefaultPreferenceName == pref.PreferenceName)
            {
                if (prefRepo.GetPreference(defaultPreferenceName) != null)
                {
                    prefRepo.DefaultPreferenceName = defaultPreferenceName;
                }
                else
                {
                    prefRepo.DefaultPreferenceName = prefRepo.GetAll().First().PreferenceName;
                }
            }

            RefreshGUI();
            PreferencesCombobox.SelectedIndex = 0;
        }

        private void PreferencesCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SavedPreference pref = (SavedPreference)PreferencesCombobox.SelectedItem;

            if (pref != null)
            {
                FillInputs(pref);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ResultService.ResultServiceInstance.CleanSearchOrders();
        }
        #endregion

        private void AddInitialInput()
        {
            PreferencesRepository prefRepo = PreferencesRepository.PreferencesRepositoryInstance;
            string defPrefName = prefRepo.DefaultPreferenceName;
            SavedPreference defaultPreference = prefRepo.GetPreference(defPrefName);

            int index = 0;
            foreach (var pref in prefRepo.GetAll())
            {
                if (pref.PreferenceName == defaultPreference.PreferenceName)
                {
                    PreferencesCombobox.SelectedIndex = index;
                    return;
                }
                ++index;
            }
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
                        if (!Results.Any(x => x == hotel))  // check if already in
                        {
                            Results.Add(hotel);
                            SoundPlayer wowSound = new SoundPlayer("../../../../Resources/Sounds/success.wav");
                            wowSound.Play();
                        }
                    }
                    List<SearchedHotel> resultsArray = Results.ToList();
                    resultsArray.Sort((y, x) => x.Score.Value.CompareTo(y.Score.Value));
                    Results = new ObservableCollection<SearchedHotel>(resultsArray);
                    ResultListView.ItemsSource = Results;
                }
            }
            );
        }

        private bool InputIsValid(out string message)
        {
            if (!DateFrom.SelectedDate.HasValue)
            {
                message = "Select value of DateFrom";
                return false;
            }
            else if (!DateTo.SelectedDate.HasValue)
            {
                message = "Select value of DateTo";
                return false;
            }
            else if (DateFrom.SelectedDate.Value.CompareTo(DateTo.SelectedDate.Value) >= 0)
            {
                message = "Date From cannot be greater than Date To";
                return false;
            }
            else if (DateFrom.SelectedDate.Value.CompareTo(DateTime.Today) < 0)
            {
                message = "Date From cannot be before today";
                return false;
            }

            message = null;
            return true;
        }

        private UserPreference GetUserPreferenceFromInputs()
        {
            return new UserPreference()
                    .WithCity(CityTextbox.Text)
                    .WithDateTo(DateTo.SelectedDate.Value)
                    .WithDateFrom(DateFrom.SelectedDate.Value)
                    .WithNumberOfAdults(AdultsNumberCombobox.SelectedIndex + 1)
                    .WithNumberOfChildren(ChildrenNumberCombobox.SelectedIndex)
                    .WithNumberOfRooms(RoomsNumberCombobox.SelectedIndex + 1)
                    .WithNumberOfReturnOffers(numberOfReturnOffers)
                    .WithDelay(GetDelaySecondsFromComboIndex(DelayCombobox.SelectedIndex))
                    .WithAirConditioning(AirConditioningCheckBox.IsChecked)
                    .WithFreeCancelation(FreeCancelationCheckBox.IsChecked)
                    .WithWifi(WIFICheckBox.IsChecked)
                    .WithBar(BarCheckBox.IsChecked)
                    .WithPool(PoolCheckBox.IsChecked)
                    .WithFridge(FridgeCheckBox.IsChecked)
                    .WithMicrowave(MicrowaveCheckBox.IsChecked)
                    .WithSafe(SafeCheckBox.IsChecked)
                    .WithTv(TVCheckBox.IsChecked)
                    .WithMassage(MassageCheckBox.IsChecked)
                    .WithSauna(SaunaCheckBox.IsChecked)
                    .WithGym(GymCheckBox.IsChecked)
                    .WithSpa(SpaCheckBox.IsChecked)
                    .WithStars((StarsChoice)StarsComboBox.SelectedItem);
        }

        private SavedPreference GetSavedPreferenceFromInputs(string name)
        {
            return new SavedPreference(name, GetUserPreferenceFromInputs());
        }

        private void FillInputs(SavedPreference savedPreference)
        {
            CityTextbox.Text = savedPreference.City;
            DateFrom.SelectedDate = DateTime.Today;
            DateTo.SelectedDate = DateTime.Today.AddDays(1);
            AdultsNumberCombobox.SelectedIndex = savedPreference.NumberOfAdults - 1;
            ChildrenNumberCombobox.SelectedIndex = savedPreference.NumberOfChildren;
            RoomsNumberCombobox.SelectedIndex = savedPreference.NumberOfRooms - 1;
            DelayCombobox.SelectedIndex = GetDelayComboIndexFromSeconds(savedPreference.Delay);
            AirConditioningCheckBox.IsChecked = savedPreference.AirConditioning;
            FreeCancelationCheckBox.IsChecked = savedPreference.FreeCancelation;
            WIFICheckBox.IsChecked = savedPreference.Wifi;
            BarCheckBox.IsChecked = savedPreference.Bar;
            PoolCheckBox.IsChecked = savedPreference.Pool;
            FridgeCheckBox.IsChecked = savedPreference.Fridge;
            MicrowaveCheckBox.IsChecked = savedPreference.Microwave;
            SafeCheckBox.IsChecked = savedPreference.Safe;
            TVCheckBox.IsChecked = savedPreference.Tv;
            MassageCheckBox.IsChecked = savedPreference.Massage;
            SaunaCheckBox.IsChecked = savedPreference.Sauna;
            GymCheckBox.IsChecked = savedPreference.Gym;
            SpaCheckBox.IsChecked = savedPreference.Spa;
            StarsComboBox.SelectedIndex = savedPreference.Stars;
        }

        private int GetDelayComboIndexFromSeconds(int seconds)
        {
            return (seconds / 3) - 1;
        }

        private int GetDelaySecondsFromComboIndex(int index)
        {
            return (index + 1) * 3;
        }


        private void AddDefaultPreference()
        {
            SavedPreference defaultPreference = (SavedPreference)(new SavedPreference()
            {
                PreferenceName = defaultPreferenceName,
            }
            .WithCity("Warsaw")
            .WithDateFrom(DateTime.Today)
            .WithDateTo(DateTime.Today.AddDays(1))
            .WithNumberOfAdults(2)
            .WithNumberOfChildren(0)
            .WithNumberOfRooms(1)
            .WithDelay(6)
            .WithNumberOfReturnOffers(numberOfReturnOffers));

            PreferencesRepository repo = PreferencesRepository.PreferencesRepositoryInstance;
            repo.Add(defaultPreference);
            repo.DefaultPreferenceName = defaultPreference.PreferenceName;
        }

        private void RefreshGUI()
        {
            Preferences = new ObservableCollection<SavedPreference>(PreferencesRepository.PreferencesRepositoryInstance.GetAll());
            PreferencesCombobox.ItemsSource = Preferences;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = e.Uri.AbsoluteUri;
            proc.Start();
        }
    }
}
