using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(string adi, string soyadi)
        {
            InitializeComponent();
            Adi = adi;
            Soyadi = soyadi;
        }
        Form1 F1 = new Form1();
        string Adi, Soyadi;
        private void FormOrtala() { this.CenterToScreen(); }
        private void SehirListele()
        {
            F1.Baglanti.Open();
            OleDbCommand Komut = new OleDbCommand("SELECT DISTINCT Sehir FROM SehirIlce", F1.Baglanti);
            OleDbDataReader Oku = Komut.ExecuteReader();
            while (Oku.Read())
            {
                comboBox1.Items.Add(Oku["Sehir"].ToString());
            }
            F1.Baglanti.Close();
        }
        private void IlceListele()
        {
            F1.Baglanti.Open();
            OleDbCommand Komut = new OleDbCommand("SELECT * FROM SehirIlce WHERE Sehir='" + comboBox1.Text + "'", F1.Baglanti);
            OleDbDataReader Oku = Komut.ExecuteReader();
            while (Oku.Read())
            {
                comboBox2.Items.Add(Oku["Ilce"].ToString());
            }
            F1.Baglanti.Close();
        }
        private void EvYasHesapla()
        {
            TimeSpan T = DateTime.Now - Convert.ToDateTime(dateTimePicker1.Text);
            textBox5.Text = T.Days.ToString();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            FormOrtala();
            SehirListele();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            IlceListele();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            EvYasHesapla();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox5.Text = "Satılık Ev";
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = true;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox5.Text = "Kiralık Ev";
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = false;
            textBox6.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = false;
        }
        private void Temizlik()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Durum = "";
            if (radioButton1.Checked == true)
                Durum = "Aktif Ev";
            if (radioButton2.Checked == true)
                Durum = "Pasif Ev";
            string Tercih = "";
            if (radioButton3.Checked == true)
                Tercih = "Satilik Ev";
            if (radioButton4.Checked == true)
                Tercih = "Kiralik Ev";
            try
            {
                DialogResult D = MessageBox.Show("Yapım tarihi " + dateTimePicker1.Text + " olan " + comboBox1.Text + " ilinde " + comboBox2.Text + " ilçesinde bulunan ev ilanı sisteme ekleniyor onaylıyormusunuz?", "Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    F1.Baglanti.Open();
                    OleDbCommand Komut = new OleDbCommand("INSERT INTO Ilan(Adi,Soyadi,Il,Ilce,Adres,EvDurumu,KatNumarasi,EvToplamAlan,EvOdaSayisi,EvTuru,EvYapimTarihi,EvYasi,EvTercihi,Kira,Depozito,Fiyat) VALUES (@Adi,@Soyadi,@Il,@Ilce,@Adres,@EvDurumu,@KatNumarasi,@EvToplamAlan,@EvOdaSayisi,@EvTuru,@EvYapimTarihi,@EvYasi,@EvTercihi,@Kira,@Depozito,@Fiyat)", F1.Baglanti);
                    Komut.Parameters.AddWithValue("@Adi", Adi);
                    Komut.Parameters.AddWithValue("@Soyadi", Soyadi);
                    Komut.Parameters.AddWithValue("@Il", comboBox1.Text);
                    Komut.Parameters.AddWithValue("@Ilce", comboBox2.Text);
                    Komut.Parameters.AddWithValue("@Adres", textBox1.Text);
                    Komut.Parameters.AddWithValue("@EvDurumu", Durum);
                    Komut.Parameters.AddWithValue("@KatNumarasi", textBox2.Text);
                    Komut.Parameters.AddWithValue("@EvToplamAlan", textBox3.Text);
                    Komut.Parameters.AddWithValue("@EvOdaSayisi", textBox4.Text);
                    Komut.Parameters.AddWithValue("@EvTuru", comboBox3.Text);
                    Komut.Parameters.AddWithValue("@EvYapimTarihi", dateTimePicker1.Text);
                    Komut.Parameters.AddWithValue("@EvYasi", textBox5.Text);
                    Komut.Parameters.AddWithValue("@EvTercihi", Tercih);
                    Komut.Parameters.AddWithValue("@Kira", textBox6.Text);
                    Komut.Parameters.AddWithValue("@Depozito", textBox7.Text);
                    Komut.Parameters.AddWithValue("@Fiyat", textBox8.Text);
                    Komut.ExecuteNonQuery();
                    Temizlik();
                    MessageBox.Show("İlan ekleme işlemi başarıyla gerçekleştirilmiştir.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    F1.Baglanti.Close();
                }
                else
                {
                    Temizlik();
                    F1.Baglanti.Close();
                    MessageBox.Show("İlan ekleme işlemi başarıyla iptal edilmiştir.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
    }
}
