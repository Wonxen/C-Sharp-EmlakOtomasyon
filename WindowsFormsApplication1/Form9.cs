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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        public Form9(string adi, string soyadi)
        {
            InitializeComponent();
            Adi = adi;
            Soyadi = soyadi;
        }
        Form1 F1 = new Form1();
        string Adi, Soyadi;
        private void IlanListele()
        {
            F1.Baglanti.Open();
            OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT * FROM Ilan WHERE Adi='" + Adi + "' AND Soyadi='" + Soyadi + "'", F1.Baglanti);
            DataTable Tablo = new DataTable();
            Yaz.Fill(Tablo);
            dataGridView1.DataSource = Tablo;
            F1.Baglanti.Close();
        }
        private void IlanSayi()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Ilan WHERE Adi='" + Adi + "' AND Soyadi='" + Soyadi + "'", F1.Baglanti);
                OleDbDataReader Oku = Komut.ExecuteReader();
                int Sayi = 0;
                while (Oku.Read())
                {
                    Sayi++;
                }
                label1.Text = "Sistemde kayıtlı " + Sayi + " ilan bilgisi bulunmaktadır.";
                F1.Baglanti.Close();
            }
            catch (Exception)
            {
                label1.Text = "Sistemde kayıtlı 0 ilan bilgisi bulunmaktadır.";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult D = MessageBox.Show(dataGridView1.CurrentRow.Cells[4].Value + " Adresinde bulunan kat numarası " + dataGridView1.CurrentRow.Cells[6].Value + " olan ev ilanını iptal etmek istediğine emin misiniz?", "Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (D == DialogResult.Yes)
                {
                    F1.Baglanti.Open();
                    OleDbCommand Komut = new OleDbCommand("DELETE * FROM Ilan WHERE Adres=@Adres AND IlanNo=@IlanNo", F1.Baglanti);
                    Komut.Parameters.AddWithValue("@IlanNo", dataGridView1.CurrentRow.Cells[0].Value);
                    Komut.ExecuteNonQuery();
                    F1.Baglanti.Close();
                    IlanListele();
                    IlanSayi();
                }
                else
                {

                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form10 F10 = new Form10(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            F10.ShowDialog();
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            IlanListele();
            IlanSayi();
        }
    }
}
