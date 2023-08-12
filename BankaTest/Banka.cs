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
    public partial class Banka : Form
    {
        public Banka()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=Vural\SQLEXPRESS;Initial Catalog=DbBanka;Integrated Security=True");

        public string hesap;

        private void Banka_Load(object sender, EventArgs e)
        {
            LblHesapNo.Text = hesap;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLKISILER WHERE HESAPNO=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", hesap);    
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2]; //" " ifadesi olduğu için tostring yapmaya gerek yok
                LblTC.Text = dr[3].ToString();
                LblTelefon.Text = dr[4].ToString();
            }
            baglanti.Close();
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            //Gönderilen hesabın bakiyesini arttırma
            baglanti.Open();    
            SqlCommand komut = new SqlCommand("UPDATE TBLHESAP SET BAKIYE=BAKIYE+@p1 WHERE HESAPNO=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@p2", MskHesapNo.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Para Gönderme İşlemi Gerçekleşti");

            //Gönderen hesabın bakiyesini azaltma
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("UPDATE TBLHESAP SET BAKIYE=BAKIYE-@p1 WHERE HESAPNO=@p2", baglanti);
            komut2.Parameters.AddWithValue("@p1", decimal.Parse(TxtTutar.Text));
            komut2.Parameters.AddWithValue("@p2", LblHesapNo.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Para Çekme İşlemi Gerçekleşti");

        }

        private void BtnHareket_Click(object sender, EventArgs e)
        {
            Hareket hrkt = new Hareket();   
            hrkt.hesapNo = LblHesapNo.Text;
            hrkt.Show();
        }
    }
}
