using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.DataRepositories
{
    public interface IRepository<T> where T:Entity
    {
        T FindByProperty(string propName, string value);
        void Add(T entity);
        void UpdateByProps(Action<T> updateMethod, string id);
        void Remove(T entity);

        //Use IEnumerable instead of list in interfaces. Its upto the class that implements to what object to be returned. If Ienumerable is used this can be more generalised.
        List<T> GetAllObjects();
    }
}
