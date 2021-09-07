using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.DTOs
{
    public class CartDTO
    {
        public long DrugId { get; set; }
        public string DrugName { get; set; }
        public int Quantity { get; set; }
    }
}
