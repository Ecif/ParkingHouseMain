﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Main.ParkingHouse
{
    public class ParkingHouse
    {
        public int Size { get; set; }
        public List<ParkingSpace> ParkingSpaces { get; set; }
    }
}
