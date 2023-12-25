using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Hastane_Proje_VTYS
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HastaGiris giris = new HastaGiris();
            giris.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoktorGiris giris = new DoktorGiris();
            giris.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SekreterGiris giris = new SekreterGiris();
            giris.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PersonelGiris giris = new PersonelGiris();
            giris.Show();
        }
    }
}
