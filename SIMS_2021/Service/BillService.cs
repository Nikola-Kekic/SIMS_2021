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
    public class BillService
    {
        public RepositoryFactory _repositoryFactory;
        public BillService()
        {
            _repositoryFactory = new FileRepositoryFactory();
        }

        public List<BillDTO> GetAllForUser(long userId)
        {
            List<Bill> bills = _repositoryFactory.GetBillRepository().GetAllByUserId(userId);
            List<BillDTO> billDTOs = new List<BillDTO>();

            foreach(var bill in bills)
            {
                BillDTO billDTO = new BillDTO { Code = bill.Code, DateAndTime = bill.DateAndTime, Pharmacist = bill.Pharmacist, TotalPrice = bill.TotalPrice };
                billDTO.DrugsAndQuantity = new Dictionary<Drug, int>();

                foreach (KeyValuePair<long, int> item in bill.DrugsAndQuantity)
                {
                    billDTO.DrugsAndQuantity.Add(_repositoryFactory.GetDrugRepository().GetOne(item.Key), item.Value);
                }
                billDTOs.Add(billDTO);
            }

            return billDTOs;
        }

        public bool Buy(List<CartDTO> cart, long userId)
        {
            Bill bill = new Bill();
            bill.DateAndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var pharmacists = _repositoryFactory.GetUserRepository().GetByUserType(UserType.Pharmacist);
            if(pharmacists.Count > 0)
            {
                bill.Pharmacist = pharmacists[0].Name + " " + pharmacists[0].LastName;
            }
            bill.DrugsAndQuantity = new Dictionary<long, int>();
            bill.UserId = userId;

            float totalPrice = 0;

            foreach(var item in cart)
            {
                if(!bill.DrugsAndQuantity.ContainsKey(item.DrugId))
                    bill.DrugsAndQuantity.Add(item.DrugId, item.Quantity);
                else
                    bill.DrugsAndQuantity[item.DrugId]++;

                totalPrice += _repositoryFactory.GetDrugRepository().GetOne(item.DrugId).Price;
            }

            bill.TotalPrice = totalPrice;

            bill.Code = _repositoryFactory.GetBillRepository().GenerateCode();

            if (_repositoryFactory.GetBillRepository().Add(bill) != null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
