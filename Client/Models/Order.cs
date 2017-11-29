using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    public class Order
    {
        public string CustomerId { get; set; }
        public int[] DishIdArray { get; set; }
        public bool Done { get; set; }
    }
}
