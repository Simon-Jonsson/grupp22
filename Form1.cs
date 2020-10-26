using Business_Layer;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            kategoriController.CreateKategori(textBox_NyKategori.Text);

            listBox_Kategori.Items.Clear();

            VisaKategori();

            textBox_NyKategori.Clear();


        }

        private void textBox_NyKategori_TextChanged(object sender, EventArgs e)
        {
           
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

        private void comboBox_Kategori_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            int uppdateringsFrekvens = int.Parse(comboBox_UF.SelectedItem.ToString());
            podcastController.CreatePodcast(textBox_URL.Text, comboBox_Kategori.SelectedItem.ToString(), uppdateringsFrekvens, 10, textBox_Namn.Text);            
            RensaNyPodcast();
            VisaPodcasts();
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
    }
}
