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
    public partial class Hareket : Form
    {
        public Hareket()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=Vural\SQLEXPRESS;Initial Catalog=DbBanka;Integrated Security=True");
        public string hesapNo;

        private void Hareket_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT TBLHAREKET.ID,(GON.AD + ' ' + GON.SOYAD) AS 'Gönderen',      (AL.AD + ' ' + AL.SOYAD) AS 'Alıcı',TUTAR AS 'Tutar' FROM TBLHAREKET INNER JOIN TBLKISILER AS GON ON TBLHAREKET.GONDEREN = GON.HESAPNO INNER JOIN TBLKISILER AS AL ON TBLHAREKET.ALICI = AL.HESAPNO WHERE GONDEREN=" + hesapNo + "ORDER BY TBLHAREKET.ID", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable(); 
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("SELECT TBLHAREKET.ID, (AL.AD + ' ' + AL.SOYAD) AS 'Alıcı', (GON.AD + ' ' + GON.SOYAD) AS 'Gönderen',TUTAR AS 'Tutar' FROM TBLHAREKET INNER JOIN TBLKISILER AS GON ON TBLHAREKET.GONDEREN = GON.HESAPNO INNER JOIN TBLKISILER AS AL ON TBLHAREKET.ALICI = AL.HESAPNO WHERE ALICI=" + hesapNo + "ORDER BY TBLHAREKET.ID", baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
            baglanti.Close();
        }
    }
}
