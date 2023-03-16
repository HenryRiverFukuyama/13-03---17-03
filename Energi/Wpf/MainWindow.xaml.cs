using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Energi;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // NOTE!!!!!
        // I have used “ErrorLabel.Content” as a substitute for an error-label.
        // I have used “Label.Content” as a substitute for all other labels.


        public EnergiDatabase Database;
        public List<Person> Peoples;
        public List<Incident> Incidents;

        public MainWindow()
        {
            InitializeComponent();

            Database = new EnergiDatabase();
            Peoples = new List<Person>();
            Incidents = new List<Incident>();

            UpdateListPerson();
            UpdateListIncident();
        }

        public void UpdateListPerson()
        {
            try
            {
                Database.TryOpen();
                Peoples = Database.GetPersons();
                Database.TryClose();
            }
            catch (InvalidOperationException m)
            {
                ErrorLabel.Content = m.Message;
                Database.TryClose();
            }
            catch (Exception)
            {
                ErrorLabel.Content = "Unknown error.";
                Database.TryClose();
            }
        }
        public void UpdateListIncident()
        {
            try
            {
                Database.TryOpen();
                Incidents = Database.GetIncidents();
                Database.TryClose();
            }
            catch (InvalidOperationException m)
            {
                ErrorLabel.Content = m.Message;
                Database.TryClose();
            }
            catch (Exception)
            {
                ErrorLabel.Content = "Unknown error.";
                Database.TryClose();
            }
        }
        public void labelsLabels()
        {
            // Needs to be fulled out with other labels, as “Label.Content” is just a substitute!
            Label.Content = "";
        }

        private void Button_UploadPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                Person person = new Person((int)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content);
                Database.UploadPerson(person);
                UpdateListPerson();
                Database.TryClose();
            }
            catch (InvalidOperationException m)
            {
                ErrorLabel.Content = m.Message;
                Database.TryClose();
            }
            catch (Exception)
            {
                ErrorLabel.Content = "Unknown error.";
                Database.TryClose();
            }
        }
        private void Button_UploadIncident_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                Incident incident = new Incident((int)Label.Content, (int)Label.Content, (int)Label.Content, (DateTime)Label.Content);
                Database.UploadIncident(incident);
                UpdateListIncident();
                Database.TryClose();
            }
            catch (InvalidOperationException m)
            {
                ErrorLabel.Content = m.Message;
                Database.TryClose();
            }
            catch (Exception)
            {
                ErrorLabel.Content = "Unknown error.";
                Database.TryClose();
            }
        }
        private void Button_UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                Person person = new Person((int)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content);
                Database.UpdatePerson(person);
                UpdateListPerson();
                Database.TryClose();
            }
            catch (InvalidOperationException m)
            {
                ErrorLabel.Content = m.Message;
                Database.TryClose();
            }
            catch (Exception)
            {
                ErrorLabel.Content = "Unknown error.";
                Database.TryClose();
            }
        }
        private void Button_UpdateIncident_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                Incident incident = new Incident((int)Label.Content, (int)Label.Content, (int)Label.Content, (DateTime)Label.Content);
                Database.UpdateIncident(incident);
                UpdateListIncident();
                Database.TryClose();
            }
            catch (InvalidOperationException m)
            {
                ErrorLabel.Content = m.Message;
                Database.TryClose();
            }
            catch (Exception)
            {
                ErrorLabel.Content = "Unknown error.";
                Database.TryClose();
            }
        }
        private void Button_DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                Person person = new Person((int)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content);
                Database.DeletePerson(person);
                UpdateListPerson();
                Database.TryClose();
            }
            catch (InvalidOperationException m)
            {
                ErrorLabel.Content = m.Message;
                Database.TryClose();
            }
            catch (Exception)
            {
                ErrorLabel.Content = "Unknown error.";
                Database.TryClose();
            }
        }
        private void Button_DeleteIncident_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                Incident incident = new Incident((int)Label.Content, (int)Label.Content, (int)Label.Content, (DateTime)Label.Content);
                Database.DeleteIncident(incident);
                UpdateListIncident();
                Database.TryClose();
            }
            catch (InvalidOperationException m)
            {
                ErrorLabel.Content = m.Message;
                Database.TryClose();
            }
            catch (Exception)
            {
                ErrorLabel.Content = "Unknown error.";
                Database.TryClose();
            }
        }
    }
}
