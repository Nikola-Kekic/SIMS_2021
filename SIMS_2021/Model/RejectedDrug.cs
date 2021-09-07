using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Model
{
    public class RejectedDrug : Entity
    {
        public Drug Drug { get; set; }
        public string Description { get; set; }
    }
}
