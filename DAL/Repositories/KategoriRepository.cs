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

        List<Kategori> kategoriList;
        DataManager dataManager;

        public KategoriRepository()
        {
            kategoriList = new List<Kategori>();
            dataManager = new DataManager();
            kategoriList = GetAll();
        }

        //Lägger till en kategori i XML-filen
        public void Create(Kategori entity)
        {
            kategoriList.Add(entity);
            SaveChanges();
        }

        //Tar bort en kategori från XML-filen
        public void Delete(int index)
        {
            kategoriList.RemoveAt(index);
            SaveChanges();
        }

        //Hämtar alla kategorier i XML-filen
        public List<Kategori> GetAll()
        {
            List<Kategori> kategoriListToBeReturned = new List<Kategori>();
            kategoriListToBeReturned = dataManager.DeserializeKategori();
            return kategoriListToBeReturned;
        }

        //Hämtar en kategori med ett visst namn från XML-filen
        public Kategori GetByName(string name)
        {
            return GetAll().First(p => p.Name.Equals(name));
        }

        //Sparar alla ändringar i XML-filen
        public void SaveChanges()
        {
            dataManager.SerializeKategori(kategoriList);
        }

        //Uppdaterar en kategori mot en ny i XML-filen
        public void Update(int index, Kategori newEntity)
        {
            if (index >= 0)
            {
                kategoriList[index] = newEntity;
            }
            SaveChanges();
        }
    }
}
