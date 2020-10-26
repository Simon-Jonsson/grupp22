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

             //Tanken är att där länken är ska vi skicka in valfri podd. Den podden ska sen lagras i "SyndicationFeed feed"
            // det är den variabeln som vi vill serialize och deserialize
            
            //int i = 0;
            //int antalAvsnitt = 0;
            //XmlReader reader = XmlReader.Create("http://joeroganexp.joerogan.libsynpro.com/rss");
            //SyndicationFeed feed = SyndicationFeed.Load(reader);
            //Console.WriteLine("Podcast Name: " + feed.Title.Text);
            //Console.WriteLine("Podcast Description: " + feed.Description.Text);
            //foreach (SyndicationItem item in feed.Items)
            //{
            //    antalAvsnitt++;
            //}
            //Console.WriteLine("Antal Avsnitt: " + antalAvsnitt + "\n");
            //foreach (SyndicationItem item in feed.Items)
            //{
            //    Console.WriteLine(item.Title.Text);
            //    Console.WriteLine("Beskrivning av avsnitt: \n\n" + item.Summary.Text + "\n");
            //    i++;
            //    if (i == 15)
            //    {
            //        i = 0;
            //        break;
            //    }

        }

        public Podcast()
        {

        }
    }
}
