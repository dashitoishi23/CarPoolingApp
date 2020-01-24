using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace CarPoolingApp.DataRepositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        // What if you want to store objects instead of arrays in the json? That case should also be handled.
        //Objects can have dynamic get the file can be fetched and desirialized in the getter
        List<T> objects = new List<T>();
        // Do not couple class names and file names. In case they are to be different this wont work. Have constant filenames mapped to class names.
        string filePath = "../../../../CarPoolingApp.Database/"+typeof(T).Name+".json";
        public Repository()
        {
            //Repetative code can be moved to a seperate method
            var jsonData = System.IO.File.ReadAllText(filePath);
            // you could rather check for empty string instead of desirialize and then check for null. And what if the file doesn't have data in json format
            objects = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
        }

        public void Add(T entity)
        {
            // Mainting two differnet lists may lead to mismatch between file and the list in the object.
            // So always get the file write make changes to the object and write it back. This functionality can be moved to a private method
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

        //Since you are aware of dealing with property names now this could become more dynamic. Send in propery name and value and it should work with any proerty not just name.
        public T FindByName(string name)
        {
            // works only for user model. But the repository class is a generic class. userName is not the mandetory property in all the classes we use
            return objects.Find(_ => (string.Equals(_.userName, name)));
        }
        public void UpdateByName(T entity)
        {
            // works only for user model. But the repository class is a generic class. userName is not the mandetory property in all the classes we use
            var entityToUpdate = objects.Find(_ => (string.Equals(_.userName, entity.userName)));

            // update the object of istead of remove and add this avoids data loss and gives you ability to chose what properties can be updated. Properties like Ids, usernames cannot be updated.
            objects.Remove(entityToUpdate);
            objects.Add(entity);

            // repetative code.
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
