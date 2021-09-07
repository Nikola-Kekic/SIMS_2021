using SIMS_2021.Model;
using SIMS_2021.Repository.RepoFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Service
{
    public class DrugService
    {
        public RepositoryFactory _repositoryFactory;

        public DrugService()
        {
            _repositoryFactory = new FileRepositoryFactory();
        }

        public List<Drug> GetAll()
        {
            List<Drug> drugs = _repositoryFactory.GetDrugRepository().GetAll();

            return drugs;
        }
        public Drug GetOne(long Id)
        {
            Drug drug = _repositoryFactory.GetDrugRepository().GetOne(Id);

            return drug;
        }

        public void Delete(Drug drug)
        {
            drug.Deleted = true;
            _repositoryFactory.GetDrugRepository().Update(drug);
        }

        public bool Add(Drug drug)
        {
            Entity entity = null;
            List<Drug> drugs = _repositoryFactory.GetDrugRepository().GetAll();

            List<Ingredient> existingIngredients = _repositoryFactory.GetIngredientRepository().GetAll();

            Drug existing = drugs.Where(x => x.Code.Equals(drug.Code)).FirstOrDefault();

            if (existing == null)
            {
                foreach (KeyValuePair<string, string> ingredient in drug.Ingredients)
                {
                    // if the ingredientdoesn't exist in the database, add it
                    if(!existingIngredients.Any(x => x.Name.Equals(ingredient.Key))) {
                        _repositoryFactory.GetIngredientRepository().Add(new Ingredient()
                        {
                            Name = ingredient.Key,
                            Description = ingredient.Value
                        });
                    }
                }

                entity = _repositoryFactory.GetDrugRepository().Add(drug);
            }
            if (entity == null)
                return false;

            return true;
        }
        public void Update(long id, bool isChecked) 
        {
           Drug drug = _repositoryFactory.GetDrugRepository().GetOne(id);
           drug.Accepted = isChecked;
           _repositoryFactory.GetDrugRepository().Update(drug); 
        }
    }
}
