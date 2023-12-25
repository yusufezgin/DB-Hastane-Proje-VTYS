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
    public partial class PersonelDetay : Form
    {
        public PersonelDetay()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();
        private void PersonelDetay_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("select \"ad\",\"soyad\",\"TC\",\"PerSifre\",\"PerCinsiyet\" from \"Personel\" inner join \"Kisiler\" on \"Kisiler\".\"kisiID\"=\"Personel\".\"kisiID\"", bgl.db_baglanti());

            da2.Fill(dt2);

            dataGridView1.DataSource = dt2;
        }
    }
}
