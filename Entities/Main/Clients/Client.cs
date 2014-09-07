using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Main.Clients
{
    public class Client
    {
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public bool HasContract { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? DepartureTime { get; set; }
    }
}
