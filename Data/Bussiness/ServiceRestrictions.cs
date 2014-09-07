using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataSource.DataClients;
using Data.DataSource.DataParkingHouse;
using Data.DataSource.DataVehicles;
using Entities.Main.Clients;
using Entities.Main.ParkingHouse;

namespace Data.Bussiness
{
    /// <summary>
    /// Services for bussiness rules.
    /// </summary>
    public class ServiceRestrictions
    {
        private const bool HasRightToEnter = true;

        private List<Client> _clientsList;
        private List<ParkingSpace> _parkingSpaces;
        private ParkingHouse _parkingHouse;
        /// <summary>
        /// Checks if parking house is full.
        /// </summary>
        /// <param name="clients">Clients list.</param>
        /// <returns>Is enough room for parking?</returns>
        public bool CheckParkingLotSize(Clients clients)
        {
            _clientsList = clients.ClientList;
            _parkingHouse = ParkingLot.ParkingHouse;
            _parkingSpaces = ParkingLot.ParkingHouse.ParkingSpaces;
            var roomLeft = _parkingSpaces.Count(x => x.ClientId != null);
            if (roomLeft == 500)
                return false;                     
            return HasRightToEnter;
        }
        /// <summary>
        /// Adds premium customer.
        /// </summary>
        /// <param name="newClient">New client</param>
        public void AddPremium(Client newClient)
        {
            var isPremiumAvailable = ParkingLot.ParkingHouse.ParkingSpaces.FirstOrDefault(x => x.ClientId == null && x.IsPremium) != null; // Checks whether is possible to park to premium space.
            AddClient(newClient, isPremiumAvailable);      // Adds customer throgh regular customer method.   
        }
        /// <summary>
        /// Adds regular customer.
        /// </summary>
        /// <param name="newClient">New client</param>
        /// <param name="toPremiumSpot">Is it possible to park to premium reserved parking space?</param>
        public void AddClient(Client newClient, bool toPremiumSpot = false)
        {
            _clientsList.Add(newClient);
            if (toPremiumSpot)
            {
                var firstOrDefault = ParkingLot.ParkingHouse.ParkingSpaces.FirstOrDefault(x => x.ClientId == null && x.IsPremium);
                if (firstOrDefault != null)
                    firstOrDefault.ClientId = newClient.ClientId;
            }
            else
            {
                var firstFreeSpot = ParkingLot.ParkingHouse.ParkingSpaces.FirstOrDefault(x => x.ClientId == null && !x.IsPremium);
                if (firstFreeSpot != null)
                {
                    firstFreeSpot.ClientId = newClient.ClientId;
                }
            }
            Console.WriteLine();
            Console.WriteLine("             -----ENTERED----");
            Console.WriteLine("Client entered the parking house. (Premium : {0})" , newClient.HasContract ? "Yes" : "No");
            Console.WriteLine();
        }
        /// <summary>
        /// Checks if car fits to any parking space.
        /// </summary>
        /// <param name="newClient">Owner of the car.</param>
        /// <returns>Is it possible to park?</returns>
        public bool CheckCarSize(Client newClient)
        {
            var vehicleId = newClient.VehicleId;
            var carsList = Cars.CarsList;
            var carType = carsList.FirstOrDefault(x => x.Id == vehicleId);
            return _parkingSpaces.Any(parkingSpace => carType.CarLength <= parkingSpace.Length && carType.CarWidth <= parkingSpace.Width);
        }
    }
}
