using SIMS_2021.Model;
using SIMS_2021.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Repository.RepoFactory
{
    public abstract class RepositoryFactory
    {
        public abstract BillRepository GetBillRepository();
        public abstract DrugRepository GetDrugRepository();
        public abstract IngredientRepository GetIngredientRepository();
        public abstract UserRepository GetUserRepository();
        public abstract RejectedDrugRepository GetRejectedDrugRepository();

    }
}
