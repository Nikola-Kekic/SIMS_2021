using SIMS_2021.Model;
using SIMS_2021.Repository.RepoFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Service
{
    public class RejectedService
    {
        private RepositoryFactory _repositoryFactory;

        public RejectedService()
        {
            _repositoryFactory = new FileRepositoryFactory();
        }
        public void Save(long id, string description) 
        {

            List<RejectedDrug> rejecteds = _repositoryFactory.GetRejectedDrugRepository().GetAll();
            RejectedDrug rejected = rejecteds.Where(x => x.Drug.Id.Equals(id)).FirstOrDefault();
          
            if (rejected == null)
            {
                RejectedDrug rejectedDrug = new RejectedDrug();
                List<Drug> drugs = _repositoryFactory.GetDrugRepository().GetAll();
                Drug drug = drugs.Where(x => x.Id.Equals(id)).FirstOrDefault();
                rejectedDrug.Drug = drug;
                rejectedDrug.Description = description;
                _repositoryFactory.GetRejectedDrugRepository().Add(rejectedDrug);
            }
            else
            {
                rejected.Description = description;
                _repositoryFactory.GetRejectedDrugRepository().Update(rejected);
            }
        }
        public RejectedDrug GetOne(long id)
        {
            RejectedDrug rejected = _repositoryFactory.GetRejectedDrugRepository().GetOne(id);

            return rejected;
        }
        public RejectedDrug GetByDrugId(long id)
        {
            RejectedDrug rejected = _repositoryFactory.GetRejectedDrugRepository()
                .GetAll().Where(x=>x.Drug.Id.Equals(id)).FirstOrDefault();

            return rejected;
        }
    }
}
