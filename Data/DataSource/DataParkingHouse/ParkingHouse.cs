using Entities.Main.ParkingHouse;

namespace Data.DataSource.DataParkingHouse
{
    /// <summary>
    /// Contains parking house. 
    /// </summary>
    public static  class ParkingLot
    {
        public static  ParkingHouse ParkingHouse{ get; set; }
        static ParkingLot()
        {
            ParkingHouse = new ParkingHouse();
        }
    }
}
