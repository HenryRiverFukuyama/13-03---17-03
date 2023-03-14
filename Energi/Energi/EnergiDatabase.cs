using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi
{
    public class EnergiDatabase
    {
        private SqlConnection Connection;

        public EnergiDatabase()
        {
            string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EnergiDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Connection = new SqlConnection(ConnectionString);
        }

        public void TryOpen()
        {
            try
            {
                Connection.Open();
            }
            catch (Exception)
            {
                throw new AggregateException("Could’t open NorthWind.");
            }
        }
        public void TryClose()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception)
            {
                throw new AggregateException("Could’t close NorthWind.");
            }
        }

        public List<Person> GetPersons()
        {
            string SQL = $"SELECT * FROM Person;";
            SqlCommand Command = new SqlCommand(SQL, Connection);
            SqlDataReader Reader = Command.ExecuteReader();

            List<Person> Persons = new List<Person>();
            while (Reader.Read()) 
            {
                int Id = (int)Reader["Id"];
                string Name = (string)Reader["Name"];
                string EnergiLevel1 = (string)Reader["EnergiLevel1"];
                string EnergiLevel2 = (string)Reader["EnergiLevel2"];
                string EnergiLevel3 = (string)Reader["EnergiLevel3"];
                string EnergiLevel4 = (string)Reader["EnergiLevel4"];
                string EnergiLevel5 = (string)Reader["EnergiLevel5"];
                Person Person = new Person(Id, Name, EnergiLevel1, EnergiLevel2, EnergiLevel3, EnergiLevel4, EnergiLevel5);
                Persons.Add(Person);
            }

            return Persons;
        }

        public List<Incident> GetIncidents()
        {
            string SQL = $"SELECT * FROM Incident;";
            SqlCommand Command = new SqlCommand(SQL, Connection);
            SqlDataReader Reader = Command.ExecuteReader();

            List<Incident> Incidents = new List<Incident>();
            while (Reader.Read())
            {
                int Id = (int)Reader["Id"];
                int PersonID = (int)Reader["PersonID"];
                int Energi = (int)Reader["Energi"];
                DateTime Time = (DateTime)Reader["Time"];
                Incident Incident = new Incident(Id, PersonID, Energi, Time);
                Incidents.Add(Incident);
            }

            return Incidents;
        }

        public void UploadPerson(Person NewPerson)
        {
            string SQL = $"INSERT INTO Person (Id, Name, EnergiLevel1, EnergiLevel2, EnergiLevel3, EnergiLevel4, EnergiLevel5) VALUES ({NewPerson.ID} , '{NewPerson.Name}', '{NewPerson.EnergiLevel1}', '{NewPerson.EnergiLevel2}', '{NewPerson.EnergiLevel3}', '{NewPerson.EnergiLevel4}', '{NewPerson.EnergiLevel5}');";
            SqlCommand Command = new SqlCommand(SQL, Connection);

            List<Person> Persons= this.GetPersons();
            foreach (Person Person in Persons) 
            {
                if (Person.ID == NewPerson.ID)
                {
                    throw new AggregateException("You can't uplode a person that doesn't have a unique ID");
                }
            }

            Command.ExecuteNonQuery();
        }

        public void UploadIncident(Incident NewIncident)
        {
            string SQL = $"INSERT INTO Incident (Id, PersonID, Energi, Time) VALUES ({NewIncident.ID}, {NewIncident.PersonID}, {NewIncident.Energi}, '{NewIncident.Time}');";
            SqlCommand Command = new SqlCommand(SQL, Connection);

            List<Incident> Incidents = this.GetIncidents();
            foreach (Incident Incident in Incidents)
            {
                if (Incident.ID == NewIncident.ID)
                {
                    throw new AggregateException("You can't uplode an incident that doesn't have a unique ID");
                }
            }

            List<Person> Persons = this.GetPersons();
            bool ForeignKeyMatch = false;
            foreach (Person Person in Persons)
            {
                if (Person.ID == NewIncident.PersonID)
                {
                    ForeignKeyMatch = true;
                }
            }
            if (ForeignKeyMatch == false) 
            {
                throw new AggregateException("You can't uplode an incident that has a unique person's ID");
            }

            Command.ExecuteNonQuery();
        }

        public void UpdatePerson(Person UpdatedPerson)
        {
            string SQL = $"UPDATE Person SET Name = '{UpdatedPerson.Name}', EnergiLevel1 = '{UpdatedPerson.EnergiLevel1}', EnergiLevel2 = '{UpdatedPerson.EnergiLevel2}', EnergiLevel3 = '{UpdatedPerson.EnergiLevel3}', EnergiLevel4 = '{UpdatedPerson.EnergiLevel4}', EnergiLevel5 = '{UpdatedPerson.EnergiLevel5}' WHERE Id = {UpdatedPerson.ID};";
            SqlCommand Command = new SqlCommand(SQL, Connection);

            List<Person> Persons = this.GetPersons();
            bool IdMatch = false;
            foreach (Person Person in Persons)
            {
                if (Person.ID == UpdatedPerson.ID)
                {
                    IdMatch = true;
                }
            }
            if (IdMatch == false)
            {
                throw new AggregateException("You can't update a person without a matching ID");
            }

            Command.ExecuteNonQuery();
        }

        public void UpdateIncident(Incident UpdatedIncident)
        {
            string SQL = $"UPDATE Incident SET PersonID = {UpdatedIncident.PersonID}, Energi = {UpdatedIncident.Energi}, Time = {UpdatedIncident.Time} WHERE Id = {UpdatedIncident.ID};";
            SqlCommand Command = new SqlCommand(SQL, Connection);

            List<Incident> Incidents = this.GetIncidents();
            bool IdMatch = false;
            foreach (Incident Incident in Incidents)
            {
                if (Incident.ID == UpdatedIncident.ID)
                {
                    IdMatch = true;
                }
            }
            if (IdMatch == false)
            {
                throw new AggregateException("You can't update an incident without a matching ID");
            }

            List<Person> Persons = this.GetPersons();
            bool ForeignKeyMatch = false;
            foreach (Person Person in Persons)
            {
                if (Person.ID == UpdatedIncident.PersonID)
                {
                    ForeignKeyMatch = true;
                }
            }
            if (ForeignKeyMatch == false)
            {
                throw new AggregateException("You can't update an incident that with a unique person's ID");
            }

            Command.ExecuteNonQuery();
        }

        public void DeletePerson(Person WrongPerson)
        {
            string SQL = $"DELETE FROM Person WHERE Id = {WrongPerson.ID};";
            string SqlForIncident = $"DELETE FROM Incident WHERE PersonID = {WrongPerson.ID};";
            SqlCommand Command = new SqlCommand(SQL, Connection);
            SqlCommand CommandForInciden = new SqlCommand(SqlForIncident, Connection);


            List<Person> Persons = this.GetPersons();
            bool IdMatch = false;
            foreach (Person Person in Persons)
            {
                if (Person.ID == WrongPerson.ID)
                {
                    IdMatch = true;
                }
            }
            if (IdMatch == false)
            {
                throw new AggregateException("You can't delete a person without a matching ID");
            }

            Command.ExecuteNonQuery();
            CommandForInciden.ExecuteNonQuery();
        }

        public void DeleteIncident(Incident WrongIncident)
        {
            string SQL = $"DELETE FROM Incident WHERE Id = {WrongIncident.ID};";
            SqlCommand Command = new SqlCommand(SQL, Connection);

            List<Incident> Incidents = this.GetIncidents();
            bool IdMatch = false;
            foreach (Incident Incident in Incidents)
            {
                if (Incident.ID == WrongIncident.ID)
                {
                    IdMatch = true;
                }
            }
            if (IdMatch == false)
            {
                throw new AggregateException("You can't delete an Incident without a matching ID");
            }

            Command.ExecuteNonQuery();
        }
    }
}
