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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        Form1 F1 = new Form1();
        string Dosyayolu;
        private void FormOrtala() { this.CenterToScreen(); }
        private void KullaniciKayitEt()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("INSERT INTO KullaniciHesaplari(KullaniciAdi,Eposta,Adi,Soyadi,DogumTarihi,Cinsiyet,Sifre,ProfilResim) VALUES (@KullaniciAdi,@Eposta,@Adi,@Soyadi,@DogumTarihi,@Cinsiyet,@Sifre,@ProfilResim)", F1.Baglanti);
                Komut.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                Komut.Parameters.AddWithValue("@Eposta", textBox4.Text);
                Komut.Parameters.AddWithValue("@Adi", textBox2.Text);
                Komut.Parameters.AddWithValue("@Soyadi", textBox3.Text);
                Komut.Parameters.AddWithValue("@DogumTarihi", maskedTextBox1.Text);
                Komut.Parameters.AddWithValue("@Cinsiyet", comboBox1.Text);
                Komut.Parameters.AddWithValue("@Sifre", textBox5.Text);
                Komut.Parameters.AddWithValue("@ProfilResim", Dosyayolu);
                Komut.ExecuteNonQuery();
                MessageBox.Show(textBox2.Text + "" + textBox3.Text + " İsimli kaydınız başarıyla oluşturuldu.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                F1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox5.Text == "")
            {
                MessageBox.Show("Hata: Bilgileri eksik doldurnuz lütfen kontrol edin.");
            }
            else
            {
                KullaniciKayitEt();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            FormOrtala();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                F1.Baglanti.Open();
                ErrorProvider provider = new ErrorProvider();
                OleDbCommand Komut = new OleDbCommand("SELECT COUNT(KullaniciAdi) FROM KullaniciHesaplari WHERE KullaniciAdi=@KullaniciAdi", F1.Baglanti);
                Komut.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                if (Convert.ToInt32(Komut.ExecuteScalar()) > 0)
                {
                    provider.SetError(textBox1, "Kullanıcı Adı Önceden Alınmış.");
                    button1.Enabled = false;
                }
                else
                {
                    provider.Dispose();
                    button1.Enabled = true;
                }
                F1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
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
    }
}
