using Npgsql;
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
    public partial class DoktorGiris : Form
    {
        public DoktorGiris()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();
        private void DoktorGiris_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            // doktor giriş yapma 

            NpgsqlCommand komut = new NpgsqlCommand("select \"TC\",\"DrSifre\" from \"Kisiler\" inner join \"Doktor\" on \"Kisiler\".\"kisiID\"=\"Doktor\".\"kisiID\" where \"TC\"=@p1 and \"DrSifre\"=@p2", bgl.db_baglanti());


            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);

            NpgsqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                DoktorDetay dd = new DoktorDetay();

                dd.TC=MskTC.Text;
                dd.Show();
                this.Hide();
            }


            else
            {
                MessageBox.Show("Hatalı TC veya Şifre!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


            bgl.db_baglanti().Close();
        }
    }
}
