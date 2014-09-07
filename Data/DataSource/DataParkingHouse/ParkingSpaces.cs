using System.Collections.Generic;
using Entities.Main.ParkingHouse;

namespace Data.DataSource.DataParkingHouse
{
    
    /// <summary>
    /// Contains parking space list.
    /// </summary>
    
    public class ParkingSpaces
    {
        public List<ParkingSpace> ParkingSpacesList { get; set; }

        public ParkingSpaces()
        {
            ParkingSpacesList = new List<ParkingSpace>();
        }
    }
}
