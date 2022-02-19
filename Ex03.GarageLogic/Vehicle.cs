using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Ex03.GarageLogic
{
    public enum eNumOfWheels
    {
        MotorcycleWheels = 2, 
        CarWheels = 4,
        TruckWheels = 16
    }
    public enum eStatus
    {
        Fixed=1,
        InRepair,
        Paid
    }
    public enum eFuelType
    {
        Octan98,
        Octan96,
        Octan95,
        Soler
    }
    public abstract class Vehicle
    {
 
        private string m_Model;
        private string m_LicenseNumber;
        private eTypeOfVehicle m_Type;
        private eNumOfWheels m_NumOfWheels;
        private List<Wheel> m_Wheel;
        private Engine m_Engine;

        // $G$ DSN-006 (-5) This method should have been protected.
        public Vehicle(string i_LicenseNumber)
        {
            LicenseNumber = i_LicenseNumber;
        }

        public eTypeOfVehicle Type
        {
            get { return m_Type;}
            set { m_Type = value; }
        }
        public Engine Engine
        {
            get { return m_Engine; }
            set { m_Engine = value;}
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheel; }
            set { m_Wheel = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber;}
            set { m_LicenseNumber = value; }
        }

        public string Model
        {
            get { return m_Model; }
            set { m_Model = value; }
        }

        public eNumOfWheels NumOfWheels
        {
            get { return m_NumOfWheels; }
            set { m_NumOfWheels = value; }
        }

        public abstract void SetUniqueInfo(string i_Info1, string i_Info2);

        public void InflateWheelsToMax()
        {
            float maxCapacity;

            foreach (Wheel wheel in this.Wheels)
            {
                maxCapacity = wheel.MaxPressure;
                wheel.Inflate(maxCapacity - wheel.Pressure);
            }
        }
        public abstract string getUniqueInfoFromGarage();

    }
}
