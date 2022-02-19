using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eMaxPressure
    {
        CarMaxPressure = 29,
        MotorcyclePressure = 30,
        TruckMaxPressure = 25
    }
    // $G$ DSN-999 (-5) This class should have been internal.
    public class Wheel
    {
        private string m_NameOfManufacturer;
        private float m_Pressure;
        // $G$ DSN-005 (-3) This property should have been read-only.
        private float m_MaxPressure;

        public Wheel()
        {
            NameOfManufacturer = m_NameOfManufacturer;
            Pressure = m_Pressure;
            MaxPressure = m_MaxPressure;
        }

        public string NameOfManufacturer
        {
            get { return m_NameOfManufacturer;}
            set { m_NameOfManufacturer = value; }
        }

        // $G$ DSN-005 (-3) The setter of this property should not have been public. Modification of the current air pressure should be done in the inflate method exclusively
        public float Pressure
        {
            get { return m_Pressure; }
            set { m_Pressure = value; }
        }

        public float MaxPressure
        {
            get { return m_MaxPressure;}
            set { m_MaxPressure = value; }
        }


        public void Inflate(float i_AirToAdd)
        {
            float maxAirPressureToAdd = MaxPressure - Pressure;

            if (i_AirToAdd <= maxAirPressureToAdd)
            {
                Pressure += i_AirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(maxAirPressureToAdd, 0, "you cannot inflate more than the maximum");
            }
        }

    }
}
