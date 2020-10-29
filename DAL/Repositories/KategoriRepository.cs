using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class KategoriRepository : IKategoriRepository<Kategori>
    {

        List<Kategori> kategoriLista;
        DataManager dataManager;

        public KategoriRepository()
        {
            kategoriLista = new List<Kategori>();
            dataManager = new DataManager();
            kategoriLista = GetAll();
        }
        public void Create(Kategori entity)
        {
            kategoriLista.Add(entity);
            SaveChanges();
        }

        public void Delete(int index)
        {
            kategoriLista.RemoveAt(index);
            SaveChanges();
        }

        public List<Kategori> GetAll()
        {
            List<Kategori> kategoriListToBeReturned = new List<Kategori>();
            kategoriListToBeReturned = dataManager.DeserializeKategori();
            return kategoriListToBeReturned;
        }

        public Kategori GetByName(string name)
        {
            return GetAll().First(p => p.Namn.Equals(name));
        }

        public void SaveChanges()
        {
            dataManager.SerializeKategori(kategoriLista);
        }

        public void Update(int index, Kategori newEntity)
        {
            if (index >= 0)
            {
                kategoriLista[index] = newEntity;
            }
            SaveChanges();
        }
    }
}
