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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        public Form8(string adi, string soyadi)
        {
            InitializeComponent();
            Adi = adi;
            Soyadi = soyadi;
        }
        Form1 F1 = new Form1();
        string Adi, Soyadi, Dosyayolu;
        private void FormOrtala() { this.CenterToScreen(); }
        private void KullaniciBilgisiListele()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM KullaniciHesaplari WHERE Adi='" + Adi + "' AND Soyadi='" + Soyadi + "'",F1.Baglanti);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    textBox1.Text = Oku["KullaniciAdi"].ToString();
                    textBox2.Text = Oku["Adi"].ToString();
                    textBox3.Text = Oku["Soyadi"].ToString();
                    textBox4.Text = Oku["Eposta"].ToString();
                    maskedTextBox1.Text = Oku["DogumTarihi"].ToString();
                    comboBox1.Text = Oku["Cinsiyet"].ToString();
                    pictureBox1.ImageLocation = Oku["ProfilResim"].ToString();
                }
                F1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void BilgileriDuzenle()
        {
            try
            {
                DialogResult D = MessageBox.Show("Bilgilerinizi düzenlemek istediğinize emin misiniz?", "Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (D == DialogResult.Yes)
                {
                    F1.Baglanti.Open();
                    OleDbCommand Komut = new OleDbCommand("UPDATE KullaniciHesaplari SET KullaniciAdi=@KullaniciAdi,Eposta=@Eposta,Adi=@Adi,Soyadi=@Soyadi,DogumTarihi=@DogumTarihi,Cinsiyet=@Cinsiyet,Sifre=@Sifre,ProfilResim=@ProfilResim WHERE Adi='" + Adi + "' AND Soyadi='" + Soyadi + "'", F1.Baglanti);
                    Komut.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                    Komut.Parameters.AddWithValue("@Eposta", textBox4.Text);
                    Komut.Parameters.AddWithValue("@Adi", textBox2.Text);
                    Komut.Parameters.AddWithValue("@Soyadi", textBox3.Text);
                    Komut.Parameters.AddWithValue("@DogumTarihi", maskedTextBox1.Text);
                    Komut.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                    Komut.Parameters.AddWithValue("@Sifre", textBox5.Text);
                    Komut.Parameters.AddWithValue("@ProfilResim", Dosyayolu);
                    Komut.ExecuteNonQuery();
                    MessageBox.Show("Başarıyla bilgileriniz düzenlendi.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    F1.Baglanti.Close();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox5.PasswordChar = '\0';
            }
            else
            {
                textBox5.PasswordChar = '*';
            }
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && maskedTextBox1.Text == "" && comboBox1.Text == "" && textBox5.Text == "")
            {
                MessageBox.Show("Bilgileriniz eksiktir lütfen tekrar deneyin.");
            }
            else
            {
                BilgileriDuzenle();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dosya = new OpenFileDialog();
            Dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png |  Tüm Dosyalar |*.*";
            Dosya.ShowDialog();
            Dosyayolu = Dosya.FileName;
            pictureBox1.ImageLocation = Dosyayolu;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            FormOrtala();
            KullaniciBilgisiListele();
        }
    }
}
