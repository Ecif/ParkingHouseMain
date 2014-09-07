using System;
using Entities.Main.Vehicles;

namespace Entities.Main.Clients
{
    /// <summary>
    /// Client entity. Contains vehicle reference.
    /// </summary>
    public class Client
    {
        public int Id { get; set; }
        //public int VehicleId { get; set; }  // parem, kui see oleks Vehicle obj ?hmmh
        public Car Vehicle { get; set; }  // parem, kui see oleks Vehicle obj ?hmmh
        public bool HasContract { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? DepartureTime { get; set; }
    }
}
