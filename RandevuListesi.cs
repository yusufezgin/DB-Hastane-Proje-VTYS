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
    public partial class RandevuListesi : Form
    {
        public RandevuListesi()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();
        private void RandevuListesi_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Randevular\"", bgl.db_baglanti());

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }
    }
}
