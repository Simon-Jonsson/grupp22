using DAL.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PodcastController
    {

        IPodcastRepository<Podcast> podcastRepository;

        public PodcastController()
        {
            podcastRepository = new PodcastRepository();
        }

        public void CreatePodcast(string _URL, string _kategori, int _uppdateringsIntervall, int _antalAvsnitt, string _namn)
        {
            Podcast nyPodcast = new Podcast(_URL, _kategori, _uppdateringsIntervall, _antalAvsnitt, _namn);
            podcastRepository.Create(nyPodcast);
        }
        public List<Podcast> GetAllPodcasts()
        {
            return podcastRepository.GetAll();
        }

        public void UpdatePodcastList(List<Podcast> podcasts)
        {
            podcastRepository.UpdateList(podcasts);
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

        public void ChangePodcast(int index, Podcast uppdateraPodcast)
        {
            podcastRepository.Update(index, uppdateraPodcast);
        }
    }
}
