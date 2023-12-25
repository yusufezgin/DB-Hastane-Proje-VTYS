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
    public partial class Branslar : Form
    {
        public Branslar()
        {
            InitializeComponent();
        }

        public void listeleme()
        {
            // Form yüklendiginde datagride branslarin gelmesi 


            DataTable dt = new DataTable();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Branslar\"", bgl.db_baglanti());

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }
        baglanti bgl = new baglanti();
        private void Branslar_Load(object sender, EventArgs e)
        {
            listeleme();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Branş ekleme

            NpgsqlCommand komut = new NpgsqlCommand("insert into \"Branslar\" (\"BransAd\") values (@p1)", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@p1", txtBransAd.Text);
            komut.ExecuteNonQuery();
            listeleme();
            bgl.db_baglanti().Close();
            MessageBox.Show("Branş Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Branş silme

            NpgsqlCommand komut = new NpgsqlCommand("delete from \"Branslar\" where \"BransID\"=@p1", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@p1", int.Parse(txtBransID.Text));
            komut.ExecuteNonQuery();
            listeleme();
            bgl.db_baglanti().Close();
            MessageBox.Show("Branş Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // Branş güncelleme

            NpgsqlCommand komut = new NpgsqlCommand("update \"Branslar\" set \"BransAd\"=@p1 where \"BransID\"=@p2", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@p1", txtBransAd.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(txtBransID.Text));
            komut.ExecuteNonQuery();
            listeleme();
            bgl.db_baglanti().Close();

            MessageBox.Show("Branş Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
