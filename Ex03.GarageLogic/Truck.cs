using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormatException = System.FormatException;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsAbleToDeliverCold;
        private float m_CargoVolume;
        private readonly int r_NumOfWheels = (int) eNumOfWheels.TruckWheels;

        public Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
        }

        public int NumOfWheels
        {
            get { return r_NumOfWheels;} 
        }

        public bool IsAbleToDeliverCold
        {
            get { return m_IsAbleToDeliverCold;}
            set { m_IsAbleToDeliverCold = value; }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume;}
            set { m_CargoVolume = value; }
        }

        public override void SetUniqueInfo(string i_Info1, string i_Info2)
        {

            if (i_Info1 == "y" || i_Info1 == "Y")
            {
                IsAbleToDeliverCold = true;
            }
            else
            {
                if (i_Info1 == "n" || i_Info1 == "N")
                {
                    IsAbleToDeliverCold = false;
                }
                else
                {
                    throw new FormatException("You entered the wrong letter");
                }
                
            }
            if (float.TryParse(i_Info2, out m_CargoVolume))
            {
                CargoVolume = m_CargoVolume;
                if (CargoVolume < 0)
                {
                    throw new FormatException("You entered invalid cargo volume");
                }
            }
        }


        public override string getUniqueInfoFromGarage()
        {
            return string.Format("The truck is able to deliver cold cargo: {0} \nThe cargo volume of the truck is: {1}", IsAbleToDeliverCold, CargoVolume);

        }
    }
}
