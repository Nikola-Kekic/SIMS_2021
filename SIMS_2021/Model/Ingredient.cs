using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Model
{
    public class Ingredient : Entity
    {
        public Ingredient()
        {
        }

        public Ingredient(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
