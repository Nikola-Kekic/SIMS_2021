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
    }
}
