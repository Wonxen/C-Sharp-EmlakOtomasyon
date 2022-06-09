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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }
        public Form10(string no)
        {
            InitializeComponent();
            No = no;
        }
        string No;
        Form1 F1 = new Form1();
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
            OleDbCommand Komut = new OleDbCommand("SELECT * FROM SehirIlce WHERE Sehir='" + comboBox1.Text + "'", F1.Baglanti);
            OleDbDataReader Oku = Komut.ExecuteReader();
            while (Oku.Read())
            {
                comboBox2.Items.Add(Oku["Ilce"].ToString());
            }
        }
        private void TurListele()
        {
            F1.Baglanti.Open();
            OleDbCommand Komut = new OleDbCommand("SELECT * FROM Tur", F1.Baglanti);
            OleDbDataReader Oku = Komut.ExecuteReader();
            while (Oku.Read())
            {
                comboBox3.Items.Add(Oku["Tur"].ToString());
            }
            F1.Baglanti.Close();
        }
        private void EvBilgiListele()
        {
            try
            {
                F1.Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM Ilan WHERE IlanNo=@IlanNo", F1.Baglanti);
                Komut.Parameters.AddWithValue("@IlanNo", No);
                OleDbDataReader Oku = Komut.ExecuteReader();
                while (Oku.Read())
                {
                    label6.Text = Oku["IlanNo"].ToString() + " Nolu İlan Bilgisi";
                    comboBox1.Text = Oku["Il"].ToString();
                    IlceListele();
                    comboBox2.Text = Oku["Ilce"].ToString();
                    textBox1.Text = Oku["Adres"].ToString();
                    if (Oku["EvDurumu"].ToString() == "Aktif Ev")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (Oku["EvDurumu"].ToString() == "Pasif Ev")
                    {
                        radioButton2.Checked = true;
                    }
                    else
                    {
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                    }
                    textBox2.Text = Oku["KatNumarasi"].ToString();
                    textBox3.Text = Oku["EvToplamAlan"].ToString();
                    textBox4.Text = Oku["EvOdaSayisi"].ToString();
                    comboBox3.Text = Oku["EvTuru"].ToString();
                    dateTimePicker1.Text = Oku["EvYapimTarihi"].ToString();
                    textBox5.Text = Oku["EvYasi"].ToString();
                    if (Oku["EvTercihi"].ToString() == "Satilik Ev")
                    {
                        radioButton3.Checked = true;
                        groupBox5.Text = "Satılık Ev";
                        label12.Visible = false;
                        label13.Visible = false;
                        label14.Visible = true;
                        textBox6.Visible = false;
                        textBox7.Visible = false;
                        textBox8.Visible = true;
                        textBox8.Text = Oku["Fiyat"].ToString();
                        radioButton4.Enabled = false;
                    }
                    else if (Oku["EvTercihi"].ToString() == "Kiralik Ev")
                    {
                        radioButton4.Checked = true;
                        groupBox5.Text = "Kiralık Ev";
                        label12.Visible = true;
                        label13.Visible = true;
                        label14.Visible = false;
                        textBox6.Visible = true;
                        textBox7.Visible = true;
                        textBox8.Visible = false;
                        textBox6.Text = Oku["Kira"].ToString();
                        textBox7.Text = Oku["Depozito"].ToString();
                        radioButton3.Enabled = false;
                    }
                    else
                    {
                        radioButton3.Checked = false;
                        radioButton4.Checked = false;
                    }
                }
                F1.Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Durum = "";
                if (radioButton1.Checked == true)
                    Durum = "Aktif Ev";
                if (radioButton2.Checked == true)
                    Durum = "Pasif Ev";
                if (radioButton3.Checked == true)
                {
                    DialogResult D = MessageBox.Show(No + " Numaralı ilanı düzenlemek istediğinize emin misiniz?", "Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (D == DialogResult.Yes)
                    {
                        F1.Baglanti.Open();
                        OleDbCommand Komut = new OleDbCommand("UPDATE Ilan SET Fiyat=@Fiyat,EvDurumu=@EvDurumu WHERE IlanNo='" + No + "'", F1.Baglanti);
                        Komut.Parameters.AddWithValue("@EvDurumu", Durum);
                        Komut.Parameters.AddWithValue("@Fiyat", textBox8.Text);
                        Komut.ExecuteNonQuery();
                        F1.Baglanti.Close();
                    }
                    else
                    {
                        MessageBox.Show("İlan düzenleme işlemi iptal edilmiştir.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else if (radioButton4.Checked == true)
                {
                    DialogResult D = MessageBox.Show(No + " Numaralı ilanı düzenlemek istediğinize emin misiniz?", "Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (D == DialogResult.Yes)
                    {
                        F1.Baglanti.Open();
                        OleDbCommand Komut = new OleDbCommand("UPDATE Ilan SET Kira=@Kira,Depozito=@Depozito,EvDurumu=@EvDurumu WHERE IlanNo='" + No + "'", F1.Baglanti);
                        Komut.Parameters.AddWithValue("@EvDurumu", Durum);
                        Komut.Parameters.AddWithValue("@Kira", textBox6.Text);
                        Komut.Parameters.AddWithValue("@Depozito", textBox7.Text);
                        Komut.ExecuteNonQuery();
                        F1.Baglanti.Close();
                    }
                    else
                    {
                        MessageBox.Show("İlan düzenleme işlemi iptal edilmiştir.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            TurListele();
            SehirListele();
            EvBilgiListele();
        }
    }
}
