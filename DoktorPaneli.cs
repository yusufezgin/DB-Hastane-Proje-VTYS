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
    public partial class DoktorPaneli : Form
    {
        public DoktorPaneli()
        {
            InitializeComponent();
        }



        baglanti bgl = new baglanti();
        private void DoktorDetay_Load(object sender, EventArgs e)
        {
            // Doktorları datagride çekme

            DataTable dt1 = new DataTable();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("select \"Kisiler\".\"kisiID\", \"ad\" ,\"soyad\",\"TC\",\"BransAd\",\"DrSifre\" from \"Kisiler\" inner join \"Doktor\" on \"Doktor\".\"kisiID\"=\"Kisiler\".\"kisiID\" inner join \"Branslar\" on \"Doktor\".\"BransID\"=\"Branslar\".\"BransID\" order by \"Kisiler\".\"kisiID\"", bgl.db_baglanti());

            da1.Fill(dt1);

            dataGridView1.DataSource = dt1;


          
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
                       
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
