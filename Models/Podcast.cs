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
        public DateTime NextUpdate { get; set; }

        public Podcast(string _URL, string _kategori, int _uppdateringsIntervall, int _antalAvsnitt, string _namn)
        {
            URL = _URL;
            Kategori = _kategori;
            UppdateringsIntervall = _uppdateringsIntervall;
            AntalAvsnitt = _antalAvsnitt;
            Namn = _namn;
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
        public void UpdateAntalAvsnitt(int antalAvsnitt)
        {
            AntalAvsnitt = antalAvsnitt;
        }

        //Sätter tidsintervallet för hur ofta podcasten ska uppdateras
        private void Update()
        {
            NextUpdate = DateTime.Now.AddSeconds(UppdateringsIntervall);
        }

        //En tom konstruktor så att man kan serializera/ desarializera
        public Podcast()
        {

        }
    }
}
