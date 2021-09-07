using SIMS_2021.Model;
using SIMS_2021.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Repository
{
    public class RejectedDrugRepository : Repository<RejectedDrug>, IRejectedDrugRepository
    {
        public RejectedDrugRepository()
        {
            EntityPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "rejectedDrugs.json";
        }
    }
}
