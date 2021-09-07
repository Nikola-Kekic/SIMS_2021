using SIMS_2021.Model;
using SIMS_2021.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Controller
{
    public class RejectedController
    {
        private RejectedService _rejectedService = new RejectedService();

        public void Save(long id, string description)
        {
            _rejectedService.Save(id, description);
        }
        public RejectedDrug GetOne(long id) 
        {
            return  _rejectedService.GetOne(id);
        }
        public RejectedDrug GetByDrugId(long id)
        {
            return  _rejectedService.GetByDrugId(id);
        }
    }
}
