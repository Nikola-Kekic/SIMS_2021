using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.DTOs
{
    public class Cart
    {
        public Cart()
        {
            DrugsAndQuantity = new Dictionary<long, int>();
        }
        public Dictionary<long, int> DrugsAndQuantity { get; set; }

    }
}
