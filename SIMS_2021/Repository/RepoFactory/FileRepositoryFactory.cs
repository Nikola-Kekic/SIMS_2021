using SIMS_2021.Model;
using SIMS_2021.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Repository.RepoFactory
{
    public class FileRepositoryFactory : RepositoryFactory
    {
        public UserRepository UserRepository { get; set; }
        public DrugRepository DrugRepository { get; set; }
        public BillRepository BillRepository { get; set; }
        public IngredientRepository IngredientRepository { get; set; }
        public RejectedDrugRepository RejectedDrugRepository { get; set; }
        public override UserRepository GetUserRepository()
        {
            if (UserRepository == null)
                return new UserRepository();
            else
                return UserRepository;
        }
        public override DrugRepository GetDrugRepository()
        {
            if (DrugRepository == null)
                return new DrugRepository();
            else
                return DrugRepository;
        }
        public override BillRepository GetBillRepository()
        {
            if (BillRepository == null)
                return new BillRepository();
            else
                return BillRepository;
        }
        public override IngredientRepository GetIngredientRepository()
        {
            if (IngredientRepository == null)
                return new IngredientRepository();
            else
                return IngredientRepository;
        }

        public override RejectedDrugRepository GetRejectedDrugRepository()
        {
            if (RejectedDrugRepository == null)
                return new RejectedDrugRepository();
            else
                return RejectedDrugRepository;
        }


    }
}
