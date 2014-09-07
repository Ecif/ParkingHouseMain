using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Main.ParkingHouse
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public bool IsPremium { get; set; }
    }
}
