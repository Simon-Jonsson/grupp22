﻿using BL;
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
                    if(podcast.AntalAvsnitt != GetAntalAvsnitt(podcast.URL))
                    {
                        UpdatePodcastAvsnitt(podcast.URL);
                        podcast.UpdateAntalAvsnitt(GetAntalAvsnitt(podcast.URL));
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

                    podcasts[0] = item.AntalAvsnitt.ToString();
                    podcasts[1] = item.Namn;
                    podcasts[2] = item.UppdateringsIntervall.ToString();
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
                    listBox_Kategori.Items.Add(item.Namn);
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
                    comboBox_Kategori.Items.Add(item.Namn);
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
                            kategoriController.ChangeKategori(index, changeKategori);
                            listBox_Kategori.Items.Clear();
                            ChangePodcastKategori(listBox_Kategori.SelectedItem.ToString());
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
                        DeletePodcastWithKategori(listBox_Kategori.SelectedItem.ToString());
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
                                int uppdateringsFrekvens = int.Parse(comboBox_UF.SelectedItem.ToString());
                                podcastController.CreatePodcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), uppdateringsFrekvens, GetAntalAvsnitt(textBox_URL.Text), textBox_Namn.Text);
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

        private int GetAntalAvsnitt(string URL)

        {
            int antalAvsnitt = 0;
            try
            {
                if (podcastValidation.isNotValidURL(URL) == false)
                {
                    XmlReader reader = XmlReader.Create(URL);
                    SyndicationFeed feed = SyndicationFeed.Load(reader);

                    foreach (var item in feed.Items)
                    {
                        antalAvsnitt++;
                    }
                }

            }
            catch (OurExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }

            return antalAvsnitt;
        }

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

        private void button_Visa_Click(object sender, EventArgs e)
        {
            GetPodcastAvsnitt();
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
            ShowPodcasts();
        }

        private void UpdatePodcastAvsnitt(string URL)
        {
            XDocument document = XDocument.Load(("Podcasts.xml"));
            var updateQuery = from r in document.Descendants("Podcast") where r.Element("URL").Value.Equals(URL) select r;
            foreach (var query in updateQuery)
            {
                query.Element("AntalAvsnitt").SetValue(GetAntalAvsnitt(URL));
            }
            document.Save(("Podcasts.xml"));

        }

        private void GetSorteradeKategorier(string valdKategori)
        {
            foreach (var item in podcastController.GetAllPodcastByKategori(valdKategori))
            {
                listBox_SorteradeKategorier.Items.Add(item.Namn);
            }
        }

        private async Task GetPodcastAvsnitt()
        {
            XmlReader reader = XmlReader.Create(GetURL());
            SyndicationFeed feed = await Task.Run(() => SyndicationFeed.Load(reader));

            foreach (SyndicationItem item in feed.Items)
            {
                listBox_Avsnitt.Items.Add(item.Title.Text);
            }

        }

        private List<String> GetAvsnittBeskrivning()
        {
            XmlReader reader = XmlReader.Create(GetURL());
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            List<string> beskrivningar = new List<string>();

            foreach (SyndicationItem item in feed.Items)
            {
                beskrivningar.Add(item.Summary.Text);
            }

            return beskrivningar;
        }

        private void listBox_Avsnitt_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox_Beskrivning.Items.Clear();

            List<string> allaAvsnitt = GetAvsnittBeskrivning();

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
            try
            {
                int uppdateringsFrekvens = int.Parse(comboBox_UF.SelectedItem.ToString());
                if (podcastValidation.isInputEmpty(textBox_Namn.Text) == false)
                {
                    if (podcastValidation.isNotValidURL(textBox_URL.Text) == false)
                    {
                        if(podcastValidation.isUppdateringsFrekvensEmpty(comboBox_UF) == false)
                        {
                            if (podcastValidation.isKategoriEmpty(comboBox_Kategori) == false)
                            {
                                Podcast ändraPodcast = new Podcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), uppdateringsFrekvens, GetAntalAvsnitt(textBox_URL.Text), textBox_Namn.Text);
                                int index = ListView_Podcast.SelectedIndices[0];
                                podcastController.ChangePodcast(index, ändraPodcast);
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

        private void Sortera_Kategori_Click(object sender, EventArgs e)
        {
            try
            {
                listBox_SorteradeKategorier.Items.Clear();
                if (kategoriValidation.isItemNull(listBox_Kategori) == false) 
                {
                    GetSorteradeKategorier(listBox_Kategori.SelectedItem.ToString());
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
