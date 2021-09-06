using SIMS_2021.DTOs;
using SIMS_2021.Model;
using SIMS_2021.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Controller
{
    public class IngredientsController
    {
        private IngredientsService _ingredientsService = new IngredientsService();

        public List<IngredientDTO> GetAll()
        {
            return _ingredientsService.GetAll();
        }
        public List<IngredientDTO> GetAllInDrug(string drugName)
        {
            return _ingredientsService.GetAllInDrug(drugName);
        }
    }
}
