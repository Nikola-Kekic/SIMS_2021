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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository()
        {
            EntityPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "users.json";
        }
        public List<User> GetByUserType(UserType userType)
        {
            return GetAll().Where(x => x.UserType.Equals(userType)).ToList();
        }
    }
}
