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
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository()
        {
            
            EntityPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "bills.json";

        }

        public List<Bill> GetAllByUserId(long userId)
        {
            return GetAll().Where(x => x.UserId.Equals(userId)).ToList();
        }

        public int GenerateCode()
        {
            List<Bill> bills = GetAll();
            var maxCode = 0;
            if (bills != null && bills.Count > 0)
            {
                maxCode = bills.Select(x => x.Code).Max();

            }
            return ++maxCode;
        }
    }
}
