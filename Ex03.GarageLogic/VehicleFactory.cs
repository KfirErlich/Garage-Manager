using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eTypeOfVehicle
    {
        FuelCar=1,
        ElectricCar,
        FuelMotorcycle,
        ElectricMotorcycle,
        FuelTruck
    }
    public class VehicleFactory
    {
        
        public Vehicle CreateVehicle(eTypeOfVehicle i_TypeOfVehicle, string i_LicenseNumber)
        {
            Vehicle newVehicleToCreate;
            Engine engineToAdd;

            switch (i_TypeOfVehicle)
            {
                case eTypeOfVehicle.FuelCar:
                    {
                        newVehicleToCreate = new Car(i_LicenseNumber);
                        engineToAdd = new FuelEngine((float)eFullCapcity.FuelCar / 10, eEnergyType.Octan95);
                        newVehicleToCreate.Engine = engineToAdd;
                        break;
                    }

                case eTypeOfVehicle.ElectricCar:
                    {
                        newVehicleToCreate = new Car(i_LicenseNumber);
                        engineToAdd = new ElectricEngine((float)eFullCapcity.ElectricCar / 10, eEnergyType.Electricity);
                        newVehicleToCreate.Engine = engineToAdd;
                        break;
                    }

                case eTypeOfVehicle.FuelMotorcycle:
                    {
                        newVehicleToCreate = new Motorcycle(i_LicenseNumber);
                        engineToAdd = new FuelEngine((float)eFullCapcity.FuelMotorcycle / 10, eEnergyType.Ocatn98);
                        newVehicleToCreate.Engine = engineToAdd;
                        break;
                    }

                case eTypeOfVehicle.ElectricMotorcycle:
                    {
                        newVehicleToCreate = new Motorcycle(i_LicenseNumber);
                        engineToAdd = new ElectricEngine((float)eFullCapcity.ElectricMotorcycle / 10, eEnergyType.Electricity);
                        newVehicleToCreate.Engine = engineToAdd;
                        break;
                    }

                case eTypeOfVehicle.FuelTruck:
                    {
                        newVehicleToCreate = new Truck(i_LicenseNumber);
                        engineToAdd = new FuelEngine((float)eFullCapcity.FuelTruck / 10, eEnergyType.Soler);
                        newVehicleToCreate.Engine = engineToAdd;
                        break;
                    }

                default:
                    {
                        throw new ArgumentException("Type is not valid");
                    }
            }

            newVehicleToCreate.Type = i_TypeOfVehicle;
            return newVehicleToCreate;
        }
    }
}
