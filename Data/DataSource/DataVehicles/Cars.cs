using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Main.Vehicles;

namespace Data.DataSource.DataVehicles
{
    public static class Cars
    {
        public static List<Car> CarsList { get; set; }
        static Cars()
        {
            CarsList = new List<Car>();
        }

    }
}
