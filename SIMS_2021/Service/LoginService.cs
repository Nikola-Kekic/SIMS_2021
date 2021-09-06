using SIMS_2021.Model;
using SIMS_2021.Repository.Interfaces;
using SIMS_2021.Repository.RepoFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Service
{
    public class LoginService
    {
        private RepositoryFactory _repositoryFactory;
        
        public LoginService() 
        {
            _repositoryFactory = new FileRepositoryFactory();
        }

        public User FindUser(String jmbg, String password) 
        {
            List<User> users = _repositoryFactory.GetUserRepository().GetAll();
            var user = users.Where(x => x.JMBG.Equals(jmbg) &&
                                        x.Password.Equals(password)).FirstOrDefault();

            return user;
        }
    }
}
