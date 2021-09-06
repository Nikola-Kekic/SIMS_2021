using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Model
{
    public class Drug : Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public Dictionary<string, string> Ingredients { get; set; }   
        public bool Accepted { get; set; }
        public bool Deleted { get; set; }
    }
}
