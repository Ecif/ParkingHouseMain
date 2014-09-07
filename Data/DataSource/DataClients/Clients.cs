using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Bussiness;
using Data.DataSource.DataParkingHouse;
using Data.DataSource.DataVehicles;
using Entities.Main.Clients;

namespace Data.DataSource.DataClients
{
    public class Clients
    {

        public List<Client> ClientList { get; set; }
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

        public ServicePayment GetPaymentServices()
        {
            return _servicePay;
        }
        public Dictionary<string, double> GetStatistics()
        {
            return _statistics;
        }

        public void AddCustomers()
        {
            if (_service.CheckParkingLotSize(this))
            {
                var count = ClientList.Count;
                if (IsFirstCall)
                {
                    foreach (var client in ClientList)
                    {
                        var parkingSpace = ParkingLot.ParkingHouse.ParkingSpaces.FirstOrDefault(x => x.ClientId == null);
                        if (parkingSpace != null)
                            parkingSpace.ClientId = client.ClientId;
                    }
                    IsFirstCall = false;

                }
                else
                {
                    var rand = new Random();
                    var vehicleId = rand.Next(1, Cars.CarsList.Count+1);
                    var newClient = new Client
                    {
                        ClientId = count + 1,
                        EntryTime = DateTime.Now,
                        HasContract = count%10 == 0,
                        VehicleId = vehicleId,
                        DepartureTime = null
                    };
                    if (_service.CheckCarSize(newClient))
                    {
                        if (newClient.HasContract)
                            _service.AddPremium(newClient);
                        else
                            _service.AddClient(newClient);
                    }
                    else
                        Console.WriteLine("The vehicle does not fit.");
                }
            }
            else
            {
                Console.WriteLine("No room in the parking house for current client." );
            }                  
        }

        public void RemoveCustomers()
        {            
            var rand = new Random();
            var randCustomerNumber = rand.Next(ClientList.Count);
            var customer = ClientList.Skip(randCustomerNumber - 1).FirstOrDefault();            
            customer.DepartureTime = DateTime.Now;
            var removeFromParking = ParkingLot.ParkingHouse.ParkingSpaces.FirstOrDefault(x => x.ClientId == customer.ClientId);
            if (removeFromParking != null)
                removeFromParking.ClientId = null;
            _servicePay.AddCheck(customer, _statistics);
            ClientList.Remove(customer);
        }

    }
}
