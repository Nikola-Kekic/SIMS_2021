using Newtonsoft.Json;
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
    public class Repository<Entity> : IRepository<Entity> where Entity : global::SIMS_2021.Model.Entity
    {
        public string EntityPath { get; set; } 
        public Entity Add(Entity newEntity)
        {
            newEntity = GenerateId(newEntity);
            List<Entity> entities = GetAll();
            entities.Add(newEntity);
            Save(entities);

            return newEntity;
        }

        public List<Entity> GetAll()
        {
            if (!File.Exists(EntityPath)) 
            {
                var file = File.OpenWrite(EntityPath);
                file.Close();
            }
            List<Entity> entities;
            String allText = File.ReadAllText(EntityPath);
            if (allText.Equals(""))
            {
                entities = new List<Entity>();
            }
            else
            {
                entities = JsonConvert.DeserializeObject<List<Entity>>(allText);
            }
            return entities;
        }

        public Entity GetOne(long id)
        {
            List<Entity> entities = GetAll();
            var requiredEntity = entities.Where(x => x.Id.Equals(id)).FirstOrDefault();

            return requiredEntity;
        }

        public Entity Update(Entity newEntity)
        {
            List<Entity> entities = GetAll();
            var requiredEntity = entities.Where(x => x.Id.Equals(newEntity.Id)).FirstOrDefault();
            entities[entities.IndexOf(requiredEntity)] = newEntity;
            Save(entities);

            return requiredEntity;
        }

        private Entity GenerateId(Entity entity) 
        {
            List<Entity> entities = GetAll();
            var entityMaxId = entities.Select(x => x.Id).Max();
            entity.Id = ++entityMaxId;

            return entity;
        }
        private void Save(List<Entity> entities) 
        {
            string serializedObjects = JsonConvert.SerializeObject(entities);
            File.WriteAllText(EntityPath, serializedObjects);
        }
    }
}
