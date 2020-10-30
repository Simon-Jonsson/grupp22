using DAL.Repositories;
using Models;
using System;
using System.Collections.Generic;

namespace BL
{
    //Hela klassen förmedlar information mellan presentationslagret och datalagret
    public class KategoriController
    {
        IKategoriRepository<Kategori> kategoriRepository;

        public KategoriController()
        {
            kategoriRepository = new KategoriRepository();
        }

        public void CreateKategori(string name)
        {
            Kategori newKategori = null;

            newKategori = new Kategori(name);
            kategoriRepository.Create(newKategori);

        }

        public List<Kategori> GetAllKategori()
        {
            return kategoriRepository.GetAll();
        }

        public string GetKategoriByName(string name)
        {
            Kategori kategori;
            kategori = kategoriRepository.GetByName(name);
            return kategori.Name;
        }

        public void RemoveKategori(int index)
        {
            kategoriRepository.Delete(index);
        }

        public void ChangeKategori(int index, Kategori updateKategori)
        {
            kategoriRepository.Update(index, updateKategori);
        }
    }
}
