using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isskirstytosios.Entities
{
    public class Item
    {
        public int No { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string description { get; set; }
        public double Rating { get; set; }

        public double Weight { get; set; }
    }
}
