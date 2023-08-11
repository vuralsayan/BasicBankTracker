using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaTest
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void LnkKayıtOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayıt frm = new Kayıt();    
            frm.Show();
        }
    }
}
