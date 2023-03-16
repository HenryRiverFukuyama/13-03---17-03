using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Energi
{
    public class Incident
    {
        private int ID_;
        private int PersonID_;
        private int Energi_;
        private int IncidentEffect_;
        private string ActivityAndCauses_;
        private DateTime Time_;

        public int ID
        {
            get
            {
                return ID_;
            }

            set
            {
                ID_ = value;
            }
        }
        public int PersonID
        {
            get
            {
                return PersonID_;
            }

            set
            {
                PersonID_ = value;
            }
        }
        public int Energi
        {
            get 
            {
                return Energi_;
            }

            set
            {
                if (value < 1 && value > 5)
                {
                    throw new AggregateException("Incident.Energi can't be be");
                }

                Energi_ = value;
            }
        }
        public int IncidentEffect
        {
            get 
            { 
                return IncidentEffect_; 
            }
            set
            {
                if (value < 2 && value > -2)
                {
                    IncidentEffect_ = value;
                }
                else
                {
                    throw new AggregateException("Energi.IncidentEffect.value needs to be -1, 0 or 1.");
                }
            }
        }
        public string ActivityAndCauses
        {
            get
            {
                return ActivityAndCauses_;
            }

            set
            {
                if (value == null)
                {
                    throw new AggregateException("Incident.ActivityAndCauses can't be null");
                }
                ActivityAndCauses_ = value;
            }
        }
        public DateTime Time
        {
            get
            {
                return Time_;
            }

            set
            {
                if (value > DateTime.Now) 
                {
                    throw new AggregateException("Time of Incident can't be set in the future.");
                }
                Time_ = value;
            } 
        }

        public Incident(int iD_, int personID_, int energi_, int incidentEffect_, string activityAndCauses_, DateTime time_)
        {
            ID = iD_;
            PersonID = personID_;
            Energi = energi_;
            IncidentEffect = incidentEffect_;
            ActivityAndCauses = activityAndCauses_;
            Time = time_;
        }
    }
}
