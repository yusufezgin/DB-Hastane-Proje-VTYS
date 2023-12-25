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
    public partial class SekreterGiris : Form
    {
        public SekreterGiris()
        {
            InitializeComponent();
        }


        baglanti bgl = new baglanti();
        private void SekreterGiris_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            // sekreter giriş yapma 

            NpgsqlCommand komut = new NpgsqlCommand("select \"TC\",\"SekSifre\" from \"Kisiler\" inner join \"Sekreterler\" on \"Kisiler\".\"kisiID\"=\"Sekreterler\".\"kisiID\" where \"TC\"=@p1 and \"SekSifre\"=@p2", bgl.db_baglanti());


            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);

            NpgsqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                SekreterDetay sd = new SekreterDetay();
                sd.TCnumara = MskTC.Text;
                sd.Show();
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
