using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Model
{
    public class Bill : Entity
    {
        public int Code { get; set; }
        public string Pharmacist { get; set; }
        public string DateAndTime { get; set; }
        public Dictionary<long, int> DrugsAndQuantity { get; set; }
        public float TotalPrice { get; set; }
        public long UserId { get; set; }
    }
}
