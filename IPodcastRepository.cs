using Data_Access_Layer.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupp22_projekt
{
    interface IPodcastRepository<T> : IRepository<T> where T : Podcast
    {
        string GetUrl(int index);
        List<Podcast> GetAllByKategori(string kategori);
    }
}
