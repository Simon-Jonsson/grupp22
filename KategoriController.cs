using Data_Access_Layer.Repositories;
using Models;
using System;
using System.Collections.Generic;

namespace Business_Layer
{
    public class KategoriController
    {
        IKategoriRepository<Kategori> kategoriRepository;
        public KategoriController()
        {            
            kategoriRepository = new KategoriRepository();
        }

        public void CreateKategori(string namn)
        {
            Kategori nyKategori = null;

           
                nyKategori = new Kategori(namn);
                kategoriRepository.Create(nyKategori);
            

        }

        public List<Kategori> GetAllKategori()
        {
            return kategoriRepository.GetAll();
        }
    }
}
