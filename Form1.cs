using BL;
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
        //Skapar en timer och en ny lista med podcasts
        private Timer timer = new Timer();
        private List<Podcast> podcastTimerList = new List<Podcast>(); 

        //Deklarerar även att vi ska ha Controllers och Validations
        KategoriController kategoriController;
        PodcastController podcastController;
        PodcastValidation podcastValidation;
        KategoriValidation kategoriValidation;
       

        public Form_Podcast()
        {
            InitializeComponent();

            //Skapar Controllers och Validations
            kategoriController = new KategoriController();
            podcastController = new PodcastController();
            podcastValidation = new PodcastValidation();
            kategoriValidation = new KategoriValidation();

            //Visar och döljer olika knappar/ textfält
            Spara_Podcast.Enabled = false;
            button_Visa.Enabled = false;
            textBox_URL.Enabled = true;

            //Sätter ett tidsintervall på timern, vad den ska göra och startar den
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            ShowKategori();
            ShowPodcasts();
            AddPodcastToTimerList();
        }

        //Lägger till alla podcasts till listan som ska användas för timern
        private void AddPodcastToTimerList()
        {
            foreach (var item in podcastController.GetAllPodcasts())
            {
                podcastTimerList.Add(item);
            }         
        }

        //Uppdaterar antalet podcastavsnitt för en podcast ifall det har kommit några nya
        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var podcast in podcastTimerList)
            {               
                if (podcast.NeedsUpdate)
                {
                    if(podcast.NumberOfEpisodes != GetNumberOfEpisodes(podcast.URL))
                    {
                        UpdatePodcastEpisodes(podcast.URL);
                        podcast.UpdateNumberOfEpisodes(GetNumberOfEpisodes(podcast.URL));
                        ListView_Podcast.Items.Clear();
                        ShowPodcasts();
                    }
                 
                }  
            }     
        }

        //Visar alla podcasts i en ListView
        private void ShowPodcasts()
        {

            foreach (var item in podcastController.GetAllPodcasts())
            {
                if (item != null)
                {
                    string[] podcasts = new string[4];
                    ListViewItem listView;

                    podcasts[0] = item.NumberOfEpisodes.ToString();
                    podcasts[1] = item.Name;
                    podcasts[2] = item.UpdateFrequency.ToString();
                    podcasts[3] = item.Kategori;
                    listView = new ListViewItem(podcasts);
                    ListView_Podcast.Items.Add(listView);
                }
            }
        }

        //Visar alla kateogrier i en ListBox
        private void ShowKategori()
        {
            foreach (var item in kategoriController.GetAllKategori())
            {
                if (item != null)
                {
                    listBox_Kategori.Items.Add(item.Name);
                }
            }
            FillKategoriCombobox();
        }

        //Fyller upp kategori-combobox
        private void FillKategoriCombobox()
        {
            comboBox_Kategori.Items.Clear();
            foreach (var item in kategoriController.GetAllKategori())
            {
                if (item != null)
                {
                    comboBox_Kategori.Items.Add(item.Name);
                }
            }
        }

        //Tömmer alla textfält för ny podcast och även ListView
        private void ClearNewPodcastFields()
        {
            textBox_Namn.Clear();
            textBox_URL.Clear();
            ListView_Podcast.Items.Clear();
        }

        //Skapar en ny kategori
        private void Ny_Kategori_Click(object sender, EventArgs e)
        {
            try
            {
                if (kategoriValidation.isInputEmpty(textBox_NyKategori.Text) == false)
                {
                    if (kategoriValidation.isInputLetters(textBox_NyKategori.Text) == false)
                    {
                        kategoriController.CreateKategori(textBox_NyKategori.Text);
                        listBox_Kategori.Items.Clear();
                        ShowKategori();
                        textBox_NyKategori.Clear();
                    }
                }
            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
    }

        //Tar bort alla podcasts med en viss kategori
        private void DeletePodcastWithKategori(string kategori)
        {

            for (int i = podcastController.GetAllPodcasts().Count - 1; i >= 0; i--)
            {
                if (podcastController.GetAllPodcasts()[i].Kategori.Equals(kategori))
                {
                    podcastController.RemovePodcast(i);
                }
            }
        }

        //Visar namnet på den kategorin man har klickat på i ett textfält
        private void listBox_Kategori_SelectedIndexChanged(object sender, EventArgs e)
        {

                if (listBox_Kategori.SelectedItem != null)
                {
                    string selectedKategori = listBox_Kategori.SelectedItem.ToString();
                    textBox_NyKategori.Text = kategoriController.GetKategoriByName(selectedKategori);
                }
                else
                {
                    listBox_Kategori.ClearSelected();
                }    

        }

        //Uppdaterar kategori namnet
        private void Spara_Kategori_Click(object sender, EventArgs e)
        {
            try
            {
                if (kategoriValidation.isInputEmpty(textBox_NyKategori.Text) == false)
                {
                    if (kategoriValidation.isInputLetters(textBox_NyKategori.Text) == false)
                    {
                        if (kategoriValidation.isItemNull(listBox_Kategori) == false)
                        {
                            Kategori changeKategori = new Kategori(textBox_NyKategori.Text);
                            int index = listBox_Kategori.SelectedIndex;
                            string selectedKategori = listBox_Kategori.SelectedItem.ToString();
                            kategoriController.ChangeKategori(index, changeKategori);
                            listBox_Kategori.Items.Clear();
                            ChangePodcastKategori(selectedKategori);
                            ShowKategori();
                            ClearNewPodcastFields();
                            ShowPodcasts();
                            textBox_NyKategori.Clear();
                            listBox_Kategori.ClearSelected();
                            MessageBox.Show("Ändringarna är sparade");
                        }
                    }                    
                }
            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Tar bort en kategori och tillhörande podcasts
        private void TaBort_Kategori_Click(object sender, EventArgs e)
        {

            try
            {
                if (kategoriValidation.isItemNull(listBox_Kategori) == false)
                {
                    var confirmResult = MessageBox.Show("Vill du ta bort denna kategori och tillhörande podcasts?", "Är du säker?", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        string selectedKategori = listBox_Kategori.SelectedItem.ToString();
                        DeletePodcastWithKategori(selectedKategori);
                        kategoriController.RemoveKategori(listBox_Kategori.SelectedIndex);
                        listBox_Kategori.Items.Clear();
                        textBox_NyKategori.Clear();
                        ShowKategori();
                        ClearNewPodcastFields();
                        ShowPodcasts();
                        listBox_SorteradeKategorier.Items.Clear();
                    }
                }
            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Skapar en ny podcast
        private void Ny_Podcast_Click(object sender, EventArgs e)
        {
            try
            {
                if (podcastValidation.isInputEmpty(textBox_Namn.Text) == false)
                {
                    if (podcastValidation.isNotValidURL(textBox_URL.Text) == false)
                    {
                        if (podcastValidation.isUppdateringsFrekvensEmpty(comboBox_UF) == false)
                        {
                            if (podcastValidation.isKategoriEmpty(comboBox_Kategori) == false)
                            {
                                int updateFrequency = int.Parse(comboBox_UF.SelectedItem.ToString());
                                podcastController.CreatePodcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), updateFrequency, GetNumberOfEpisodes(textBox_URL.Text), textBox_Namn.Text);
                                ClearNewPodcastFields();
                                ShowPodcasts();

                                Spara_Podcast.Enabled = false;
                                button_Visa.Enabled = false;
                                textBox_URL.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Hämtar antalet avsnitt på en podcast från en URL
        private int GetNumberOfEpisodes(string URL)

        {
            int numberOfEpisodes = 0;
            try
            {
                if (podcastValidation.isNotValidURL(URL) == false)
                {
                    XmlReader reader = XmlReader.Create(URL);
                    SyndicationFeed feed = SyndicationFeed.Load(reader);

                    foreach (var item in feed.Items)
                    {
                        numberOfEpisodes++;
                    }
                }

            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }

            return numberOfEpisodes;
        }

        //Tar bort en podcast
        private void TaBort_Podcast_Click(object sender, EventArgs e)
        {

            try
            {
                if (podcastValidation.isInputEmpty(textBox_URL.Text) == false) //Ändra till en bättre validering
                {
                    var confirmResult = MessageBox.Show("Vill du ta bort denna podcast?", "Är du säker?", MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        var index = ListView_Podcast.SelectedIndices;
                        podcastController.RemovePodcast(index[0]);

                        ClearNewPodcastFields();
                        ShowPodcasts();

                        Spara_Podcast.Enabled = false;
                        button_Visa.Enabled = false;
                        textBox_URL.Enabled = true;
                    }
                    else
                    {
                        ClearNewPodcastFields();
                        ShowPodcasts();
                    }
                }
            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Visar alla avsnitt för en podcast
        private async void button_Visa_ClickAsync(object sender, EventArgs e)
        {
            await GetPodcastEpisodes();
        }

        //Hämtar URLen för en podcast från våran XML fil
        private string GetURL()
        {

            var index = ListView_Podcast.SelectedIndices;

            return podcastController.GetPodcastUrl(index[0]);

        }

        //Byter kategorin på alla podcasts med den valda kategorin
        private void ChangePodcastKategori(string selectedKategori)
        {
            XDocument document = XDocument.Load(("Podcasts.xml"));
            var updateQuery = from r in document.Descendants("Podcast") where r.Element("Kategori").Value.Equals(selectedKategori) select r;
            foreach (var item in updateQuery)
            {
                item.Element("Kategori").SetValue(textBox_NyKategori.Text);
            }
            document.Save(("Podcasts.xml"));
            ShowPodcasts();
        }

        //Uppdaterar antalet avsnitt för en podcast-URL
        private void UpdatePodcastEpisodes(string URL)
        {
            XDocument document = XDocument.Load(("Podcasts.xml"));
            var updateQuery = from r in document.Descendants("Podcast") where r.Element("URL").Value.Equals(URL) select r;
            foreach (var item in updateQuery)
            {
                item.Element("AntalAvsnitt").SetValue(GetNumberOfEpisodes(URL));
            }
            document.Save(("Podcasts.xml"));

        }

        //Hämtar alla podcast med den valda kategorin
        private void GetSortedKategorier(string selectedKategori)
        {
            foreach (var item in podcastController.GetAllPodcastByKategori(selectedKategori))
            {
                listBox_SorteradeKategorier.Items.Add(item.Name);
            }
        }

        //Hämtar alla avsnitt för den valda podcasten
        private async Task GetPodcastEpisodes()
        {
            XmlReader reader = XmlReader.Create(GetURL());
            SyndicationFeed feed = await Task.Run(() => SyndicationFeed.Load(reader));

            foreach (var item in feed.Items)
            {
                listBox_Avsnitt.Items.Add(item.Title.Text);
            }

        }

        //Returnerar en lista med alla avsnitts-beskrivningar
        private List<String> GetEpisodeDescription()
        {
            XmlReader reader = XmlReader.Create(GetURL());
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            List<string> descriptions = new List<string>();

            foreach (var item in feed.Items)
            {
                descriptions.Add(item.Summary.Text);
            }

            return descriptions;
        }

        //Visar beskrivningen för det avsnittet du har valt
        private void listBox_Avsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_Beskrivning.Items.Clear();

            List<string> allEpisodes = GetEpisodeDescription();

            int episodeIndex = listBox_Avsnitt.SelectedIndex;
            avsnittLabel.Text = listBox_Avsnitt.SelectedItem.ToString();

            for (int i = 0; i < allEpisodes.Count; i++)
            {
                if (i == episodeIndex)
                {
                    listBox_Beskrivning.Items.Add(allEpisodes[episodeIndex]);
                }
            }

        }

        //Sparar ändringarna för en podcast
        private void Spara_Podcast_Click(object sender, EventArgs e)
        {
            try
            {
                int updateFrequency = int.Parse(comboBox_UF.SelectedItem.ToString());
                if (podcastValidation.isInputEmpty(textBox_Namn.Text) == false)
                {
                    if (podcastValidation.isNotValidURL(textBox_URL.Text) == false)
                    {
                        if(podcastValidation.isUppdateringsFrekvensEmpty(comboBox_UF) == false)
                        {
                            if (podcastValidation.isKategoriEmpty(comboBox_Kategori) == false)
                            {
                                Podcast changePodcast = new Podcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), updateFrequency, GetNumberOfEpisodes(textBox_URL.Text), textBox_Namn.Text);
                                int index = ListView_Podcast.SelectedIndices[0];
                                podcastController.ChangePodcast(index, changePodcast);

                                ClearNewPodcastFields();
                                ShowPodcasts();

                                MessageBox.Show("Ändringarna är sparade");

                                Spara_Podcast.Enabled = false;
                                button_Visa.Enabled = false;
                                textBox_URL.Enabled = true;
                            }  
                        }                       
                    }
                }
            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //När man klickar på en podcast visas dens information upp i textfälten och comboboxes
        private void ListView_Podcast_MouseClick(object sender, MouseEventArgs e)
        {
                textBox_Namn.Text = ListView_Podcast.SelectedItems[0].SubItems[1].Text;
                comboBox_UF.Text = ListView_Podcast.SelectedItems[0].SubItems[2].Text;
                comboBox_Kategori.Text = ListView_Podcast.SelectedItems[0].SubItems[3].Text;

                textBox_URL.Enabled = false;
                Spara_Podcast.Enabled = true;
                button_Visa.Enabled = true;

                listBox_Avsnitt.Items.Clear();
                listBox_Beskrivning.Items.Clear();
                avsnittLabel.Text = "Avsnitt";

                string urlText = GetURL();
                textBox_URL.Text = urlText;
        }

        //Sorterar kategorierna utifrån det värde man skrev in i textfältet
        private void Sortera_Kategori_Click(object sender, EventArgs e)
        {
            try
            {
                listBox_SorteradeKategorier.Items.Clear();
                if (kategoriValidation.isItemNull(listBox_Kategori) == false) 
                {
                    GetSortedKategorier(listBox_Kategori.SelectedItem.ToString());
                    textBox_NyKategori.Clear();
                    listBox_Kategori.ClearSelected();
                } 
            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
