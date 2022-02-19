using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        // $G$ DSN-005 (-3) This property should have been read-only.
        private List<Client> m_Clients = new List<Client>();
        private VehicleFactory m_VehicleFactory = new VehicleFactory();

        public VehicleFactory VehicleFactory
        {
            get { return m_VehicleFactory; }
            set { m_VehicleFactory = value; }
        }

        public List<Client> Clients
        {
            get { return m_Clients; }
        }

        public void InsertNewVehicleToGarage(Client i_Client)
        {
            m_Clients.Add(i_Client);
        }

        public bool CheckIfLicenseNumberExists(string i_LicenseNumber)
        {
            bool isExist = false;
            foreach (Client currentClient in m_Clients)
            {
                if (currentClient.Vehicle.LicenseNumber == i_LicenseNumber)
                {
                    isExist = true;
                    break;
                }
                else
                {
                    isExist = false;
                }
            }
            return isExist;
        }

        public int GetClientIndex(string i_LicenseNumber)
        {
            int clientIndex = -1;

            foreach (Client currClient in m_Clients)
            {
                if (currClient.Vehicle.LicenseNumber == i_LicenseNumber)
                {
                    clientIndex = m_Clients.IndexOf(currClient);
                    break;
                }
            }

            return clientIndex;

        }

        public void FuelVehicle(string i_LicenseNumber, float i_FuelToAddInLiters)
        {
            int indexOfClient = GetClientIndex(i_LicenseNumber);
            if (m_Clients[indexOfClient].Vehicle.Engine is FuelEngine)
            {
                m_Clients[indexOfClient].Vehicle.Engine.addEnergy(i_FuelToAddInLiters);
            }
            else
            {
                throw new ArgumentException("The vehicle doesn't have a fuel engine");
            }
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_MinutesToCharge)
        {
            int indexOfClient = GetClientIndex(i_LicenseNumber);
            if (m_Clients[indexOfClient].Vehicle.Engine is ElectricEngine)
            {
                m_Clients[indexOfClient].Vehicle.Engine.addEnergy(i_MinutesToCharge);
            }
            else
            {
                throw new ArgumentException("The vehicle doesn't have an electric engine");
            }
        }

    }

}
