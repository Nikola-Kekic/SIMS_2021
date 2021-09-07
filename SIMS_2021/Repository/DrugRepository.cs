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
    public class DrugRepository : Repository<Drug>, IDrugRepository  
    {
        public DrugRepository()
        {
            EntityPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "drugs.json";
        }

        public List<Drug> FindByName(string name)
        {
            return this.GetAll().Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
        }
        
    }
}
