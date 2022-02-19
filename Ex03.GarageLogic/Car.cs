using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eColors
    {
        Red = 1, 
        White, 
        Black,
        Blue
    }

    public enum eNumOfDoors
    {
        Two = 1,
        Three,
        Four,
        Five
    }
    public class Car : Vehicle
    {
        private eColors m_Color;
        private eNumOfDoors m_NumOfDoors;
        private readonly int r_NumOfWheels = (int)eNumOfWheels.CarWheels;

        public Car(string i_LicenseNumber) : base (i_LicenseNumber)
        {
        }
        public int NumOfWheels
        {
            get { return r_NumOfWheels; }
        }

        public eColors Color
        {
            get { return m_Color;}
            set { m_Color = value; }
        }

        public eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors;}
            set { m_NumOfDoors = value; }
        }

        public override void SetUniqueInfo(string i_Info1, string i_Info2)
        {
            if (eNumOfDoors.TryParse(i_Info1, out m_NumOfDoors))
            {
                NumOfDoors = m_NumOfDoors;
                if (NumOfDoors < eNumOfDoors.Two || NumOfDoors > eNumOfDoors.Five)
                {
                    throw new ValueOutOfRangeException((int)eNumOfDoors.Five, (int)eNumOfDoors.Two, "You Entered invalid number of doors");
                }
            }
            if (eColors.TryParse(i_Info2, out m_Color))
            {
                Color = m_Color;
                if (Color <  eColors.Red || Color > eColors.Blue)
                {
                    throw new ValueOutOfRangeException((int)eColors.Blue, (int)eColors.Red, "You Entered invalid color");
                }
            }
        }

        public override string getUniqueInfoFromGarage()
        {
            return string.Format("The number of doors is: {0} \nThe color of the car is: {1}", NumOfDoors, Color);
            
        }


    }
}
