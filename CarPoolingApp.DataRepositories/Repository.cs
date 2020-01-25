using System.IO;
using System.Collections.Generic;
using CarPoolingApp.Helpers;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using CarPoolingApp.StringPool;
using System.Reflection;
using System;

namespace CarPoolingApp.DataRepositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        // What if you want to store objects instead of arrays in the json? That case should also be handled.

        string filePath = "../../../../CarPoolingApp.Database/" + FilePathsMapper.fileMapper[typeof(T).Name];


        List<T> objects
        {
            get
            {
                var jsonData = System.IO.File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
            }
            set { }
        }

        public void Add(T entity)
        {
            var jsonData = System.IO.File.ReadAllText(filePath);
            var objectsNew = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
            AddToFile(objectsNew, entity);
            objects.Add(entity);
        }
        void AddToFile<O>(List<O> objectsNew, O entity)
        {
            objectsNew.Add(entity);
            var jsonData = JsonConvert.SerializeObject(objectsNew);
            System.IO.File.WriteAllText(filePath, jsonData);
        }
        public T FindByProperty(string propName, string value)
        {
            Type type = objects.ElementAt(0).GetType();
            PropertyInfo[] props = type.GetProperties();
            var prop = props.ToList().Find(_ => (string.Equals(_.Name, propName)));
            return objects.Find(_ => (string.Equals(prop.GetValue(objects.ElementAt(0)), value)));
        }
        public void UpdateByProps(Action<T> updateMethod, string id)
        {
            var oldObj = this.FindByProperty("Id", id);
            int index = objects.FindIndex(_=>string.Equals(_.Id, id));
            updateMethod(oldObj);
            objects[index] = oldObj;
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
