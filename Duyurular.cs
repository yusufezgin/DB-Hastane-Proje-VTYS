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
    public partial class Duyurular : Form
    {
        public Duyurular()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();
        private void Duyurular_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Duyurular\"", bgl.db_baglanti());

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
