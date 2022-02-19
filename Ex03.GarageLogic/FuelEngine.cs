using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private string m_FuelType;
        private float m_CurrentFuelAmount;
        private float m_MaxFuelAmount;

        public FuelEngine(float i_MaxFuelAmount, eEnergyType i_EnergyType) : base(i_EnergyType)
        {
            MaxFuelAmount = i_MaxFuelAmount;
        }
        public string FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public float CurrentFuelAmount
        {
            get { return m_CurrentFuelAmount; }
            set { m_CurrentFuelAmount = value; }
        }

        public float MaxFuelAmount
        {
            get { return m_MaxFuelAmount; }
            set { m_MaxFuelAmount = value; }
        }

        public override float GetCurrentCapcity()
        {
            return CurrentFuelAmount;
        }

        // $G$ CSS-011 (-3) Public methods should start with an Uppercase letter.
        public override void addEnergy(float i_EnergyToAdd)
        {
            if (i_EnergyToAdd + CurrentFuelAmount > MaxFuelAmount)
            {
                throw new ArgumentException(
                    "The energy you want to add is more than the possible capacity of your maximum energy");

            }

            CurrentFuelAmount += i_EnergyToAdd;
        }
    }
}
