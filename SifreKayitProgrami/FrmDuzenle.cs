using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SifreKayitProgrami
{
    public partial class FrmDuzenle : Form
    {
        public FrmDuzenle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=ONUR; initial catalog=SifreProgram; integrated security=true");

        public string id, uygulamaad, sifre, kategori;

        public void Guncelle()
        {
            if (txtAd.Text == "" && txtSifre.Text == "" && cmbKategori.Text == "")
            {
                MessageBox.Show("Lütfen Boş Alanları Doldurunuz.");
            }
            else
            {
                try
                {
                    
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                        SqlCommand guncelle = new SqlCommand("update sifre set UygulamaAd=@p2, Sifre=@p3, Kategori=@p4 where SifreID='" + txtid.Text + "'", baglanti);
                        guncelle.Parameters.AddWithValue("@p2", txtAd.Text);
                        guncelle.Parameters.AddWithValue("@p3", txtSifre.Text);
                        guncelle.Parameters.AddWithValue("@p4", cmbKategori.Text);
                        guncelle.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Bilgiler Güncellendi");
                        this.Hide();
                    }
                }
                catch (Exception hata)
                {

                    MessageBox.Show(hata.Message);
                }

            }
        }

        public void Sil()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                    SqlCommand sil = new SqlCommand("delete from sifre where SifreID=@p1", baglanti);
                    sil.Parameters.AddWithValue("@p1", txtid.Text);
                    sil.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Şifre Bilgisi Başarıyla Silindi");
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Sil();
        }
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            Guncelle();
        }

        private void FrmDuzenle_Load(object sender, EventArgs e)
        {
            txtid.Text = id;
            txtAd.Text = uygulamaad;
            txtSifre.Text = sifre;
            cmbKategori.Text = kategori;
        }
    }
}
