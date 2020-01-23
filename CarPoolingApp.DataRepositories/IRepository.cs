using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolingApp.DataRepositories
{
    public interface IRepository<T> where T:IEntity
    {
        T FindById(string id);
        T FindByName(string name);
        void Add(T entity);
        void UpdateById(T entity);
        void UpdateByName(T entity);
        void Remove(T entity);
        List<T> GetAllObjects();
    }
}
