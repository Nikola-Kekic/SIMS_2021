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
    public class PatientService
    {

        private RepositoryFactory _repositoryFactory;

        public PatientService()
        {
            _repositoryFactory = new FileRepositoryFactory();
        }

        public List<User> GetAll()
        {
            List<User> users = _repositoryFactory.GetUserRepository().GetAll();
            users = users.Where(x => x.UserType.Equals(UserType.Patient)).ToList();

            return users;
        }

        public bool AddPatient(User patient)
        {
            bool taken = _repositoryFactory.GetUserRepository().GetAll()
                .Any(x => x.JMBG.Equals(patient.JMBG) || x.Email.Equals(patient.Email));
            if (taken)
                return false;
            
            patient.UserType = UserType.Patient;

            if (_repositoryFactory.GetUserRepository().Add(patient) != null)
                return true;
            else
                return false;

        }
        public int GetBoughtDrugsSum(long patientId)
        {
            List<Bill> userBills = _repositoryFactory.GetBillRepository().GetAllByUserId(patientId);
            int sum = 0;

            if (userBills.Count.Equals(0))
            {
                return 0;
            }
            try
            {
                var recent = userBills.Where(x => DateTime.Parse(x.DateAndTime) > DateTime.Today.AddDays(-7));

                foreach (var bill in recent)
                {
                    foreach (var item in bill.DrugsAndQuantity)
                    {
                        sum += item.Value;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return sum;
        }

        public List<CartDTO> GetDrugsAndQuantity(Dictionary<long, int> cart)
        {
            List<CartDTO> cartDTOs = new List<CartDTO>();
            foreach (KeyValuePair<long, int> entry in cart)
            {
                var drug = _repositoryFactory.GetDrugRepository().GetOne(entry.Key);
                if (drug != null)
                {
                    cartDTOs.Add(new CartDTO() { DrugId = drug.Id, DrugName = drug.Name, Quantity = entry.Value });
                }
            }
            return cartDTOs;
        }
    }
}
