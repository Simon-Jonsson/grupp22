namespace grupp22_projekt
{
    partial class Form_Podcast
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListView_Podcast = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listBox_Kategori = new System.Windows.Forms.ListBox();
            this.textBox_NyKategori = new System.Windows.Forms.TextBox();
            this.textBox_URL = new System.Windows.Forms.TextBox();
            this.Ny_Podcast = new System.Windows.Forms.Button();
            this.Spara_Podcast = new System.Windows.Forms.Button();
            this.comboBox_UF = new System.Windows.Forms.ComboBox();
            this.comboBox_Kategori = new System.Windows.Forms.ComboBox();
            this.TaBort_Podcast = new System.Windows.Forms.Button();
            this.Ny_Kategori = new System.Windows.Forms.Button();
            this.Spara_Kategori = new System.Windows.Forms.Button();
            this.TaBort_Kategori = new System.Windows.Forms.Button();
            this.listBox_Avsnitt = new System.Windows.Forms.ListBox();
            this.textBox_Beskrivning = new System.Windows.Forms.TextBox();
            this.label_URL = new System.Windows.Forms.Label();
            this.label_UF = new System.Windows.Forms.Label();
            this.label_Kategori_NyPodd = new System.Windows.Forms.Label();
            this.label_Vald_Podd = new System.Windows.Forms.Label();
            this.label_Kategori_NyKategori = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListView_Podcast
            // 
            this.ListView_Podcast.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.ListView_Podcast.HideSelection = false;
            this.ListView_Podcast.Location = new System.Drawing.Point(12, 30);
            this.ListView_Podcast.Name = "ListView_Podcast";
            this.ListView_Podcast.Size = new System.Drawing.Size(410, 125);
            this.ListView_Podcast.TabIndex = 0;
            this.ListView_Podcast.UseCompatibleStateImageBehavior = false;
            this.ListView_Podcast.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Avsnitt";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Namn";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Frekvens";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Kategori";
            this.columnHeader4.Width = 70;
            // 
            // listBox_Kategori
            // 
            this.listBox_Kategori.FormattingEnabled = true;
            this.listBox_Kategori.Location = new System.Drawing.Point(443, 28);
            this.listBox_Kategori.Name = "listBox_Kategori";
            this.listBox_Kategori.Size = new System.Drawing.Size(307, 121);
            this.listBox_Kategori.TabIndex = 1;
            // 
            // textBox_NyKategori
            // 
            this.textBox_NyKategori.Location = new System.Drawing.Point(443, 170);
            this.textBox_NyKategori.Multiline = true;
            this.textBox_NyKategori.Name = "textBox_NyKategori";
            this.textBox_NyKategori.Size = new System.Drawing.Size(307, 30);
            this.textBox_NyKategori.TabIndex = 2;
            // 
            // textBox_URL
            // 
            this.textBox_URL.Location = new System.Drawing.Point(13, 185);
            this.textBox_URL.Multiline = true;
            this.textBox_URL.Name = "textBox_URL";
            this.textBox_URL.Size = new System.Drawing.Size(180, 21);
            this.textBox_URL.TabIndex = 3;
            // 
            // Ny_Podcast
            // 
            this.Ny_Podcast.Location = new System.Drawing.Point(160, 212);
            this.Ny_Podcast.Name = "Ny_Podcast";
            this.Ny_Podcast.Size = new System.Drawing.Size(83, 35);
            this.Ny_Podcast.TabIndex = 4;
            this.Ny_Podcast.Text = "Ny...";
            this.Ny_Podcast.UseVisualStyleBackColor = true;
            // 
            // Spara_Podcast
            // 
            this.Spara_Podcast.Location = new System.Drawing.Point(249, 212);
            this.Spara_Podcast.Name = "Spara_Podcast";
            this.Spara_Podcast.Size = new System.Drawing.Size(83, 35);
            this.Spara_Podcast.TabIndex = 5;
            this.Spara_Podcast.Text = "Spara";
            this.Spara_Podcast.UseVisualStyleBackColor = true;
            // 
            // comboBox_UF
            // 
            this.comboBox_UF.FormattingEnabled = true;
            this.comboBox_UF.Location = new System.Drawing.Point(201, 185);
            this.comboBox_UF.Name = "comboBox_UF";
            this.comboBox_UF.Size = new System.Drawing.Size(125, 21);
            this.comboBox_UF.TabIndex = 6;
            // 
            // comboBox_Kategori
            // 
            this.comboBox_Kategori.FormattingEnabled = true;
            this.comboBox_Kategori.Location = new System.Drawing.Point(332, 185);
            this.comboBox_Kategori.Name = "comboBox_Kategori";
            this.comboBox_Kategori.Size = new System.Drawing.Size(90, 21);
            this.comboBox_Kategori.TabIndex = 7;
            // 
            // TaBort_Podcast
            // 
            this.TaBort_Podcast.Location = new System.Drawing.Point(339, 212);
            this.TaBort_Podcast.Name = "TaBort_Podcast";
            this.TaBort_Podcast.Size = new System.Drawing.Size(83, 35);
            this.TaBort_Podcast.TabIndex = 8;
            this.TaBort_Podcast.Text = "Ta bort...";
            this.TaBort_Podcast.UseVisualStyleBackColor = true;
            // 
            // Ny_Kategori
            // 
            this.Ny_Kategori.Location = new System.Drawing.Point(443, 212);
            this.Ny_Kategori.Name = "Ny_Kategori";
            this.Ny_Kategori.Size = new System.Drawing.Size(95, 35);
            this.Ny_Kategori.TabIndex = 9;
            this.Ny_Kategori.Text = "Ny...";
            this.Ny_Kategori.UseVisualStyleBackColor = true;
            this.Ny_Kategori.Click += new System.EventHandler(this.Ny_Kategori_Click);
            // 
            // Spara_Kategori
            // 
            this.Spara_Kategori.Location = new System.Drawing.Point(549, 212);
            this.Spara_Kategori.Name = "Spara_Kategori";
            this.Spara_Kategori.Size = new System.Drawing.Size(95, 35);
            this.Spara_Kategori.TabIndex = 10;
            this.Spara_Kategori.Text = "Spara";
            this.Spara_Kategori.UseVisualStyleBackColor = true;
            // 
            // TaBort_Kategori
            // 
            this.TaBort_Kategori.Location = new System.Drawing.Point(655, 212);
            this.TaBort_Kategori.Name = "TaBort_Kategori";
            this.TaBort_Kategori.Size = new System.Drawing.Size(95, 35);
            this.TaBort_Kategori.TabIndex = 11;
            this.TaBort_Kategori.Text = "Ta bort..";
            this.TaBort_Kategori.UseVisualStyleBackColor = true;
            // 
            // listBox_Avsnitt
            // 
            this.listBox_Avsnitt.FormattingEnabled = true;
            this.listBox_Avsnitt.Location = new System.Drawing.Point(15, 273);
            this.listBox_Avsnitt.Name = "listBox_Avsnitt";
            this.listBox_Avsnitt.Size = new System.Drawing.Size(407, 134);
            this.listBox_Avsnitt.TabIndex = 12;
            // 
            // textBox_Beskrivning
            // 
            this.textBox_Beskrivning.Enabled = false;
            this.textBox_Beskrivning.Location = new System.Drawing.Point(443, 273);
            this.textBox_Beskrivning.Multiline = true;
            this.textBox_Beskrivning.Name = "textBox_Beskrivning";
            this.textBox_Beskrivning.Size = new System.Drawing.Size(307, 134);
            this.textBox_Beskrivning.TabIndex = 13;
            // 
            // label_URL
            // 
            this.label_URL.AutoSize = true;
            this.label_URL.Location = new System.Drawing.Point(12, 170);
            this.label_URL.Name = "label_URL";
            this.label_URL.Size = new System.Drawing.Size(32, 13);
            this.label_URL.TabIndex = 14;
            this.label_URL.Text = "URL:";
            // 
            // label_UF
            // 
            this.label_UF.AutoSize = true;
            this.label_UF.Location = new System.Drawing.Point(198, 169);
            this.label_UF.Name = "label_UF";
            this.label_UF.Size = new System.Drawing.Size(114, 13);
            this.label_UF.TabIndex = 15;
            this.label_UF.Text = "Uppdateringsfrekvens:";
            // 
            // label_Kategori_NyPodd
            // 
            this.label_Kategori_NyPodd.AutoSize = true;
            this.label_Kategori_NyPodd.Location = new System.Drawing.Point(329, 169);
            this.label_Kategori_NyPodd.Name = "label_Kategori_NyPodd";
            this.label_Kategori_NyPodd.Size = new System.Drawing.Size(49, 13);
            this.label_Kategori_NyPodd.TabIndex = 16;
            this.label_Kategori_NyPodd.Text = "Kategori:";
            // 
            // label_Vald_Podd
            // 
            this.label_Vald_Podd.AutoSize = true;
            this.label_Vald_Podd.Location = new System.Drawing.Point(14, 246);
            this.label_Vald_Podd.Name = "label_Vald_Podd";
            this.label_Vald_Podd.Size = new System.Drawing.Size(104, 13);
            this.label_Vald_Podd.TabIndex = 17;
            this.label_Vald_Podd.Text = "Ingen podcast vald..";
            // 
            // label_Kategori_NyKategori
            // 
            this.label_Kategori_NyKategori.AutoSize = true;
            this.label_Kategori_NyKategori.Location = new System.Drawing.Point(443, 9);
            this.label_Kategori_NyKategori.Name = "label_Kategori_NyKategori";
            this.label_Kategori_NyKategori.Size = new System.Drawing.Size(58, 13);
            this.label_Kategori_NyKategori.TabIndex = 18;
            this.label_Kategori_NyKategori.Text = "Kategorier:";
            // 
            // Form_Podcast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 419);
            this.Controls.Add(this.label_Kategori_NyKategori);
            this.Controls.Add(this.label_Vald_Podd);
            this.Controls.Add(this.label_Kategori_NyPodd);
            this.Controls.Add(this.label_UF);
            this.Controls.Add(this.label_URL);
            this.Controls.Add(this.textBox_Beskrivning);
            this.Controls.Add(this.listBox_Avsnitt);
            this.Controls.Add(this.TaBort_Kategori);
            this.Controls.Add(this.Spara_Kategori);
            this.Controls.Add(this.Ny_Kategori);
            this.Controls.Add(this.TaBort_Podcast);
            this.Controls.Add(this.comboBox_Kategori);
            this.Controls.Add(this.comboBox_UF);
            this.Controls.Add(this.Spara_Podcast);
            this.Controls.Add(this.Ny_Podcast);
            this.Controls.Add(this.textBox_URL);
            this.Controls.Add(this.textBox_NyKategori);
            this.Controls.Add(this.listBox_Kategori);
            this.Controls.Add(this.ListView_Podcast);
            this.Name = "Form_Podcast";
            this.Text = "Podcasts";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ListView_Podcast;
        private System.Windows.Forms.ListBox listBox_Kategori;
        private System.Windows.Forms.TextBox textBox_NyKategori;
        private System.Windows.Forms.TextBox textBox_URL;
        private System.Windows.Forms.Button Ny_Podcast;
        private System.Windows.Forms.Button Spara_Podcast;
        private System.Windows.Forms.ComboBox comboBox_UF;
        private System.Windows.Forms.ComboBox comboBox_Kategori;
        private System.Windows.Forms.Button TaBort_Podcast;
        private System.Windows.Forms.Button Ny_Kategori;
        private System.Windows.Forms.Button Spara_Kategori;
        private System.Windows.Forms.Button TaBort_Kategori;
        private System.Windows.Forms.ListBox listBox_Avsnitt;
        private System.Windows.Forms.TextBox textBox_Beskrivning;
        private System.Windows.Forms.Label label_URL;
        private System.Windows.Forms.Label label_UF;
        private System.Windows.Forms.Label label_Kategori_NyPodd;
        private System.Windows.Forms.Label label_Vald_Podd;
        private System.Windows.Forms.Label label_Kategori_NyKategori;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}

