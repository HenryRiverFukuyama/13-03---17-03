using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi
{
    public class Incident
    {
        private int ID_;
        private int PersonID_;
        private int Energi_;
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

        public Incident(int iD_, int personID_, int energi_, DateTime time_)
        {
            ID = iD_;
            PersonID = personID_;
            Energi = energi_;
            Time = time_;
        }
    }
}
