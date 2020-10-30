using System;

namespace Models
{
    public class Podcast
    {
        public string URL { get; set; }
        public string Kategori { get; set; }
        public int UpdateFrequency { get; set; }
        public int NumberOfEpisodes { get; set; }
        public string Name { get; set; }
        public DateTime NextUpdate { get; set; }

        public Podcast(string _URL, string _kategori, int _uppdateringsIntervall, int _antalAvsnitt, string _namn)
        {
            URL = _URL;
            Kategori = _kategori;
            UpdateFrequency = _uppdateringsIntervall;
            NumberOfEpisodes = _antalAvsnitt;
            Name = _namn;
            Update();
        }

        //Kollar ifall det är dags att uppdatera podcasten
        public bool NeedsUpdate
        {
            get
            {
                return NextUpdate <= DateTime.Now;
            }
        }

        //Uppdaterar värdet för antalet avsnitt
        public void UpdateNumberOfEpisodes(int numberOfEpisodes)
        {
            NumberOfEpisodes = numberOfEpisodes;
        }

        //Sätter tidsintervallet för hur ofta podcasten ska uppdateras
        private void Update()
        {
            NextUpdate = DateTime.Now.AddSeconds(UpdateFrequency);
        }

        //En tom konstruktor så att man kan serializera/ desarializera
        public Podcast()
        {

        }
    }
}
