using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Bussiness;
using Data.DataSource.DataParkingHouse;
using Data.DataSource.DataVehicles;
using Entities.Main.Clients;
using Entities.Main.Vehicles;

namespace Data.DataSource.DataClients
{
    /// <summary>
    /// Holds reference to bussiness rules classes. 
    /// Also contains statistics list.
    /// </summary>
    public class Clients
    {

        public IList<Client> ClientList { get; set; }
        private readonly Dictionary<string, double> _statistics = new Dictionary<string, double>();
        private bool IsFirstCall {get; set; }
        private readonly ServiceRestrictions _service;
        private readonly ServicePayment _servicePay;
        public Clients()
        {
            ClientList = new List<Client>();
            IsFirstCall = true;
            _service = new ServiceRestrictions();
            _servicePay = new ServicePayment();
            _statistics.Add("premiumSum" , 0);
            _statistics.Add("regularSum", 0);
            _statistics.Add("clientsCount", 0);
        }
        /// <summary>
        /// Payment services.
        /// </summary>
        /// <returns>Payment services reference</returns>
        public ServicePayment GetPaymentServices()
        {
            return _servicePay;
        }
        /// <summary>
        /// Statistics.
        /// </summary>
        /// <returns>Statistics reference</returns>
        public Dictionary<string, double> GetStatistics()
        {
            return _statistics;
        }
        /// <summary>
        /// Adds customers to parking house.
        /// </summary>
        public void AddCustomers()
        {
            if (_service.CheckParkingLotSize(this)) // Checks whether parking house is full.
            {
                var count = ClientList.Count;
                // Populate parking house with default clients list.
                if (IsFirstCall)
                {
                    foreach (var client in ClientList)
                    {
                        var parkingSpace = ParkingLot.ParkingHouse.ParkingSpaces.FirstOrDefault(x => x.ClientId == null);
                        if (parkingSpace != null)
                            parkingSpace.ClientId = client.Id;
                    }
                    IsFirstCall = false;
                }
                else
                {
                    // Creates new client every time client adding is called.
                    // Adds random vehicle Id to Client.
                    var rand = new Random();
                    var vehicleId = rand.Next(1, Cars.CarsList.Count+1);
                    var newClient = new Client
                    {
                        Id = count + 1,
                        EntryTime = DateTime.Now,
                        HasContract = count%10 == 0,
                        Vehicle = new Car {Id = vehicleId},
                        DepartureTime = null
                    };
                    if (_service.CheckCarSize(newClient)) // Checks whether car fits into parking house.
                    {
                        if (newClient.HasContract)
                            _service.AddPremium(newClient); // Add new premium customer.
                        else
                            _service.AddClient(newClient); // Add new regular customer.
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("Client tried to enter ------ ");
                        Console.WriteLine("The vehicle does not fit.");
                    }
                        
                }
            }
            else
            {
                Console.WriteLine();
                Console.Write("Client tried to enter ------ ");
                Console.WriteLine("No room in the parking house for current client." );
            }                  
        }
        /// <summary>
        /// Removes customer from parking house. 
        /// </summary>
        public void RemoveCustomers()
        {            
            var rand = new Random();
            var randCustomerNumber = rand.Next(ClientList.Count);
            var customer = ClientList.Skip(randCustomerNumber - 1).FirstOrDefault();  // Takes random customer from client list.        
            customer.DepartureTime = DateTime.Now;
            var removeFromParking = ParkingLot.ParkingHouse.ParkingSpaces.FirstOrDefault(x => x.ClientId == customer.Id);
            if (removeFromParking != null)
                removeFromParking.ClientId = null; // Removes client reference from parking space.
            // Method for creating check for customer and statistics.
            _servicePay.AddCheck(customer, _statistics);
            ClientList.Remove(customer); // Remove client from client list.
        }

    }
}
