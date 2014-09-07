using System;

namespace Entities.Main.Clients
{
    /// <summary>
    /// Client entity. Contains vehicle reference.
    /// </summary>
    public class Client
    {
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public bool HasContract { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? DepartureTime { get; set; }
    }
}
