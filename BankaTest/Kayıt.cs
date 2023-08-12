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

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            // Random hesap numarası oluşturma 
            Random rnd = new Random();
            string yeniNumara;

            do
            {
                int rastgeleSayi = rnd.Next(100000, 1000000);
                yeniNumara = rastgeleSayi.ToString();
                MskHesapNo.Text = yeniNumara;

            } while (IsNumberUsed(yeniNumara, baglanti));

            // Hesap numarası kullanılmış mı kontrolü
            bool IsNumberUsed(string numara, SqlConnection baglanti)
            {
                using (SqlCommand komut2 = new SqlCommand("SELECT COUNT(*) FROM TBLKISILER WHERE HESAPNO = @P1", baglanti))
                {
                    baglanti.Open();
                    komut2.Parameters.AddWithValue("@P1", numara);
                    int count = (int)komut2.ExecuteScalar();
                    baglanti.Close();
                    return count > 0; // count > 0 ise true döndürür.
                }
            }
        }


        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKISILER (AD, SOYAD, TC, TELEFON, HESAPNO, SIFRE) VALUES(@P1,@P2,@P3,@P4,@P5,@P6)", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTC.Text);
            komut.Parameters.AddWithValue("@P4", MskTelefon.Text);
            komut.Parameters.AddWithValue("@P5", MskHesapNo.Text);
            komut.Parameters.AddWithValue("@P6", MskSifre.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            //TBLHESAP tablosuna HESAPNO değerini ekleme    
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO TBLHESAP (HESAPNO) VALUES(@P1)", baglanti);
            komut2.Parameters.AddWithValue("@P1", MskHesapNo.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarılı Bir Şekilde Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
