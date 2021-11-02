using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isskirstytosios.Entities
{
    public class Computerstore
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Owner_name { get; set; }
        public  Item  item{ get; set; }
        public int ItemId { get; set; }

    }
}
