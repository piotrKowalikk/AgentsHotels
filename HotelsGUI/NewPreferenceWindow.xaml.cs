using HotelsLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelsGUI
{
    /// <summary>
    /// Interaction logic for NewPreferenceWindow.xaml
    /// </summary>
    public partial class NewPreferenceWindow : Window
    {
        private CreatePreferenceEventHandler preferenceCreationEvent;
        string name = null;

        public NewPreferenceWindow(CreatePreferenceEventHandler preferenceCreationEvent)
        {
            this.preferenceCreationEvent = preferenceCreationEvent;
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (name != null)
                preferenceCreationEvent.Invoke(name);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPreferenceNameValid(PreferenceNameTextBox.Text, out string message))
            {
                MessageBox.Show(message, "Invalid name");
            }
            else
            {
                name = PreferenceNameTextBox.Text;
                Close();
            }
        }
        private bool IsPreferenceNameValid(string name, out string message)
        {
            if (string.IsNullOrWhiteSpace(PreferenceNameTextBox.Text))
            {
                message = "Please enter the name";
                return false;
            }
            else if (string.Equals(PreferenceNameTextBox.Text, "--default preference--") || string.Equals(PreferenceNameTextBox.Text, "Default"))
            {
                message = "Please select different name";
                return false;
            }

            IEnumerable<SavedPreference> prefList = PreferencesRepository.PreferencesRepositoryInstance.GetAll();

            foreach(var pref in prefList)
            {
                if(pref.PreferenceName == name)
                {
                    message = "Name already in use";
                    return false;
                }
            }

            message = string.Empty;
            return true;
        }
    }



}
