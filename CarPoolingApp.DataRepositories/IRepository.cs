using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.DataRepositories
{
    public interface IRepository<T> where T:IEntity
    {
        // Seperate interfaces from classes.
        T FindById(string id);
        T FindByName(string name);
        void Add(T entity);
        void UpdateById(T entity);
        void UpdateByName(T entity);
        void Remove(T entity);

        //Use IEnumerable instead of list in interfaces. Its upto the class that implements to what object to be returned. If Ienumerable is used this can be more generalised.
        List<T> GetAllObjects();
    }
}
