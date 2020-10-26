using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Repositories
{
    public interface IKategoriRepository<T> : IRepository<T> where T : Kategori
    {

        T GetByName(string name);
    }
}
