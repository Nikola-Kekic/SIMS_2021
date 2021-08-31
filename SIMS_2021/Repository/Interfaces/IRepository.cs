using SIMS_2021.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_2021.Repository.Interfaces
{
    public interface IRepository<Entity> where Entity : class
    {
        List<Entity> GetAll();
        Entity GetOne(long id);
        Entity Add(Entity newEntity);
        Entity Update(Entity newEntity);
    }
}
