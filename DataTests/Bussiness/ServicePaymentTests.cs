using System;
using System.Collections.Generic;
using Data.Bussiness;
using Entities.Main.Clients;
using Entities.Main.ParkingHouse;
using Entities.Main.Vehicles;
using NUnit.Framework;

namespace DataTests.Bussiness
{
    [TestFixture()]
    public class ServicePaymentTests
    {
        [Test()]
        public void AddCheckTest()
        {
            var dict = new Dictionary<string, double>
            {
                {"premiumSum", 0.0f},
                {"regularSum", 0.0f},
                {"clientsCount", 0}
            };

            var servicePayment = new ServicePayment();
            servicePayment.AddCheck(new Client { DepartureTime = DateTime.Now, EntryTime = DateTime.Now, Id = 1},dict );
            Assert.AreEqual(1.0d, dict["clientsCount"]);
        }

        [Test()]
        public void CalculateRemainingSumTest()
        {
            var servicePayment = new ServicePayment();
            var clients = GenerateClients(2);
            var spaces = GenerateParkingSpaces(2);
            var sum = servicePayment.CalculateRemainingSum(clients, spaces);
            Assert.AreEqual(4.0d, sum);

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
                        DepartureTime = DateTime.Now,
                        HasContract = false,
                        Vehicle = new Car { Id = 1 }
                    });
            }
            return clients;
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
                        ClientId = i,
                        Length = 4,
                        Width = 2,
                        IsPremium = i % 10 == 0
                    });
            }

            return parkingSpaces;
        }
    }
}
