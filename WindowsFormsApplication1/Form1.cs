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
using System.Net.Mail;
using System.Net;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public OleDbConnection Baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=EmlakOtomasyon.accdb");
        private void FormOrtala() { this.CenterToScreen(); }
        private void KullaniciGiris()
        {
            try
            {
                Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM KullaniciHesaplari WHERE KullaniciAdi='" + textBox1.Text + "' AND Sifre='" + textBox2.Text + "'",Baglanti);
                OleDbDataReader Oku = Komut.ExecuteReader();
                if (Oku.Read()) 
                {
                    Form2 F2 = new Form2(textBox1.Text, textBox2.Text);
                    F2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifreniz yanlış.");
                }
                Baglanti.Close();
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "Admin" && textBox2.Text == "1234")
                {
                    Form3 F3 = new Form3();
                    F3.Show();
                    this.Hide();
                }
                else
                {
                    KullaniciGiris();
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormOrtala();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 F4 = new Form4();
            F4.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            groupBox3.Visible = true;
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Baglanti.Open();
                OleDbCommand Komut = new OleDbCommand("SELECT * FROM KullaniciHesaplari WHERE KullaniciAdi=@KullaniciAdi", Baglanti);
                Komut.Parameters.AddWithValue("@KullaniciAdi", textBox3.Text);
                OleDbDataReader Oku = Komut.ExecuteReader();
                if (Oku.Read())
                {
                    try
                    {
                        DialogResult D = MessageBox.Show(textBox3.Text + " Kullanıcı adına sahip biligleri metin belgenize gönderilicek onaylıyor musunuz?", "Emlak", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (D == DialogResult.Yes)
                        {
                            using (System.IO.StreamWriter Dosya = new System.IO.StreamWriter("ŞifremiUnuttum.txt"))
                            {
                                String Metin = ("Merhaba " + Oku["Adi"].ToString() + " " + Oku["Soyadi"].ToString() + "\n\nŞifrenizi gönderdiniz ve şifrenizi göndermek için bir metin belgesi gönderildi.\nKullanıcı Adınız ve Şifreniz aşağıda belirtilmektedir." + "\n\nKullanıcı Adı: " + Oku["KullaniciAdi"].ToString() + "\nŞifreniz: " + Oku["Sifre"].ToString() + "\n\nGüvenliğiniz için giriş yaptıktan sonra lütfen hemen şifrenizi değiştirin." + "\n\nEMLAK EKİBİNİZ");
                                Dosya.WriteLine(Metin);
                            }
                            MessageBox.Show("Başarıyla metin belgesine bilgileriniz gönderilmiştir.\nGüvenliğiniz için giriş yaptıktan sonra lütfen şifrenizi değiştirin.", "Emlak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            textBox3.Clear();
                        }
                    }
                    catch (Exception Hata)
                    {
                        MessageBox.Show(Hata.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı bilgisi bulunamadı veya eksik girildi.", "Emlak");
                }
            }
            catch (Exception Hata)
            {
                MessageBox.Show(Hata.Message);
            }
            Baglanti.Close();
        }
    }
}

// otomasyon.gonderim@outlook.com
// otomasyon.mail.deneme@gmail.com