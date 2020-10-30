using DAL.Repositories;
using Models;
using System;
using System.Collections.Generic;

namespace BL
{
    //Hela klassen förmedlar information mellan presentationslagret och datalagret
    public class PodcastController
    {

        IPodcastRepository<Podcast> podcastRepository;

        public PodcastController()
        {
            podcastRepository = new PodcastRepository();
        }

        public void CreatePodcast(string _URL, string _kategori, int _updateFrequency, int _numberOfEpisodes, string _name)
        {
            Podcast newPodcast = new Podcast(_URL, _kategori, _updateFrequency, _numberOfEpisodes, _name);
            podcastRepository.Create(newPodcast);
        }
        public List<Podcast> GetAllPodcasts()
        {
            return podcastRepository.GetAll();
        }

        public void RemovePodcast(int index)
        {
            podcastRepository.Delete(index);
        }
        public string GetPodcastUrl(int index)
        {
            return podcastRepository.GetUrl(index);
        }

        public List<Podcast> GetAllPodcastByKategori(string kategori)
        {
            return podcastRepository.GetAllByKategori(kategori);
        }

        public void ChangePodcast(int index, Podcast updatePodcast)
        {
            podcastRepository.Update(index, updatePodcast);
        }
    }
}
