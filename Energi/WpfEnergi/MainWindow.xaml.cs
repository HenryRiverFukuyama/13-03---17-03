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
        // I have used “LogInLabel.Content” as a substitute for all Log-In Labels.
        // I have used (Person)PersonDataGrid.SelectedItem as a substitute for the Selected Item from a Incidents Data-Grid
        // I have used (Incident)IncidentsDataGrid.SelectedItem as a substitute for the Selected Item from a Incidents Data-Grid

        public EnergiDatabase Database;
        public Person Person;
        public List<Incident> Incidents;

        public MainWindow()
        {
            InitializeComponent();

            Database = new EnergiDatabase();
            Person = new Person(0, "", "", "", "", "", "", "");
            Incidents = new List<Incident>();
        }

        public void UpdateLabels()
        {
            Label.Content = "";
            LogInLabel.Content = "";
        }
        public void LogInTest()
        {

            Person TestPerson = Database.GetPersonsById((int)Label.Content, (string)Label.Content);
            if (Person.ID != (int)Label.Content)
            {
                throw new AggregateException("Invalid unilogin.");
            }

        }

        public void Button_LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                Person = Database.GetPersonsById((int)Label.Content, (string)Label.Content);
                Incidents = Database.GetIncidentsById((int)Label.Content, (string)Label.Content);
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
        public void Button_UploadPerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                Person person = new Person((int)LogInLabel.Content, (string)LogInLabel.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content, (string)Label.Content);
                Database.UploadPerson(person);
                UpdateLabels();
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
        public void Button_UploadIncident_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                LogInTest();
                Incident incident = new Incident((int)Label.Content, Person.ID, (int)Label.Content, (int)Label.Content, (string)Label.Content, DateTime.Now);
                Database.UploadIncident(incident);
                UpdateLabels();
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
        public void Button_UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                LogInTest();
                Database.UpdatePerson(Person);
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
        public void Button_UpdateIncident_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                LogInTest();
                Incident SelectedOreder = (Incident)IncidentsDataGrid.SelectedItem;
                Incident incident = new Incident(SelectedOreder.ID, Person.ID, SelectedOreder.Energi, SelectedOreder.IncidentEffect, SelectedOreder.ActivityAndCauses, SelectedOreder.Time);
                Database.UpdateIncident(incident);
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
        public void Button_DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                LogInTest();
                Database.DeletePerson(Person);
                Person = new Person(0, "", "", "", "", "", "", "");
                Incidents = new List<Incident>();
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
        public void Button_DeleteIncident_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Database.TryOpen();
                LogInTest();
                Database.DeleteIncident((Incident)IncidentsDataGrid.SelectedItem);
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
        public void Button_LogOut_Click(object sender, RoutedEventArgs e)
        {
            Person = new Person(0, "", "", "", "", "", "", "");
            Incidents = new List<Incident>();
            UpdateLabels();
        }
    }
}
