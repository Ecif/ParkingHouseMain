using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Main.ParkingHouse;

namespace Data.DataSource.DataParkingHouse
{
    public static  class ParkingLot
    {
        public static  ParkingHouse ParkingHouse{ get; set; }
        static ParkingLot()
        {
            ParkingHouse = new ParkingHouse();
        }
    }
}
