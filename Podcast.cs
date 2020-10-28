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

        public bool NeedsUpdate
        {
            get
            {
                // Om nästa uppdatering är innan nuvarande klockslag så ska en uppdatering ske
                // dvs metoden NeedsUpdate ska returnera true
                return NextUpdate <= DateTime.Now;
            }
        }

        public string Update()
        {
            // nästa uppdatering sker om "UpdateInterval" minuter
            // Vi hittar den tidpunkten genom att lägga till det antalet minuter till den 
            // nuvarande tiden.
            NextUpdate = DateTime.Now.AddSeconds(UppdateringsIntervall);
            return Namn + "'s Update() was invoked. Next update is at " + NextUpdate;
        }

        public Podcast()
        {

        }
    }
}
