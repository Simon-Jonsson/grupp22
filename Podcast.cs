using System;

namespace Models
{
    public class Podcast
    {
        public string URL { get; set; }
        public string Kategori { get; set; }
        public int UppdateringsIntervall { get; set; }
        public int AntalAvsnitt { get; set; }
        public string Namn { get; set; }

        public Podcast(string _URL, string _kategori, int _uppdateringsIntervall, int _antalAvsnitt, string _namn)
        {
            URL = _URL;
            Kategori = _kategori;
            UppdateringsIntervall = _uppdateringsIntervall;
            AntalAvsnitt = _antalAvsnitt;
            Namn = _namn;
        }

        public Podcast()
        {

        }
    }
}
