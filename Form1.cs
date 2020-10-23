using Business_Layer;
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
        public Form_Podcast()
        {
            InitializeComponent();
            kategoriController = new KategoriController();
        }

        private void Ny_Kategori_Click(object sender, EventArgs e)
        {
            kategoriController.CreateKategori(textBox_NyKategori.Text);

            listBox_Kategori.Items.Clear();

            foreach (var item in kategoriController.GetAllKategori())
            {
                if (item != null)
                {
                    listBox_Kategori.Items.Add(item.Namn);
                }
            }
        }
    }
}
