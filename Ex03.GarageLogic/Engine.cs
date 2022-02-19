using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFullCapcity
    {
        FuelMotorcycle = 58,
        ElectricMotorcycle = 23,
        FuelCar = 480,
        ElectricCar = 26,
        FuelTruck = 1300
    }
    public enum eEnergyType
    {
        Ocatn98,
        Octan96,
        Octan95,
        Soler,
        Electricity
    }
    public abstract class Engine
    {
        private eEnergyType m_EnergyType;

        public Engine(eEnergyType i_EnergyType)
        {
            EnergyType = i_EnergyType;
        }

        public eEnergyType EnergyType
        {
            get { return m_EnergyType; }
            set { m_EnergyType = value; }
        }

        public abstract void addEnergy(float i_EnergyToAdd);
        public abstract float GetCurrentCapcity();

    }
}
