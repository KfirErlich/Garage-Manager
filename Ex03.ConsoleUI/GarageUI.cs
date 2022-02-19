using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public enum eNumInfo
    {
        NumOfInfos= 7
    }
    class GarageUI
    {
        // $G$ DSN-005 (-3) This property should have been read-only.
        private Garage m_Garage = new Garage();

        public Garage GarageStorage
        {
            get { return m_Garage; }
            set { m_Garage = value; }
        }
        public static int PrintGarageMenu()
        {
            string userAction = null;
            int numberOfAction;
            Console.WriteLine(string.Format(@"
-------------------------------------------------
Welcome to the Garage!
Please  choose the number of action you would like to do: 
1. Add vehicle to the garage 
2. Show the list of the vehicles in the Garage
3. Change car status
4. Inflate the vehicle wheels to maximum
5. Fuel the vehicle
6. Charge vehicle 
7. Show full information of a vehicle 
8. Exit
-------------------------------------------------
"));

            userAction = Console.ReadLine();

            int.TryParse(userAction, out numberOfAction);
            return numberOfAction;
        }

        public void GarageMenu()
        {
            bool programRunning = true;
            
            while (programRunning)
            {
                // $G$ CSS-018 (-3) You should have used enumerations here.
                int numOfAction = PrintGarageMenu();
                switch (numOfAction)
                {
                    case 1:
                        Console.Clear();
                        string newVehicleLicenseNumber = GetLicenseNumber();
                        createNewClient(newVehicleLicenseNumber);
                        break;
                    case 2:
                        Console.Clear();
                        FilteredAllVehicles();
                        break;
                    case 3:
                        Console.Clear();
                        UpdateCarStatus();
                        break;
                    case 4:
                        Console.Clear();
                        String vehicleLicenseNumber = GetLicenseNumber();
                        InflateWheelsToMax(vehicleLicenseNumber);
                        break;
                    case 5:
                        Console.Clear();
                        vehicleLicenseNumber = GetLicenseNumber();
                        fuelVehicleOfClient(vehicleLicenseNumber);
                        break;
                    case 6:
                        Console.Clear();
                        vehicleLicenseNumber = GetLicenseNumber();
                        ChargeVehicleOfClient(vehicleLicenseNumber);
                        break;
                    case 7:
                        Console.Clear();
                        vehicleLicenseNumber = GetLicenseNumber();
                        PrintVehicleInfo(vehicleLicenseNumber);
                        break;
                    case 8:
                        Console.Clear();
                        programRunning = false;
                        Console.WriteLine("Bye Bye");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.WriteLine("You entered a wrong input, the input should be between 1-8");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                        
                }
            }
        }

        private void createNewClient(string i_VehicleLicenseNumber)
        {
            try
            {
                if (!(m_Garage.CheckIfLicenseNumberExists(i_VehicleLicenseNumber)))
                {
                    Client newClient;
                    newClient = GetNewClient(i_VehicleLicenseNumber);
                    m_Garage.Clients.Add(newClient);
                    Console.WriteLine("We have successfully entered you vehicle to our garage");

                }
                else
                {
                    throw new ArgumentException("The vehicle is already exist in the garage");
                }
            }
            catch (ArgumentException argException)
            {
                Console.WriteLine(argException.Message);
            }
            catch (ValueOutOfRangeException outOfRangeException)
            {
                Console.WriteLine(outOfRangeException.Message);
            }
            catch (FormatException formatException)
            {
                Console.WriteLine(formatException.Message);
            }
        }

        private Client GetNewClient(string i_VehicleLicenseNumber)
        {
            List<string> vehicleInfo = new List<string>();
            float maxEnergyOfCurrVehicle = 0;
            List<Wheel> Wheels = new List<Wheel>();
            bool isNotValid = true;
            string clientName = null;
            string clientPhoneNumber = null;
            string vehicleModel = null;
            Vehicle clientNewVehicle = null;
            int numOfWheels = 0, maxPressure = 0;
            eEnergyType energyType = 0;
            int typeOfVehicle = 0;
            float energyLeft = 0;

            clientName = GetClientName();
            clientPhoneNumber = GetClientPhoneNumber();

            while (isNotValid)
            {
                Console.Clear();
                try
                {
                    isNotValid = false;
                    typeOfVehicle = getVehicleType();
                    vehicleModel = GetVehicleModel();
                    Console.Clear();
                    GetInfoOfVehicle(typeOfVehicle, ref numOfWheels, ref maxPressure, ref energyType, ref vehicleInfo,
                        ref maxEnergyOfCurrVehicle);
                    energyLeft = GetEnergyLeft(i_VehicleLicenseNumber, maxEnergyOfCurrVehicle);
                    Console.Clear();
                    Wheels = GetWheelsInfo(numOfWheels, maxPressure);
                }
                catch (ValueOutOfRangeException outOfRangeException)
                {
                    Console.WriteLine(outOfRangeException.Message);
                    isNotValid = true;
                    Thread.Sleep(2000);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                    isNotValid = true;
                    Thread.Sleep(2000);
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                    isNotValid = true;
                    Thread.Sleep(2000);
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                    isNotValid = true;
                    Thread.Sleep(2000);
                }

            }

            clientNewVehicle = GarageStorage.VehicleFactory.CreateVehicle((eTypeOfVehicle)typeOfVehicle, i_VehicleLicenseNumber);
            clientNewVehicle.Wheels = Wheels;
            clientNewVehicle.Model = vehicleModel;
            clientNewVehicle.Engine.addEnergy(energyLeft);
            clientNewVehicle.SetUniqueInfo(vehicleInfo[0], vehicleInfo[1]);
            clientNewVehicle.NumOfWheels = (eNumOfWheels)numOfWheels;

            return new Client(clientName, clientPhoneNumber, clientNewVehicle);
        }

        private string GetClientName()
        {
            bool nameIsNotValid = true;
            Console.WriteLine("Please enter your name: ");
            string clientName = Console.ReadLine();

            while (nameIsNotValid)
            {
                if (checkIfStringContainsLettersOnly(clientName))
                {
                    nameIsNotValid = false;
                    break;
                }

                Console.WriteLine("The input you entered is not valid. Please enter your choice again: ");
                clientName = Console.ReadLine();
            }
            Console.Clear();
            return clientName;
        }
        private string GetClientPhoneNumber()
        {
            bool numberIsNotValid = true;
            Console.WriteLine("Please enter your phone number: ");
            string clientPhoneNumber = Console.ReadLine();
            while (numberIsNotValid)
            {
                if (checkIfStringContainsDigitsOnly(clientPhoneNumber))
                {
                    numberIsNotValid = false;
                    break;
                }

                Console.WriteLine("The input you entered is not valid. Please enter your choice again: ");
                clientPhoneNumber = Console.ReadLine();
            }

            Console.Clear();
            return clientPhoneNumber;
        }

        private string GetLicenseNumber()
        {
            Console.WriteLine("Please enter the license number: ");
            string newVehicleLicenseNumber = Console.ReadLine();
            Console.Clear();

            return newVehicleLicenseNumber;
        }

        private void PrintVehicleInfo(string i_LicenseNumber)
        {
            int index = 0;
            try
            {
                if (m_Garage.CheckIfLicenseNumberExists(i_LicenseNumber))
                {
                    index = m_Garage.GetClientIndex(i_LicenseNumber);
                    PrintVehicle(m_Garage.Clients[index]);
                }
                else
                {
                    throw new ArgumentException("The vehicle doesn't exist in the garage");
                }
            }
            catch (ArgumentException argException)
            {
                Console.WriteLine(argException.Message);
            }


        }

        // $G$ DSN-999 (-3) You should override ToString in vehicle class.
        private void PrintVehicle(Client i_Client)
        {
            Console.WriteLine(string.Format(
@"License number: {0}
Model: {1}
Owner name: {2}
Status: {3}
Type of vehicle: {4}", i_Client.Vehicle.LicenseNumber, i_Client.Vehicle.Model, i_Client.ClientName, i_Client.Status, i_Client.Vehicle.Type));

            if (i_Client.Vehicle.Engine is ElectricEngine)
            {
                Console.WriteLine(string.Format(@"Their are {0} minutes left in the vehicle", i_Client.Vehicle.Engine.GetCurrentCapcity() * 60));
            }
            else
            {
                Console.WriteLine(string.Format(@"Their are {0} liters left in the vehicle", i_Client.Vehicle.Engine.GetCurrentCapcity()));
            }

            string uniqueInfo = getUniqueInfoOfVehicle(i_Client.Vehicle);
            Console.WriteLine(uniqueInfo);
            printWheelsOfClientVehicle(i_Client);


        }

        private string getUniqueInfoOfVehicle(Vehicle i_CurrVehicle)
        {
            string uniqueInfo;
            uniqueInfo = i_CurrVehicle.getUniqueInfoFromGarage();

            return uniqueInfo;
        }

        // $G$ DSN-001 (-10) The UI must not know specific types and their properties concretely! It means that when adding a new type you'll have to change the code here too!
        private int getVehicleType()
        {
            int typeOfVehicle = 0;
            Console.WriteLine("Please choose the type of the vehicle you would like to insert to our garage: ");
            Console.WriteLine(string.Format(@"
-------------------------------------------------
1. Fuel Car
2. Electric Car
3. Fuel Motorcycle
4. Electric Motorcycle
5. Truck
-------------------------------------------------
"));

            if (int.TryParse(Console.ReadLine(), out typeOfVehicle))
            {
                if (typeOfVehicle < 1 || typeOfVehicle > 5)
                {
                    throw new ValueOutOfRangeException(5, 1, "You entered invalid choice");
                }
            }
            else
            {
                throw new FormatException("You entered a wrong input format");
            }
            Console.Clear();

            return typeOfVehicle;
        }

        private string GetVehicleModel()
        {
            Console.WriteLine("Please insert Vehicle model: ");
            string vehicleModel = Console.ReadLine();

            return vehicleModel;
        }
        private void ChargeVehicleOfClient(string i_VehicleLicenseNumber)
        {
            try
            {
                if (m_Garage.CheckIfLicenseNumberExists(i_VehicleLicenseNumber))
                {
                    float electricEnergyToAdd = getElectricEnergyToAdd(m_Garage.GetClientIndex(i_VehicleLicenseNumber));
                    m_Garage.ChargeVehicle(i_VehicleLicenseNumber, electricEnergyToAdd);
                    Console.WriteLine("We have successfully charged your vehicle");
                }
                else
                {
                    throw new ArgumentException("The vehicle doesn't exist in the garage");
                }
            }
            catch (ArgumentException argException)
            {
                Console.WriteLine(argException.Message);
            }
            catch (ValueOutOfRangeException outOfRangeException)
            {
                Console.WriteLine(outOfRangeException.Message);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        private void fuelVehicleOfClient(string i_VehicleLicenseNumber)
        {
            try
            {
                if (m_Garage.CheckIfLicenseNumberExists(i_VehicleLicenseNumber))
                {
                    float fuelToAdd = getFuelToAdd(m_Garage.GetClientIndex(i_VehicleLicenseNumber));
                    m_Garage.FuelVehicle(i_VehicleLicenseNumber, fuelToAdd);
                    Console.WriteLine("We have successfully fueled your vehicle");
                }
                else
                {
                    throw new ArgumentException("The vehicle doesn't exist in the garage");
                }
            }
            catch (ArgumentException argException)
            {
                Console.WriteLine(argException.Message);
            }
            catch (ValueOutOfRangeException outOfRangeException)
            {
                Console.WriteLine(outOfRangeException.Message);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        private float getElectricEnergyToAdd(int i_Index)
        {
            float electricEnergyToAdd;
            ElectricEngine currEngine = m_Garage.Clients[i_Index].Vehicle.Engine as ElectricEngine;
            if (m_Garage.Clients[i_Index].Vehicle.Engine is FuelEngine)
            {
                throw new ArgumentException("The vehicle's engine is not an electric engine");
            }
            Console.WriteLine("Please enter the amount of energy you would like to add: ");

            if (!float.TryParse(Console.ReadLine(), out electricEnergyToAdd))
            {
                throw new FormatException("The input you entered is not valid");
            }
           
            if(electricEnergyToAdd <= 0 || electricEnergyToAdd > (currEngine.MaxBatteryLifeInHours - currEngine.BatteryLeftInHours))
            {
                throw new ValueOutOfRangeException(currEngine.MaxBatteryLifeInHours - currEngine.BatteryLeftInHours, 0,
                    "The amount of energy you entered is invalid");
            }

            return electricEnergyToAdd;
        }

        private float getFuelToAdd(int i_Index)
        {
            float fuelToAdd;
            FuelEngine currEngine = m_Garage.Clients[i_Index].Vehicle.Engine as FuelEngine;
            if (m_Garage.Clients[i_Index].Vehicle.Engine is ElectricEngine)
            {
                throw new ArgumentException("The vehicle's engine is not a fuel engine");
            }
            Console.WriteLine("Please enter the amount of fuel you would like to add: ");

            if (!float.TryParse(Console.ReadLine(), out fuelToAdd))
            {
                throw new FormatException("The input you entered is not valid");
            }
            if (fuelToAdd <= 0 || fuelToAdd > (currEngine.MaxFuelAmount - currEngine.CurrentFuelAmount))
            {
                throw new ValueOutOfRangeException(currEngine.MaxFuelAmount - currEngine.CurrentFuelAmount, 0,
                    "The amount of fuel you entered is invalid");
            }
            return fuelToAdd;
        }

        private void FilteredAllVehicles()
        {
            string userAction;
            int numberOfAction;
            Console.WriteLine(string.Format(

@"
-----------------------------------------
Please choose one out of four options:
1. Show all vehicles license plate in the garage.
2. Show all the vehicles license plate that have already been fixed but not paid.
3. Show all the vehicles license plate that currently under repair. 
4. Show all the vehicles license plate that are already been paid. 
-----------------------------------------
"));
            userAction = Console.ReadLine();
            int.TryParse(userAction, out numberOfAction);
            PrintAllChosenVehicles(numberOfAction);
        }

        private void UpdateCarStatus()
        {
            int indexOfClient = 0;
            eStatus newStatus = 0;
            bool isExist = true;
            Console.WriteLine("Please enter the license number of the car you would like to change her status");
            string licenseNumber = Console.ReadLine();
            try
            {
                if (m_Garage.CheckIfLicenseNumberExists(licenseNumber))
                {
                    indexOfClient = m_Garage.GetClientIndex(licenseNumber);
                    Console.WriteLine(string.Format(@"
-----------------------------------------
To which status would you like to change your vehicle?
1. Fixed
2. In repair 
3. Paid
-----------------------------------------"));
                    eStatus.TryParse(Console.ReadLine(), out newStatus);
                    m_Garage.Clients[indexOfClient].Status = newStatus;
                    Console.Clear();
                    Console.WriteLine("We have updated your vehicle status");
                }
                else
                {
                    throw new ArgumentException("The license number doesn't exists in the garage");
                }
            }
            catch (ArgumentException argException)
            {
                Console.WriteLine(argException.Message);
            }
        }

        private void PrintAllChosenVehicles(int i_NumberOfAction)
        {
            Console.Clear();
            switch (i_NumberOfAction)
            {
                case 1:
                    foreach (Client client in m_Garage.Clients)
                    {
                        Console.WriteLine(client.Vehicle.LicenseNumber);
                    }

                    break;
                case 2:
                    foreach (Client client in m_Garage.Clients)
                    {
                        if (client.Status is eStatus.Fixed)
                        {
                            Console.WriteLine(client.Vehicle.LicenseNumber);
                        }
                    }

                    break;
                case 3:
                    foreach (Client client in m_Garage.Clients)
                    {
                        if (client.Status is eStatus.InRepair)
                        {
                            Console.WriteLine(client.Vehicle.LicenseNumber);
                        }
                    }

                    break;
                case 4:
                    foreach (Client client in m_Garage.Clients)
                    {
                        if (client.Status is eStatus.Paid)
                        {
                            Console.WriteLine(client.Vehicle.LicenseNumber);
                        }
                    }
                    break;
            }
        }

        private float GetEnergyLeft(string i_VehicleLicenseNumber, float i_MaxEnergy)
        {
            Console.WriteLine("Please insert how much energy left:  ");
            int clientIndex = m_Garage.GetClientIndex(i_VehicleLicenseNumber);

            if (float.TryParse(Console.ReadLine(), out float energyLeft))
            {
                if (energyLeft < 0 || energyLeft > (i_MaxEnergy))
                {
                    throw new ValueOutOfRangeException(i_MaxEnergy, 0, "The input is invalid");
                }
            }
            else
            {
                throw new FormatException("The input format is invalid");
            }
            return energyLeft;
        }

        private void GetInfoOfVehicle(int i_TypeOfVehicle, ref int io_NumOfWheels, ref int io_MaxPressure, ref eEnergyType io_EnergyType, ref List<string> i_UniqueInfo, ref float io_MaxEnergy)
        {
            switch (i_TypeOfVehicle)
            {
                case 1:
                    io_NumOfWheels = (int)eNumOfWheels.CarWheels;
                    io_MaxPressure = (int) eMaxPressure.CarMaxPressure;
                    io_EnergyType =  eEnergyType.Octan95;
                    i_UniqueInfo.Add(GetNumOfDoors());
                    i_UniqueInfo.Add(GetColorOfCar());
                    io_MaxEnergy = (float) eFullCapcity.FuelCar/10;

                    break;
                case 2:
                    io_NumOfWheels = (int)eNumOfWheels.CarWheels;
                    io_MaxPressure = (int)eMaxPressure.CarMaxPressure;
                    io_EnergyType = eEnergyType.Electricity;
                    i_UniqueInfo.Add(GetNumOfDoors());
                    i_UniqueInfo.Add(GetColorOfCar());
                    io_MaxEnergy = (float)eFullCapcity.ElectricCar / 10;

                    break;
                case 3:
                    io_NumOfWheels = (int)eNumOfWheels.MotorcycleWheels;
                    io_MaxPressure = (int)eMaxPressure.MotorcyclePressure;
                    io_EnergyType = eEnergyType.Ocatn98;
                    i_UniqueInfo.Add(GetLicenseType());
                    i_UniqueInfo.Add(GetEngineVolume());
                    io_MaxEnergy = (float)eFullCapcity.FuelMotorcycle / 10;

                    break;
                case 4:
                    io_NumOfWheels = (int)eNumOfWheels.MotorcycleWheels;
                    io_MaxPressure = (int)eMaxPressure.MotorcyclePressure;
                    io_EnergyType = eEnergyType.Electricity;
                    i_UniqueInfo.Add(GetLicenseType());
                    i_UniqueInfo.Add(GetEngineVolume());
                    io_MaxEnergy = (float)eFullCapcity.ElectricMotorcycle / 10;

                    break;
                case 5:
                    io_NumOfWheels = (int)eNumOfWheels.TruckWheels;
                    io_MaxPressure = (int)eMaxPressure.TruckMaxPressure;
                    io_EnergyType = eEnergyType.Soler;
                    i_UniqueInfo.Add(GetAbleToDeliverCold());
                    i_UniqueInfo.Add(GetCargoVolume());
                    io_MaxEnergy = (float)eFullCapcity.FuelTruck / 10;

                    break;
            }
        }

        private string GetNumOfDoors()
        {
            Console.WriteLine(string.Format(@"Please choose the doors number in your car:
1. Two
2. Three
3. Four
4. Five"));
            string numOfDoors = Console.ReadLine();
            Console.Clear();
            return numOfDoors;
        }

        private string GetColorOfCar()
        {
            Console.WriteLine(string.Format(@"Please choose the color number of your car:
1. Red
2. White
3. Black
4. Blue"));
            string colorOfCar = Console.ReadLine();
            Console.Clear();
            return colorOfCar;
        }
        private string GetLicenseType()
        {
            Console.WriteLine(string.Format(
@"Please choose the type of your License: 
1.A
2.A2
3.AA
4.B
"));
            string licenseType = Console.ReadLine();
            Console.Clear();
            return licenseType;
        }

        private string GetEngineVolume()
        {
            Console.WriteLine("Please insert your engine volume:  ");
            string engineVolume = Console.ReadLine();
            Console.Clear();
            return engineVolume;
        }

        private string GetAbleToDeliverCold()
        {
            Console.WriteLine("Is the truck able to deliver cold? (y/n) ");
            string isAbleToDeliverCold = Console.ReadLine();
            Console.Clear();
            return isAbleToDeliverCold;
        }

        private string GetCargoVolume()
        {
            Console.WriteLine("Please insert your cargo volume: ");
            string cargoVolume = Console.ReadLine();
            Console.Clear();
            return cargoVolume;
        }

    
        private List<Wheel> GetWheelsInfo(int i_NumOfWheels, int i_MaxPressure)
        {
            List<Wheel> newVehicleWheels = new List<Wheel>();
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                Console.Clear();
                Console.WriteLine(string.Format(@"Wheel number {0} ", i + 1));
                Console.WriteLine("---------------------");
                Wheel wheel = new Wheel();
                wheel.MaxPressure = i_MaxPressure;
                Console.Write("Wheel Manufacturer: ");
                wheel.NameOfManufacturer = Console.ReadLine();
                wheel.Pressure = getWheelCurrentPressure(i_MaxPressure);
                newVehicleWheels.Add(wheel);
            }
            Console.Clear();
            return newVehicleWheels;
        }

        private float getWheelCurrentPressure(float i_maxPressure) // Need to check if its a legal number??
        {
            float currPressure = 0;
            string currPressureString = null;
            bool currPressureIsNotValid = true;
            Console.Write("Wheel current pressure: ");
            currPressureString = Console.ReadLine();
            while (currPressureIsNotValid)
            {
                if (checkIfStringContainsDigitsOnly(currPressureString))
                {
                    float.TryParse(currPressureString, out currPressure);
                    if (currPressure <= i_maxPressure && currPressure >= 0)
                    {
                        currPressureIsNotValid = false;
                        break;
                    }
                }
                Console.WriteLine("The current pressure is not valid, please enter again");
                currPressureString = Console.ReadLine();
                float.TryParse(currPressureString, out currPressure);
            }
            return currPressure;
        }
        private void printWheelsOfClientVehicle(Client i_Client)
        {
            int indexOfWheel = 1;
            List<Wheel> currWheels = i_Client.Vehicle.Wheels;
            foreach (Wheel wheel in currWheels)
            {
                string wheelPressure = (wheel.Pressure).ToString();
                string wheelMaxPressure = (wheel.MaxPressure).ToString();
                Console.WriteLine("\nWheel number:{0} ", indexOfWheel);
                Console.WriteLine("---------------------");
                Console.WriteLine(String.Format(@"
The current pressure of this wheel is: {0}
The max pressure of this wheel is: {1}
The manufacturer of this wheel is: {2}", wheel.Pressure, wheel.MaxPressure, wheel.NameOfManufacturer));
                indexOfWheel++;
            }
        }

        private void InflateWheelsToMax(string i_LicenseNumber)
        {
            try
            {
                if (m_Garage.CheckIfLicenseNumberExists(i_LicenseNumber))
                {
                    int indexOfVehicle = 0;
                    indexOfVehicle = m_Garage.GetClientIndex(i_LicenseNumber);
                    m_Garage.Clients[indexOfVehicle].Vehicle.InflateWheelsToMax();
                    Console.WriteLine("We have successfully inflate your wheels");
                }
                else
                {
                    throw new ArgumentException("The vehicle doesn't exist in the garage");
                }
            }
            catch (ArgumentException argException)
            {
                Console.WriteLine(argException.Message);
            }
            catch (ValueOutOfRangeException outOfRangeException)
            {
                Console.WriteLine(outOfRangeException.Message);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }

        private bool checkIfStringContainsLettersOnly(string i_String)
        {
            bool stringOnlyDigits = true;

            foreach (char currChar in i_String)
            {
                if (!char.IsLetter(currChar) && currChar != ' ')
                {
                    stringOnlyDigits = false;
                    break;
                }
            }
            return stringOnlyDigits;
        }

        private bool checkIfStringContainsDigitsOnly(string i_String)
        {
            bool stringOnlyDigits = true;

            foreach (char currChar in i_String)
            {
                if (!char.IsDigit(currChar))
                {
                    stringOnlyDigits = false;
                    break;
                }
            }
            return stringOnlyDigits;
        }
    }
}
