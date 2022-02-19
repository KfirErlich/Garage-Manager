using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Client
    {
        // $G$ CSS-003 (-3) Bad readonly members variable name (should be in the form of r_PascalCase).
        private readonly Vehicle m_Vehicle;
        private readonly string m_OwnerName;
        private readonly string m_OwnerPhoneNumber;
        private eStatus m_Status;

        public Client(string i_clientName, string i_clientPhoneNumber, Vehicle i_vehicle)
        {
            m_Status = eStatus.InRepair;
            m_Vehicle = i_vehicle;
            m_OwnerName = i_clientName;
            m_OwnerPhoneNumber = i_clientPhoneNumber;
        }

        public string ClientName
        {
            get { return m_OwnerName; }
        }

        public string ClientPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
        }
        public eStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }
        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }
    }

    
}
