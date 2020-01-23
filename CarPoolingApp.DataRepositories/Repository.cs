using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace CarPoolingApp.DataRepositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        List<T> objects = new List<T>();
        string filePath = "../../../../CarPoolingApp.Database/"+typeof(T).Name+".json";
        public Repository()
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            objects = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
        }

        public void Add(T entity)
        {
            objects.Add(entity);
            var jsonData = System.IO.File.ReadAllText(filePath);
            var objectsNew = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
            objectsNew.Add(entity);
            jsonData = JsonConvert.SerializeObject(objectsNew);
            System.IO.File.WriteAllText(filePath, jsonData);
        }

        public T FindById(string id)
        {
            return objects.Find(_ => (string.Equals(_.id, id)));
        }

        public T FindByName(string name)
        {
            return objects.Find(_ => (string.Equals(_.userName, name)));
        }
        public void UpdateByName(T entity)
        {
            var entityToUpdate = objects.Find(_ => (string.Equals(_.userName, entity.userName)));
            objects.Remove(entityToUpdate);
            objects.Add(entity);
            var jsonData = JsonConvert.SerializeObject(objects);
            System.IO.File.WriteAllText(filePath, jsonData);
        }
        public void UpdateById(T entity)
        {
            var entityToUpdate = objects.Find(_ => (string.Equals(_.id, entity.id)));
            objects.Remove(entityToUpdate);
            objects.Add(entity);
            var jsonData = JsonConvert.SerializeObject(objects);
            System.IO.File.WriteAllText(filePath, jsonData);
        }
        public void Remove(T entity)
        {
            objects.Remove(entity);
            var jsonData = JsonConvert.SerializeObject(objects);
            System.IO.File.WriteAllText(filePath, jsonData);
        }
        public List<T> GetAllObjects()
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            objects = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
            return objects;
        }
    }
}
