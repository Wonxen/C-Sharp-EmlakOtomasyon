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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        Form1 F1 = new Form1();
        private void FormOrtala() { this.CenterToScreen(); }
        private void IlanListele()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT * FROM Ilan",F1.Baglanti);
                DataTable Tablo = new DataTable();
                Yaz.Fill(Tablo);
                dataGridView1.DataSource = Tablo;
                F1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            IlSayi();
            IlanSayi();
            IlceSayi();
            FormOrtala();
            IlanListele();
            KullaniciSayi();
            KullaniciListele();
            SehirIlceListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("DELETE * FROM Ilan WHERE IlanNo=@IlanNo", F1.Baglanti);
                Komut.Parameters.AddWithValue("@IlanNo", dataGridView1.CurrentRow.Cells[0].Value);
                Komut.ExecuteNonQuery();
                F1.Baglanti.Close();
                IlanListele();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void IlanSayi()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Ilan", F1.Baglanti);
                OleDbDataReader Oku = Komut.ExecuteReader();
                int Sayi = 0;
                while (Oku.Read())
                {
                    Sayi++;
                }
                label2.Text = "Sistemde kayıtlı " + Sayi + " ilan bilgisi bulunmaktadır.";
                F1.Baglanti.Close();
            }
            catch (Exception)
            {
                label2.Text = "Sistemde kayıtlı 0 ilan bilgisi bulunmaktadır.";
            }
        }

        private void KullaniciListele()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT * FROM KullaniciHesaplari", F1.Baglanti);
                DataTable Tablo = new DataTable();
                Yaz.Fill(Tablo);
                dataGridView2.DataSource = Tablo;
                F1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void KullaniciSayi()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM KullaniciHesaplari", F1.Baglanti);
                OleDbDataReader Oku = Komut.ExecuteReader();
                int Sayi = 0;
                while (Oku.Read())
                {
                    Sayi++;
                }
                label4.Text = "Sistemde kayıtlı " + Sayi + " kullanıcı bulunmaktadır.";
                F1.Baglanti.Close();
            }
            catch (Exception)
            {
                label4.Text = "Sistemde kayıtlı 0 kullanıcı bulunmaktadır.";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show(dataGridView2.CurrentRow.Cells[0].Value + " Kullanıcı adlı kayıdı silmek istediğinize emin misiniz?", "Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (D == DialogResult.Yes)
                {
                    F1.Baglanti.Open();
                    OleDbCommand Komut = new OleDbCommand("DELETE * FROM KullaniciHesaplari WHERE KullaniciAdi=@KullaniciAdi", F1.Baglanti);
                    Komut.Parameters.AddWithValue("@KullaniciAdi", dataGridView2.CurrentRow.Cells[0].Value);
                    Komut.ExecuteNonQuery();
                    F1.Baglanti.Close();
                    KullaniciListele();
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edili.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void SehirIlceListele()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT DISTINCT Sehir FROM SehirIlce", F1.Baglanti);
                DataTable Tablo = new DataTable();
                Yaz.Fill(Tablo);
                dataGridView3.DataSource = Tablo;
                F1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void IlSayi()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT DISTINCT Sehir FROM SehirIlce", F1.Baglanti);
                OleDbDataReader Oku = Komut.ExecuteReader();
                int Sayi = 0;
                while (Oku.Read())
                {
                    Sayi++;
                }
                label6.Text = "Sistemde kayıtlı " + Sayi + " il bulunmaktadır.";
                F1.Baglanti.Close();
            }
            catch (Exception)
            {
                label6.Text = "Sistemde kayıtlı 0 il bulunmaktadır.";
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                F1.Baglanti.Open();
                OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT Ilce FROM SehirIlce WHERE Sehir='" + dataGridView3.CurrentRow.Cells[0].Value + "'", F1.Baglanti);
                DataTable Tablo = new DataTable();
                Yaz.Fill(Tablo);
                dataGridView4.DataSource = Tablo;
                F1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void IlceSayi()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT DISTINCT Ilce FROM SehirIlce", F1.Baglanti);
                OleDbDataReader Oku = Komut.ExecuteReader();
                int Sayi = 0;
                while (Oku.Read())
                {
                    Sayi++;
                }
                label7.Text = "Sistemde kayıtlı " + Sayi + " ilçe bulunmaktadır.";
                F1.Baglanti.Close();
            }
            catch (Exception)
            {
                label7.Text = "Sistemde kayıtlı 0 ilçe bulunmaktadır.";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DialogResult D = MessageBox.Show(dataGridView3.CurrentRow.Cells[0].Value + " il kayıdını silmek istediğinize emin misiniz?", "Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (D == DialogResult.Yes)
                    {
                        F1.Baglanti.Open();
                        OleDbCommand Komut = new OleDbCommand("DELETE * FROM SehirIlce WHERE Sehir=@Sehir", F1.Baglanti);
                        Komut.Parameters.AddWithValue("@Sehir", dataGridView3.CurrentRow.Cells[0].Value);
                        Komut.ExecuteNonQuery();
                        F1.Baglanti.Close();
                        SehirIlceListele();
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi iptal edili.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                catch (Exception Hata)
                {
                    MessageBox.Show(Hata.Message);
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
    }
}
