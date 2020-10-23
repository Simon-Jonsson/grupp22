using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Repositories
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
