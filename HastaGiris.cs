using Npgsql;
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

namespace DB_Hastane_Proje_VTYS
{
    public partial class HastaGiris : Form
    {
        public HastaGiris()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();
        private void HastaGiris_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            // hasta giriş yapma 
          
            NpgsqlCommand komut = new NpgsqlCommand("select \"TC\",\"HastaSifre\" from \"Kisiler\" inner join \"Hastalar\" on \"Kisiler\".\"kisiID\"=\"Hastalar\".\"kisiID\" where \"TC\"=@p1 and \"HastaSifre\"=@p2", bgl.db_baglanti());


            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);

            NpgsqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                HastaDetay hd = new HastaDetay();
                hd.tc = MskTC.Text;
                hd.Show();
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
