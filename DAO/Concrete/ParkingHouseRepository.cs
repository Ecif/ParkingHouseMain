using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Abstract;
using Data.DataSource.DataClients;
using Data.DataSource.DataParkingHouse;
using Entities.Main.ParkingHouse;

namespace DAO.Concrete
{
    public class ParkingHouseRepository : IParkingHouse
    {        
        private readonly ParkingSpaces _parkingSpaces;
        private readonly IClientsRepository _clientsRepo;

        public ParkingHouseRepository(IClientsRepository clientsRepo)
        {
            _clientsRepo = clientsRepo;            
            _parkingSpaces = new ParkingSpaces();
            CreateParkingSpots();
        }

        private void CreateParkingSpots()
        {
            const int parkingSpots = 500;
            const float premiumPerCentage = 0.1f;
            const int premiumSpots = (int) (parkingSpots*premiumPerCentage);
            for (int i = 0; i < parkingSpots; i++)
            {
                _parkingSpaces.ParkingSpacesList.Add(
                    new ParkingSpace
                    {
                        Id = i,
                        ClientId = null,
                        Length = 4,
                        Width = 2,
                        IsPremium = i % 10 == 0
                    });
            }
            ParkingLot.ParkingHouse.ParkingSpaces = _parkingSpaces.ParkingSpacesList;            
            ParkingLot.ParkingHouse.Size = _parkingSpaces.ParkingSpacesList.Count();
            var clients = _clientsRepo.GetClients();
            clients.AddCustomers();
        }

        public void AddCustomer()
        {
            _clientsRepo.GetClients().AddCustomers();
        }

        public void RemoveCustomer()
        {
            _clientsRepo.GetClients().RemoveCustomers();
        }

        public int GetFreeParkingSpaceCount()
        {
            return _parkingSpaces.ParkingSpacesList.Count(x => x.ClientId == null);
        }

        public int GetSize()
        {
            var size = ParkingLot.ParkingHouse.Size;
            return size;
        }

        public double GetTotalSum()
        {
            var statistics = _clientsRepo.GetClients().GetStatistics();
            var remainingSum = _clientsRepo.GetClients().GetPaymentServices().CalculateRemainingSum(_clientsRepo.GetClients().ClientList, _parkingSpaces.ParkingSpacesList);
            return statistics.Where(statistic => !statistic.Key.Equals("clientsCount")).Sum(statistic => statistic.Value) + remainingSum;
        }

        public int GetTotalClientsCount()
        {
            var statistics = _clientsRepo.GetClients().GetStatistics();
            return _parkingSpaces.ParkingSpacesList.Count(parkingSpace => parkingSpace.ClientId != null) + (int) statistics["clientsCount"];                       
        }
    }
}
