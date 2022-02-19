using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        private float m_BatteryLeftInHours;
        private float m_MaxBatteryLifeInHours;

        public ElectricEngine(float i_MaxBatteryLifeInHours, eEnergyType i_EnergyType) : base(i_EnergyType)
        {
            MaxBatteryLifeInHours = i_MaxBatteryLifeInHours;
        }
        public float BatteryLeftInHours
        {
            get { return m_BatteryLeftInHours;}
            set { m_BatteryLeftInHours = value; }
        }

        public float MaxBatteryLifeInHours
        {
            get { return m_MaxBatteryLifeInHours;}
            set { m_MaxBatteryLifeInHours = value; }
        }
        //void Charge(float i_AddHours);
        public override float GetCurrentCapcity()
        {
           return BatteryLeftInHours;
        }
        public override void addEnergy(float i_EnergyToAdd)
        {
            if (i_EnergyToAdd + BatteryLeftInHours > MaxBatteryLifeInHours)
            {
                throw new ArgumentException(
                    "The energy you want to add is more than the possible capacity of your maximum energy");

            }

            BatteryLeftInHours += i_EnergyToAdd;
        }
    }
}
