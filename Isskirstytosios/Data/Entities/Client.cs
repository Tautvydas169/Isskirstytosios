using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Isskirstytosios.Entities
{
    public class Client
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Adress { get; set; }
        public string Company_name { get; set; }

        public Computerstore computerstore { get; set; }
        public int ComputerstoreId { get; set; }
    }
}
