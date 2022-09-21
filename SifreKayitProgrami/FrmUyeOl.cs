using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;

namespace SifreKayitProgrami
{
    public partial class FrmUyeOl : Form
    {
        public FrmUyeOl()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=ONUR; initial catalog=SifreProgram; integrated security=true");
        private void btnUyeOl_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAd.Text != "" && txtAdres.Text != "" && txtMail.Text != "" && txtSifre.Text != "")
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                        SqlCommand ekle = new SqlCommand("insert into uye(AdSoyad,Mail,Sifre,Adres) values(@p1,@p2,@p3,@p4)", baglanti);
                        ekle.Parameters.AddWithValue("@p1", txtAd.Text);
                        ekle.Parameters.AddWithValue("@p2", txtMail.Text);
                        ekle.Parameters.AddWithValue("@p3", txtSifre.Text);
                        ekle.Parameters.AddWithValue("@p4", txtAdres.Text);
                        ekle.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Üye Eklendi");
                        FrmLogin fr = new FrmLogin();
                        fr.Show();
                        this.Hide();
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
    }
}
