using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SifreKayitProgrami
{
    public partial class FrmSifremiUnuttum : Form
    {
        public FrmSifremiUnuttum()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=ONUR; initial catalog=SifreProgram; integrated security=true");
        public bool MailGonder(string konu, string icerik)
        {
            MailMessage eposta = new MailMessage();
            eposta.From = new MailAddress("maildenemesi00@gmail.com");
            eposta.To.Add(txtMail.Text);

            eposta.Subject = konu;
            eposta.Body = icerik;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("maildenemesi00@gmail.com", "denememail");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(eposta);
            object userstate = true;
            bool kontrol = true;
            try
            {
                client.SendAsync(eposta, (object)eposta);
            }
            catch (SmtpException ex)
            {

                kontrol = false;
                MessageBox.Show(ex.Message);
            }
            return kontrol;
        }

        string sifre;
        private void btnGonder_Click(object sender, EventArgs e)
        {
            //Mail Gönderme Hatası Var.
            try
            {
                if (txtMail.Text == "")
                {
                    MessageBox.Show("Lütfen Alanları Doldurunuz.");
                }
                else
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("select * from uye where Mail=@p1", baglanti);
                        komut.Parameters.AddWithValue("@p1", txtMail.Text);
                        SqlDataReader dr = komut.ExecuteReader();
                        if (dr.Read())
                        {
                            sifre = dr["Sifre"].ToString();
                            MailGonder("ŞİFRE HATIRLATMA", "Şifreniz: " + sifre);
                            baglanti.Close();
                            MessageBox.Show("Mail Adresinine Sifreniz Gönderilmiştir.");
                        }
                        else
                        {
                            MessageBox.Show("Böyle Bir Mail Adresi Bulunmamatadır.");
                        }
                    }
                }

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }

        }
    }
}
