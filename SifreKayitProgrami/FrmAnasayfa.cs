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
    public partial class FrmAnasayfa : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=ONUR; initial catalog=SifreProgram; integrated security=true");
        SqlConnection baglanti2 = new SqlConnection("Data Source=ONUR; initial catalog=SifreProgram; integrated security=true");
        public FrmAnasayfa()
        {
            InitializeComponent();
        }

        public void Goster()
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                    string goster = "select SifreID,UygulamaAd,Sifre,Kategori from sifre where UyeID='" + lblid.Text + "'";
                    SqlCommand komut = new SqlCommand(goster, baglanti);
                    SqlDataAdapter adap = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;
                    baglanti.Close();
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show("HATA OLUŞTU", hata.Message);
            }
        }

        public void SifreKaydet()
        {
            try
            {
                if (baglanti2.State == ConnectionState.Closed)
                {
                    baglanti2.Open();
                    SqlCommand tekrar = new SqlCommand("select count(*) from sifre where UygulamaAd= '" + txtAd.Text + "'", baglanti2);
                    int sonuc = (int)tekrar.ExecuteScalar();
                    if (sonuc == 0)
                    {
                        try
                        {
                            if (baglanti.State == ConnectionState.Closed)
                            {
                                if (txtAd.Text == "" && txtSifre.Text == "" && cmbKategori.Text == "")
                                {
                                    MessageBox.Show("Lütfen Boş Alanları Doldurunuz.");
                                }
                                else
                                {
                                    baglanti.Open();
                                    SqlCommand ekle = new SqlCommand("insert into sifre (UygulamaAd,Sifre,Kategori,UyeID) values (@p1,@p2,@p3,@p4)", baglanti);
                                    ekle.Parameters.AddWithValue("@p1", txtAd.Text);
                                    ekle.Parameters.AddWithValue("@p2", txtSifre.Text);
                                    ekle.Parameters.AddWithValue("@p3", cmbKategori.Text);
                                    ekle.Parameters.AddWithValue("@p4", lblid.Text);
                                    ekle.ExecuteNonQuery();
                                    baglanti.Close();
                                    MessageBox.Show("Kayıt Eklendi");
                                }
                            }
                        }
                        catch (Exception hata)
                        {

                            MessageBox.Show(hata.Message);
                        }
                    }
                    if (sonuc > 0)
                    {
                        MessageBox.Show(txtAd.Text + " Adlı Bir Şifre Mevcut");
                    }
                    baglanti2.Close();
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        }
        public string adsoyad { get; set; }
        public string id { get; set; }
        private void FrmAnasayfa_Load(object sender, EventArgs e)
        {
            lblKullaniciAd.Text = adsoyad;
            lblid.Text = id;
            Goster();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SifreKaydet();
            Goster();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FrmDuzenle fr = new FrmDuzenle();
            fr.id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            fr.uygulamaad = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            fr.sifre = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            fr.kategori = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            fr.ShowDialog();
        }

        private void cmbGoruntule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                    string goster = "select SifreID,UygulamaAd,Sifre,Kategori from sifre where Kategori='" + cmbGoruntule.Text + "'";
                    SqlCommand komut = new SqlCommand(goster, baglanti);
                    SqlDataAdapter adap = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    adap.Fill(dt);
                    dataGridView1.DataSource = dt;
                    baglanti.Close();
                }
            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }


        }
    }
}
