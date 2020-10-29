using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public interface IKategoriRepository<T> : IRepository<T> where T : Kategori
    {

        T GetByName(string name);
    }
}
