using System;
using System.Collections.Generic;
using System.Linq;
using Data.Bussiness;
using Data.DataSource.DataClients;
using Data.DataSource.DataParkingHouse;
using Entities.Main.Clients;
using Entities.Main.ParkingHouse;
using Entities.Main.Vehicles;
using Moq;
using NUnit.Framework;

namespace DataTests.Bussiness
{
    [TestFixture()]
    public class ServiceRestrictionsTests
    {
        [Test()]
        public void CheckParkingLotSizeTest()
        {            
            var parkingSpaces= GenerateParkingSpaces(500);
            ParkingLot.ParkingHouse.ParkingSpaces = parkingSpaces;
            var clientsList = GenerateClients(50);
            SetUpParkingHouse(parkingSpaces, clientsList);

            var clients = new Clients {ClientList = clientsList};
            clients.AddCustomers();

            clients.ClientList = GenerateClients(1);
            ParkingLot.ParkingHouse.ParkingSpaces = parkingSpaces;
            var obj = new ServiceRestrictions();

           var canMeEnter = obj.CheckParkingLotSize(clients);

           Assert.IsTrue(canMeEnter);   
        }

        [Test()]
        public void AddPremiumTest()
        {
            var parkingLot = GenerateParkingSpaces();
            var clients = GenerateClients();
        }

        [Test()]
        public void AddSimpleClientTest()
        {

            var restrictions = new ServiceRestrictions();
            ParkingLot.ParkingHouse.ParkingSpaces = GenerateParkingSpaces(1);

            var clients = new Clients();
            restrictions.CheckParkingLotSize(clients);
            restrictions.AddClient(new Client() {Id = 123}, false); 
            Assert.AreEqual(123, clients.ClientList.First().Id);    
        }

        [Test()]
        public void AddPremiumClientTest()
        {            
            var restrictions = new ServiceRestrictions();
            ParkingLot.ParkingHouse.ParkingSpaces = GenerateParkingSpaces(11);  

            var clients = new Clients();
            restrictions.CheckParkingLotSize(clients);
            restrictions.AddClient(new Client() { Id = 123, HasContract = true}, true);
            Assert.IsTrue(clients.ClientList.First().HasContract);    
            Assert.AreEqual(123, clients.ClientList.First().Id);    
        }

        [Test()]
        public void CheckCarSizeTest()
        {
            var restrictions = new ServiceRestrictions();
            ParkingLot.ParkingHouse.ParkingSpaces = GenerateParkingSpaces(11);  
            var clients = new Clients();
            restrictions.CheckParkingLotSize(clients);
            var client = new Client {Id = 123, HasContract = true, Vehicle = new Car
            {
                CarLength = 999, CarWidth = 1
            }};
            restrictions.AddClient(client);
            var val = restrictions.CheckCarSize(client);
            Assert.IsFalse(val);
        }

        #region Privates

        private void SetUpParkingHouse(List<ParkingSpace> parkingSpaces, IEnumerable<Client> clients)
        {
            foreach (var client in clients)
            {
                var parkingSpot = parkingSpaces.FirstOrDefault(s => s.ClientId == null);
                if (parkingSpot != null) parkingSpot.ClientId = client.Id;
            }
        }


        private List<ParkingSpace> GenerateParkingSpaces(int howMany = 20)
        {
            var parkingSpaces = new List<ParkingSpace>();
            for (var i = 0; i < howMany; i++)
            {
                parkingSpaces.Add(
                    new ParkingSpace
                    {
                        Id = i,
                        ClientId = null,
                        Length = 4,
                        Width = 2,
                        IsPremium = i % 10 == 0
                    });
            }

            return parkingSpaces;
        }

        private IList<Client> GenerateClients(int howMany = 20)
        {
            var clients = new List<Client>();
            for (var i = 0; i < howMany; i++)
            {
                clients.Add(
                    new Client
                    {
                        Id = i,
                        EntryTime = DateTime.Now,
                        HasContract = i % 10 == 0,
                        Vehicle = new Car {Id = 1},
                        DepartureTime = null
                    });
            }
            return clients;
        }

        #endregion
    }
}
