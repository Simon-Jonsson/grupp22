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

namespace grupp22_projekt
{
    public partial class Form_Podcast : Form
    {
        KategoriController kategoriController;
        PodcastController podcastController;
        private string kategoriNamn;
        public Form_Podcast()
        {
            InitializeComponent();
            kategoriController = new KategoriController();
            podcastController = new PodcastController();
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
            kategoriController.ChangeKategori(index, bytKategoriNamn);
            listBox_Kategori.Items.Clear();
            VisaKategori();
            textBox_NyKategori.Clear();
        }

        private void TaBort_Kategori_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Vill du ta bort denna kategori och tillhörande podcasts?", "Confirm Delete", MessageBoxButtons.YesNo);


            if (listBox_Kategori.SelectedItem != null)
            {
                if (confirmResult == DialogResult.Yes)
                {
                    kategoriController.RemoveKategori(listBox_Kategori.SelectedIndex);
                    listBox_Kategori.Items.Clear();
                    VisaKategori();
                    textBox_NyKategori.Clear();
                }
            }
            else
            {
                MessageBox.Show("Ingen kategori vald");
            }
        }

        private void Ny_Podcast_Click(object sender, EventArgs e)
        {
            if (textBox_Namn.Text.Equals("") || textBox_URL.Text.Equals("") || comboBox_Kategori.Text.Equals("") || comboBox_UF.Text.Equals(""))
            {
                MessageBox.Show("Kontrollera att namn, URL, uppdateringsfrekvens och kategori är ifyllt");
            }
            else
            {
                int uppdateringsFrekvens = int.Parse(comboBox_UF.SelectedItem.ToString());
                podcastController.CreatePodcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), uppdateringsFrekvens, GetAntalAvsnitt(textBox_URL.Text), textBox_Namn.Text);
                RensaNyPodcast();
                VisaPodcasts();
            }
        }

        private int GetAntalAvsnitt(string URL)

        {
            int antalAvsnitt = 0;
            XmlReader reader = XmlReader.Create(URL);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            foreach (var item in feed.Items)
            {
                antalAvsnitt++;
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

    }
}
