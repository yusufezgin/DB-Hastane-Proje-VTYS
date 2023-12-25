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
    public partial class PersonelGiris : Form
    {
        public PersonelGiris()
        {
            InitializeComponent();
        }
        baglanti bgl = new baglanti();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            // personel giriş yapma 

            NpgsqlCommand komut = new NpgsqlCommand("select \"TC\",\"PerSifre\" from \"Kisiler\" inner join \"Personel\" on \"Kisiler\".\"kisiID\"=\"Personel\".\"kisiID\" where \"TC\"=@p1 and \"PerSifre\"=@p2", bgl.db_baglanti());


            komut.Parameters.AddWithValue("@p1", MskTC.Text);
            komut.Parameters.AddWithValue("@p2", txtSifre.Text);

            NpgsqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                PersonelDetay pd = new PersonelDetay();

                pd.Show();
                this.Hide();
            }


            else
            {
                MessageBox.Show("Hatalı TC veya Şifre!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }


            bgl.db_baglanti().Close();
        }

        private void PersonelGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
