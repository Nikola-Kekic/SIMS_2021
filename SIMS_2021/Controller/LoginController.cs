using SIMS_2021.Model;
using SIMS_2021.Repository.RepoFactory;
using SIMS_2021.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Controller
{
    public class LoginController
    {

        private LoginService _loginService = new LoginService();

        public User FindUser(String jmbg, String password)
        {
            return _loginService.FindUser(jmbg, password); 
        }
    }
}
