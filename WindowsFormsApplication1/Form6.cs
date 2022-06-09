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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        Form1 F1 = new Form1();
        private void FormOrtala() { this.CenterToScreen(); }
        private void Form6_Load(object sender, EventArgs e)
        {
            FormOrtala();
            SehirListele();
        }
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                F1.Baglanti.Open();
                OleDbDataAdapter Yaz = new OleDbDataAdapter("SELECT * FROM Ilan WHERE Il='" + comboBox1.Text + "' AND Ilce='" + comboBox2.Text + "'",F1.Baglanti);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            IlceListele();
        }
    }
}
