using Data_Access_Layer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupp22_projekt
{
    class PodcastRepository : IPodcastRepository<Podcast>

    {
        List<Podcast> podcastLista;
        DataManager dataManager;

        public PodcastRepository()
        {
            podcastLista = new List<Podcast>();
            dataManager = new DataManager();
            podcastLista = GetAll();
        }
        public void Create(Podcast entity)
        {
            podcastLista.Add(entity);
            SaveChanges();
        }

        public void Delete(int index)
        {
            podcastLista.RemoveAt(index);
            SaveChanges();
        }

        public List<Podcast> GetAll()
        {
            List<Podcast> podcastListToBeReturned = new List<Podcast>();
            podcastListToBeReturned = dataManager.DeserializePodcast();
            return podcastListToBeReturned;
        }

        public List<Podcast> GetAllByKategori(string kategori)
        {
            return GetAll().FindAll(p => p.Kategori.Equals(kategori));
        }

        public void SaveChanges()
        {
            dataManager.SerializePodcast(podcastLista);
        }

        public void UpdateList(List<Podcast> podcasts)
        {
            SaveChanges();
        }

        public void Update(int index, Podcast newEntity)
        {
            if (index >= 0)
            {
                podcastLista[index] = newEntity;
            }
            SaveChanges();
        }

        public string GetUrl(int index)
        {
            return podcastLista[index].URL;
        }

    }
}
