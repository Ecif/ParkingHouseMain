using System.Collections.Generic;
using Entities.Main.Vehicles;

namespace Data.DataSource.DataVehicles
{
    /// <summary>
    /// Contains car entity list.
    /// </summary>
    public static class Cars
    {
        public static List<Car> CarsList { get; set; }
        static Cars()
        {
            CarsList = new List<Car>();
        }

    }
}
