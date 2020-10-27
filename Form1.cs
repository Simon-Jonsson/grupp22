using Business_Layer;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace grupp22_projekt
{
    public partial class Form_Podcast : Form
    {
        KategoriController kategoriController;
        PodcastController podcastController;
        public Form_Podcast()
        {
            InitializeComponent();
            kategoriController = new KategoriController();
            podcastController = new PodcastController();
            Spara_Podcast.Enabled = false;
            textBox_URL.Enabled = true;
            VisaKategori();
            VisaPodcasts();          
        }
        
        private void VisaPodcasts()
        {

            foreach (var item in podcastController.GetAllPodcasts())
            {
                if (item != null)
                {
                    string[] podcasts = new string[4];
                    ListViewItem listView;

                    podcasts[0] = item.AntalAvsnitt.ToString();
                    podcasts[1] = item.Namn;
                    podcasts[2] = item.UppdateringsIntervall.ToString();
                    podcasts[3] = item.Kategori;
                    listView = new ListViewItem(podcasts);
                    ListView_Podcast.Items.Add(listView);
                }
            }
        }

        private void VisaKategori()
        {
            foreach (var item in kategoriController.GetAllKategori())
            {
                if (item != null)
                {
                    listBox_Kategori.Items.Add(item.Namn);
                }
            }
            FyllKategoriCombobox();
        }

        private void FyllKategoriCombobox()
        {
            comboBox_Kategori.Items.Clear();
            foreach (var item in kategoriController.GetAllKategori())
            {
                if (item != null)
                {
                    comboBox_Kategori.Items.Add(item.Namn);
                }
            }
        }

        private void RensaNyPodcast()
        {
            textBox_Namn.Clear();
            textBox_URL.Clear();
            comboBox_UF.ResetText();
            comboBox_Kategori.ResetText();
            ListView_Podcast.Items.Clear();
        }

        private void Ny_Kategori_Click(object sender, EventArgs e)
        {

                if (textBox_NyKategori.Text.Equals(""))
                {
                    MessageBox.Show("Inget kategorinamn angivet");
                }
                else
                {
                    kategoriController.CreateKategori(textBox_NyKategori.Text);
                    listBox_Kategori.Items.Clear();
                    VisaKategori();
                    textBox_NyKategori.Clear();
                }
            
            
        }

        private void DeletePodcastMedKategori(string kategori)
        {

            for (int i = podcastController.GetAllPodcasts().Count - 1; i >= 0; i--)
            {
                if (podcastController.GetAllPodcasts()[i].Kategori.Equals(kategori))
                {
                    listBox_Beskrivning.Items.Add(podcastController.GetAllPodcasts()[i].Namn + "\t" + podcastController.GetAllPodcasts()[i].Kategori + "\t" + i);

                    podcastController.RemovePodcast(i);
                }
            }
        }

        private void listBox_Kategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_Kategori.SelectedItem != null)
            {
                string selectedKategori = listBox_Kategori.SelectedItem.ToString();
                textBox_NyKategori.Text = kategoriController.GetKategoriByName(selectedKategori);
                
            }
            else
            {
                MessageBox.Show("Ingen kategori vald");
            }
            

        }

        private void Spara_Kategori_Click(object sender, EventArgs e)
        {
            Kategori bytKategoriNamn = new Kategori(textBox_NyKategori.Text);
            int index = listBox_Kategori.SelectedIndex;
            string valdKategori = listBox_Kategori.SelectedItem.ToString();
            kategoriController.ChangeKategori(index, bytKategoriNamn);
            listBox_Kategori.Items.Clear();
            ChangePodcastKategori(valdKategori);
            VisaKategori();
            RensaNyPodcast();
            VisaPodcasts();
            textBox_NyKategori.Clear();
           
        }

        private void TaBort_Kategori_Click(object sender, EventArgs e)
        {
            
            var confirmResult = MessageBox.Show("Vill du ta bort denna kategori och tillhörande podcasts?", "Confirm Delete", MessageBoxButtons.YesNo);


            if (listBox_Kategori.SelectedItem != null)
            {
                if (confirmResult == DialogResult.Yes)
                {
                    string valdKategori = listBox_Kategori.SelectedItem.ToString();
                    DeletePodcastMedKategori(valdKategori);
                    kategoriController.RemoveKategori(listBox_Kategori.SelectedIndex);
                    listBox_Kategori.Items.Clear();
                    textBox_NyKategori.Clear();
                    VisaKategori();
                    RensaNyPodcast();
                    VisaPodcasts();
                }
            }
            else
            {
                MessageBox.Show("Ingen kategori vald");
            }
        }

        private void Ny_Podcast_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_Namn.Text.Equals("") || textBox_URL.Text.Equals("") || comboBox_Kategori.Text.Equals("") || comboBox_UF.Text.Equals(""))
                {
                    MessageBox.Show("Kontrollera att namn, URL, uppdateringsfrekvens och kategori är ifyllt");
                }
                else if(GetAntalAvsnitt(textBox_URL.Text) == 0)
                {
                    MessageBox.Show("Kontrollera att du har angett en giltig URL");
                }
                else
                {          
                    int uppdateringsFrekvens = int.Parse(comboBox_UF.SelectedItem.ToString());
                    podcastController.CreatePodcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), uppdateringsFrekvens, GetAntalAvsnitt(textBox_URL.Text), textBox_Namn.Text);
                    RensaNyPodcast();
                    VisaPodcasts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetAntalAvsnitt(string URL)

        {
            int antalAvsnitt = 0;
            try
            {
               
                XmlReader reader = XmlReader.Create(URL);
                SyndicationFeed feed = SyndicationFeed.Load(reader);

                foreach (var item in feed.Items)
                {
                    antalAvsnitt++;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                
            }

            return antalAvsnitt;
        }

        private void TaBort_Podcast_Click(object sender, EventArgs e)
        {
            if (ListView_Podcast.SelectedItems != null)
            {

                var index = ListView_Podcast.SelectedIndices;
                podcastController.RemovePodcast(index[0]);
                RensaNyPodcast();
                VisaPodcasts();      
            }
            else
            {
                MessageBox.Show("Ingen podcast vald");
            }
        }

        private void button_Visa_Click(object sender, EventArgs e)
        {
            listBox_Avsnitt.Items.Clear();

            if (ListView_Podcast.SelectedItems != null)
            {
                GetPodcastAvsnitt();
            }
            else
            {
                MessageBox.Show("Ingen podcast vald");
            }
        }

        private string GetURL()
        {

            var index = ListView_Podcast.SelectedIndices;

            return podcastController.GetPodcastUrl(index[0]);

        }

        private void ChangePodcastKategori(string valdKategori)
        {
            XDocument document = XDocument.Load(("Podcasts.xml"));
            var updateQuery = from r in document.Descendants("Podcast") where r.Element("Kategori").Value.Equals(valdKategori) select r;
            foreach (var query in updateQuery)
            {
                query.Element("Kategori").SetValue(textBox_NyKategori.Text);
            }
            document.Save(("Podcasts.xml"));
            VisaPodcasts();
        }

        private void GetSorteradeKategorier(string valdKategori)
        {
            foreach (var item in podcastController.GetAllPodcastByKategori(valdKategori))
            {
                listBox_SorteradeKategorier.Items.Add(item.Namn);
            }
        }

        private List<String> GetPodcastAvsnitt()
        {
            XmlReader reader = XmlReader.Create(GetURL());
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            List<string> beskrivningar = new List<string>();
            

            foreach (SyndicationItem item in feed.Items)
            {
                listBox_Avsnitt.Items.Add(item.Title.Text);
                beskrivningar.Add(item.Summary.Text);
            }

            return beskrivningar;
        }

        private void listBox_Avsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_Beskrivning.Items.Clear();

            List<string> allaAvsnitt = GetPodcastAvsnitt();

            int avsnittIndex = listBox_Avsnitt.SelectedIndex;
            avsnittLabel.Text = listBox_Avsnitt.SelectedItem.ToString();

            for (int i = 0; i < allaAvsnitt.Count; i++)
            {
                if (i == avsnittIndex)
                {
                    listBox_Beskrivning.Items.Add(allaAvsnitt[avsnittIndex]);
                }
            }

        }

        private void Spara_Podcast_Click(object sender, EventArgs e)
        {
            Spara_Podcast.Enabled = false;
            textBox_URL.Enabled = true;
            int uppdateringsFrekvens = int.Parse(comboBox_UF.SelectedItem.ToString());
            if (textBox_Namn.Text.Equals("") || textBox_URL.Text.Equals("") || comboBox_Kategori.Text.Equals("") || comboBox_UF.Text.Equals(""))
            {
                MessageBox.Show("Kontrollera att namn, URL, uppdateringsfrekvens och kategori är ifyllt");
            }
            else
            {
                Podcast ändraPodcast = new Podcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), uppdateringsFrekvens, GetAntalAvsnitt(textBox_URL.Text), textBox_Namn.Text);
                int index = ListView_Podcast.SelectedIndices[0];
                podcastController.ChangePodcast(index, ändraPodcast);
                ListView_Podcast.Items.Clear();
                RensaNyPodcast();
                VisaPodcasts();
                MessageBox.Show("Ändringarna är sparade");
            }
        }

        private void ListView_Podcast_MouseClick(object sender, MouseEventArgs e)
        {
            textBox_Namn.Text = ListView_Podcast.SelectedItems[0].SubItems[1].Text;
            comboBox_UF.Text = ListView_Podcast.SelectedItems[0].SubItems[2].Text;
            comboBox_Kategori.Text = ListView_Podcast.SelectedItems[0].SubItems[3].Text;
            textBox_URL.Enabled = false;
            Spara_Podcast.Enabled = true;
            string urlText = GetURL();
            textBox_URL.Text = urlText;
        }

        private void Sortera_Kategori_Click(object sender, EventArgs e)
        {
            listBox_SorteradeKategorier.Items.Clear();
            GetSorteradeKategorier(listBox_Kategori.SelectedItem.ToString());
            textBox_NyKategori.Clear();  
        }
    }
}
