using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PodcastRepository : IPodcastRepository<Podcast>

    {
        List<Podcast> podcastList;
        DataManager dataManager;

        public PodcastRepository()
        {
            podcastList = new List<Podcast>();
            dataManager = new DataManager();
            podcastList = GetAll();
        }

        //Lägger till en podcast i XML-filen
        public void Create(Podcast entity)
        {
            podcastList.Add(entity);
            SaveChanges();
        }

        //Tar bort en podcast från XML-filen
        public void Delete(int index)
        {
            podcastList.RemoveAt(index);
            SaveChanges();
        }

        //Hämtar alla podcasts i XML-filen
        public List<Podcast> GetAll()
        {
            List<Podcast> podcastListToBeReturned = new List<Podcast>();
            podcastListToBeReturned = dataManager.DeserializePodcast();
            return podcastListToBeReturned;
        }

        //Hämtar alla podcasts med en viss kategori från XML-filen
        public List<Podcast> GetAllByKategori(string kategori)
        {
            return GetAll().FindAll(p => p.Kategori.Equals(kategori));
        }

        //Sparar alla ändringar i XML-filen
        public void SaveChanges()
        {
            dataManager.SerializePodcast(podcastList);
        }

        //Uppdaterar en podcast mot en ny i XML-filen
        public void Update(int index, Podcast newEntity)
        {
            if (index >= 0)
            {
                podcastList[index] = newEntity;
            }
            SaveChanges();
        }

        //Hämtar URLen för en podcast i XML-filen
        public string GetUrl(int index)
        {
            return podcastList[index].URL;
        }

    }
}
