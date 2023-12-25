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
    public partial class DoktorDetay : Form
    {
        public DoktorDetay()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();

        public string TC;
        private void DoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;

            // doktor ad soyad çekme

            NpgsqlCommand komut = new NpgsqlCommand("Select \"ad\",\"soyad\" from \"Kisiler\" where \"TC\"=@p1", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@p1", lblTC.Text);

            NpgsqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.db_baglanti().Close();


            // Randevular 

            DataTable dt = new DataTable();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter("Select * from \"Randevular\" where \"DoktorAdSoyad\"='" + lblAdSoyad.Text + "'", bgl.db_baglanti());

            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            Duyurular dy = new Duyurular();
            dy.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            DoktorBilgiDuzenle db = new DoktorBilgiDuzenle();

            db.TCNO = lblTC.Text;
            db.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // datagrid de herhnagi bir hucreye bir kez tıklayınca yan taraftaki randevu detaya hastanın şikayetini getirme
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            rchSikayet.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }
    }
}
