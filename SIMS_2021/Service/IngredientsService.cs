using SIMS_2021.DTOs;
using SIMS_2021.Model;
using SIMS_2021.Repository.RepoFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Service
{
    public class IngredientsService
    {
        private RepositoryFactory _repositoryFactory;

        public IngredientsService()
        {
            _repositoryFactory = new FileRepositoryFactory();
        }

        public List<IngredientDTO> GetAll()
        {
            List<Ingredient> ingredients = _repositoryFactory.GetIngredientRepository().GetAll();
            List<IngredientDTO> ingredientDTOs = new List<IngredientDTO>();

            foreach(var ingredient in ingredients)
            {
                IngredientDTO ingredientDTO = new IngredientDTO(ingredient.Name, 
                    ingredient.Description, 
                    GetFrequencyInDrugs(ingredient.Name));
                ingredientDTOs.Add(ingredientDTO);
            }

            return ingredientDTOs;
        }

        private int GetFrequencyInDrugs(string ingredientName)
        {
            List<Drug> drugs = _repositoryFactory.GetDrugRepository().GetAll();
            return drugs.Where(x => x.Ingredients.Keys.Contains(ingredientName)).Count();
        }

        internal List<IngredientDTO> GetAllInDrug(string drugName)
        {
            List<Drug> drugs = _repositoryFactory.GetDrugRepository().FindByName(drugName.Trim());
            List<IngredientDTO> ingredientDTOs = new List<IngredientDTO>();

            foreach(Drug drug in drugs)
            {
                foreach (KeyValuePair<string, string> ingredient in drug.Ingredients)
                {
                    ingredientDTOs.Add(new IngredientDTO(ingredient.Key, ingredient.Value,
                        GetFrequencyInDrugs(ingredient.Key)));
                }
            }

            return ingredientDTOs;
        }
    }
}
