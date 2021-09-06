using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.DTOs
{
    public class IngredientDTO
    {
        public IngredientDTO(string name, string description, int frequency)
        {
            Name = name;
            Description = description;
            Frequency = frequency;
        }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public int Frequency { get; set; }
    }
}
