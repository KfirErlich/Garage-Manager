using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        A = 1,
        A2,
        AA,
        B
    }
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        private readonly int r_NumOfWheels = (int)eNumOfWheels.MotorcycleWheels;

        public Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
        }
        public int NumOfWheels
        {
            get { return r_NumOfWheels; }
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType;}
            set { m_LicenseType = value; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume;}
            set { m_EngineVolume = value; }
        }

        public override void SetUniqueInfo(string i_Info1, string i_Info2)
        {
            if (eLicenseType.TryParse(i_Info1, out m_LicenseType))
            {
                LicenseType = m_LicenseType;
                if (LicenseType < eLicenseType.A || LicenseType > eLicenseType.B)
                {
                    throw new ValueOutOfRangeException((int)eLicenseType.B, (int)eLicenseType.A, "You Entered invalid input");
                }
            }
            if (int.TryParse(i_Info2, out m_EngineVolume))
            {
                EngineVolume = m_EngineVolume;
                if (EngineVolume < 0)
                {
                    throw new ArgumentException("You Entered invalid engine volume");
                }
            }
        }
        public override string getUniqueInfoFromGarage()
        {
            return string.Format("The license type is: {0} \nThe engine volume is: {1}", LicenseType, EngineVolume);

        }
    }
}
