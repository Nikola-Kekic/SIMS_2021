using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Model
{
    public class User
    {
        public string JMBG { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public UserType UserType { get; set; }
    }
}
