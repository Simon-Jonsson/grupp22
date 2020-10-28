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
        private Timer timer1 = new Timer();
        //Klassen Student definieras i en separat fil
        // Addera till listan students två Student objekten. 
        // Den första objektet har uppdateringsinterval av halv minut
        // Den andra objektet har uppdateringsinterval av en minut
        private List<Podcast> students = new List<Podcast>(); 
        
        //{
        //    new Podcast("_URL", "_kategori", 10, 50, "_namn"),
        //    new Podcast ("_URL2", "_kategori2", 20, 100, "_namn2")
        //};

        KategoriController kategoriController;
        PodcastController podcastController;
        Validation validation;
       

        public Form_Podcast()
        {
            InitializeComponent();
            kategoriController = new KategoriController();
            podcastController = new PodcastController();
            validation = new Validation();

            timer1.Interval = 1000;
            // Koppla event handler Timer_Tick() som ska köras varje gång timern körs dvs varje sekund
            // Tick är en event i klassen Timer som använder en inbyggd delegat EventHandler(object sender, EventArgs e); 
            timer1.Tick += Timer1_Tick;
            // starta timer
            timer1.Start();

            Spara_Podcast.Enabled = false;
            textBox_URL.Enabled = true;
            VisaKategori();
            VisaPodcasts();
            AddPodcastToTimer();
        }

        
        private void Timer_Tick(object sender, EventArgs e)
        {
            // loopa genom listan av studenter
            foreach (var podcast in podcastController.GetAllPodcasts())
            {
                // kolla om student objekt behöver uppdateras
                if (podcast.NeedsUpdate)
                {
                    // i så fall anroppa dess Update() och tilldela sträng värdet till en listbox
                    listBox_Avsnitt.Items.Add(podcast.Update());
                    listBox_Beskrivning.Items.Add(podcast.Namn + "\t" + podcast.UppdateringsIntervall);

                }
            }

        }

        private void AddPodcastToTimer()
        {
            foreach (var item in podcastController.GetAllPodcasts())
            {
                students.Add(item);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            // loopa genom listan av studenter
            foreach (var student in students)
            {
                // kolla om student objekt behöver uppdateras
                if (student.NeedsUpdate)
                {
                    // i så fall anroppa dess Update() och tilldela sträng värdet till en listbox
                    listBox_Avsnitt.Items.Add(student.Update());
                }
            }

            
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
            if (validation.isInputEmpty(textBox_NyKategori.Text) == false)
            {
                if (validation.isInputLetters(textBox_NyKategori.Text) == false)
                {
                    MessageBox.Show("Namnet får endast innehålla bokstäver");
                }
                else
                {
                    if (listBox_Kategori.SelectedItem != null)
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
                        listBox_Kategori.ClearSelected();
                        MessageBox.Show("Ändringarna är sparade");
                    }
                    else
                    {
                        MessageBox.Show("Vänligen välj en kategori att spara");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vänligen fyll i kategorinamnet");
            }
        }

        private void TaBort_Kategori_Click(object sender, EventArgs e)
        {
            
            var confirmResult = MessageBox.Show("Vill du ta bort denna kategori och tillhörande podcasts?", "Confirm Delete", MessageBoxButtons.YesNo);


            try 
            {
                if (confirmResult == DialogResult.Yes)
                {
                    if (listBox_Kategori.SelectedItem != null)
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
                    else
                    {
                        MessageBox.Show("Vänligen välj en kategori att ta bort");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen välj en kategori att ta bort");
            }
            
        }

        private void Ny_Podcast_Click(object sender, EventArgs e)
        {
            try
            {
                if (validation.isInputEmpty(textBox_Namn.Text) == false)
                {
                    if (GetAntalAvsnitt(textBox_URL.Text) != 0)
                    {
                        int uppdateringsFrekvens = int.Parse(comboBox_UF.SelectedItem.ToString());
                        podcastController.CreatePodcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), uppdateringsFrekvens, GetAntalAvsnitt(textBox_URL.Text), textBox_Namn.Text);
                        RensaNyPodcast();
                        VisaPodcasts();
                    }                      
                }
                else
                {
                    MessageBox.Show("Kontrollera att du har fyllt i podcastnamnet");
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

                MessageBox.Show("Kontrollera att du har angett en giltig URL");

            }

            return antalAvsnitt;
        }

        private void TaBort_Podcast_Click(object sender, EventArgs e)
        {

            try
            {
                var confirmResult = MessageBox.Show("Vill du ta bort denna kategori och tillhörande podcasts?", "Confirm Delete", MessageBoxButtons.YesNo);
                    
                if (confirmResult == DialogResult.Yes)
                    {
                        var index = ListView_Podcast.SelectedIndices;
                        podcastController.RemovePodcast(index[0]);
                        RensaNyPodcast();
                        VisaPodcasts();
                    }
                  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen välj en podcast att ta bort.");
            }
        }

        private void button_Visa_Click(object sender, EventArgs e)
        {
            listBox_Avsnitt.Items.Clear();

            try
            {
                if (ListView_Podcast.SelectedItems != null)
                {
                    GetPodcastAvsnitt();
                }
                else
                {
                    MessageBox.Show("Ingen podcast vald");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vänligen välj en podcast först.");
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
            try
            {
                int uppdateringsFrekvens = int.Parse(comboBox_UF.SelectedItem.ToString());
                if (validation.isInputEmpty(textBox_Namn.Text) == false)
                {
                    if (validation.isInputLetters(textBox_Namn.Text) == false)
                    {
                        MessageBox.Show("Namnet får endast innehålla bokstäver");
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
                        Spara_Podcast.Enabled = false;
                        textBox_URL.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Kontrollera att namn, URL, uppdateringsfrekvens och kategori är ifyllt");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
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
            try
            {
                listBox_SorteradeKategorier.Items.Clear();
                if (listBox_Kategori.SelectedItem != null) 
                {
                    GetSorteradeKategorier(listBox_Kategori.SelectedItem.ToString());
                    textBox_NyKategori.Clear();
                    listBox_Kategori.ClearSelected();
                }
                else
                {
                    MessageBox.Show("Vänligen välj en kategori att sortera");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
