using SIMS_2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.DTOs
{
    public class BillDTO
    {
        public int Code { get; set; }
        public string Pharmacist { get; set; }
        public string DateAndTime { get; set; }
        public Dictionary<Drug, int> DrugsAndQuantity { get; set; }
        public float TotalPrice { get; set; }
    }
}
