using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi
{
    public class Person
    {
        private int ID_;
        private string Name_;
        private string EnergiLevel1_;
        private string EnergiLevel2_;
        private string EnergiLevel3_;
        private string EnergiLevel4_;
        private string EnergiLevel5_;
        private string GeneralStrategy_;

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
        public string Name
        {
            get 
            { 
                return Name_;
            }

            set 
            { 
                if(value== null) 
                {
                    throw new AggregateException("Person.ID can't be null");
                }
                Name_ = value;
            }
        }
        public string EnergiLevel1
        {
            get
            {
                return EnergiLevel1_;
            }

            set
            {
                if (value == null)
                {
                    throw new AggregateException("Person.EnergiLevel1 can't be null");
                }
                EnergiLevel1_ = value;
            }
        }
        public string EnergiLevel2
        {
            get
            {
                return EnergiLevel2_;
            }

            set
            {
                if (value == null)
                {
                    throw new AggregateException("Person.EnergiLevel2 can't be null");
                }
                EnergiLevel2_ = value;
            }
        }
        public string EnergiLevel3
        {
            get
            {
                return EnergiLevel3_;
            }

            set
            {
                if (value == null)
                {
                    throw new AggregateException("Person.EnergiLevel3 can't be null");
                }
                EnergiLevel3_ = value;
            }
        }
        public string EnergiLevel4
        {
            get
            {
                return EnergiLevel4_;
            }

            set
            {
                if (value == null)
                {
                    throw new AggregateException("Person.EnergiLevel4 can't be null");
                }
                EnergiLevel4_ = value;
            }
        }
        public string EnergiLevel5
        {
            get
            {
                return EnergiLevel5_;
            }

            set
            {
                if (value == null)
                {
                    throw new AggregateException("Person.EnergiLevel5 can't be null");
                }
                EnergiLevel5_ = value;
            }
        }
        public string GeneralStrategy
        {
            get
            {
                return GeneralStrategy_;
            }

            set
            {
                if (value == null)
                {
                    throw new AggregateException("Person.GeneralStrategy can't be null");
                }
                GeneralStrategy_ = value;
            }
        }

        public Person(int iD_, string name_, string energiLevel1_, string energiLevel2_, string energiLevel3_, string energiLevel4_, string energiLevel5_, string generalStrategy_)
        {
            ID = iD_;
            Name = name_;
            EnergiLevel1 = energiLevel1_;
            EnergiLevel2 = energiLevel2_;
            EnergiLevel3 = energiLevel3_;
            EnergiLevel4 = energiLevel4_;
            EnergiLevel5 = energiLevel5_;
            GeneralStrategy = generalStrategy_;
        }
    }
}
