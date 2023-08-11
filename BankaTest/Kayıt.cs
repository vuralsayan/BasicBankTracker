using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BankaTest
{
    public partial class Kayıt : Form
    {
        public Kayıt()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=Vural\SQLEXPRESS;Initial Catalog=DbBanka;Integrated Security=True");


        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKISILER (AD, SOYAD, TC, TELEFON, HESAPNO, SIFRE) VALUES(@P1,@P2,@P3,@P4,@P5,@P6)",baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTC.Text);
            komut.Parameters.AddWithValue("@P4", MskTelefon.Text);
            komut.Parameters.AddWithValue("@P5", MskHesapNo.Text);
            komut.Parameters.AddWithValue("@P6", MskSifre.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarılı Bir Şekilde Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int sayi = rnd.Next(100000, 1000000);    
            MskHesapNo.Text = sayi.ToString();  
        }
    }
}
