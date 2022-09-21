using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Data.SqlClient;

namespace SifreKayitProgrami
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=ONUR; initial catalog=SifreProgram; integrated security=true");
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                lblKontrol.Text = "İnternet Bağlantısı Var";
                lblKontrol.ForeColor = Color.Green;
                btnGiris.Enabled = true;
            }
            else
            {
                lblKontrol.Text = "İnternet Bağlantısı Mevcut Değil";
                lblKontrol.ForeColor = Color.Red;
                btnGiris.Enabled = false;
            }
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtKullaniciAd.Text != "" && txtSifre.Text != "")
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                        SqlCommand giris = new SqlCommand("select * from uye where Mail=@p1 AND Sifre=@p2", baglanti);
                        giris.Parameters.AddWithValue("@p1", txtKullaniciAd.Text);
                        giris.Parameters.AddWithValue("@p2", txtSifre.Text);
                        SqlDataReader dr = giris.ExecuteReader();
                        if (dr.Read())
                        {
                            FrmAnasayfa ana = new FrmAnasayfa();
                            ana.adsoyad = dr["AdSoyad"].ToString();
                            ana.id = dr["UyeID"].ToString();
                            ana.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Adı veya Şifre Yanlış.");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen Boş Alanları Doldurunuz.");
                }


            }
            catch (Exception hata)
            {

                MessageBox.Show("HATA OLUŞTU", hata.Message);
            }

        }

        private void linkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmUyeOl uye = new FrmUyeOl();
            uye.Show();
            this.Hide();
        }

        private void linkSifremiUnuttum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSifremiUnuttum fr = new FrmSifremiUnuttum();
            fr.Show();
            this.Hide();
        }
    }
}
