using SIMS_2021.DTOs;
using SIMS_2021.Model;
using SIMS_2021.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Controller
{
    public class PatientsController
    {
        private PatientService _patientService = new PatientService();

        public List<User> GetAll()
        {
            return _patientService.GetAll();
        }
        public bool AddPatient(User patient)
        {
            return _patientService.AddPatient(patient);
        }
        public int GetBoughtDrugsSum(long patientId)
        {
            return _patientService.GetBoughtDrugsSum(patientId);
        }
        public List<CartDTO> GetDrugsAndQuantity(Dictionary<long, int> cart)
        {
            return _patientService.GetDrugsAndQuantity(cart);
        }
    }
}

