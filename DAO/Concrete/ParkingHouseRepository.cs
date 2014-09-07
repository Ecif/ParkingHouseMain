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
    /// <summary>
    /// Parking house and parking spaces repository
    /// </summary>
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
        /// <summary>
        /// Creates default parking spaces list.
        /// </summary>
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
        /// <summary>
        /// Add customer to DB
        /// </summary>
        public void AddCustomer()
        {
            _clientsRepo.GetClients().AddCustomers();
        }
        /// <summary>
        /// Remove customer from DB
        /// </summary>
        public void RemoveCustomer()
        {
            _clientsRepo.GetClients().RemoveCustomers();
        }
        /// <summary>
        /// Free parking spaces count.
        /// </summary>
        /// <returns>Parking spots count.</returns>
        public int GetFreeParkingSpaceCount()
        {
            return _parkingSpaces.ParkingSpacesList.Count(x => x.ClientId == null);
        }
        /// <summary>
        /// Size of Parking house.
        /// </summary>
        /// <returns>Parking house size.</returns>
        public int GetSize()
        {
            var size = ParkingLot.ParkingHouse.Size;
            return size;
        }
        /// <summary>
        /// Total sum of simulation.
        /// </summary>
        /// <returns>Total sum of simulation</returns>
        public double GetTotalSum()
        {
            var statistics = _clientsRepo.GetClients().GetStatistics();
            var remainingSum = _clientsRepo.GetClients().GetPaymentServices().CalculateRemainingSum(_clientsRepo.GetClients().ClientList, _parkingSpaces.ParkingSpacesList);
            return statistics.Where(statistic => !statistic.Key.Equals("clientsCount")).Sum(statistic => statistic.Value) + remainingSum;
        }
        /// <summary>
        /// Visited client count.
        /// </summary>
        /// <returns>Visited client count.</returns>
        public int GetTotalClientsCount()
        {
            var statistics = _clientsRepo.GetClients().GetStatistics();
            return _parkingSpaces.ParkingSpacesList.Count(parkingSpace => parkingSpace.ClientId != null) + (int) statistics["clientsCount"];                       
        }
    }
}
