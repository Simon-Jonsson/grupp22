using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Delete(int index);
        void Update(int index, T newEntity);
        List<T> GetAll();
        void SaveChanges();
    }
}
