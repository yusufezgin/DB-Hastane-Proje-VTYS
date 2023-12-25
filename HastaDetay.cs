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
    public partial class HastaDetay : Form
    {
        public HastaDetay()
        {
            InitializeComponent();
        }

        baglanti bgl = new baglanti();

        public string tc;
        private void HastaDetay_Load(object sender, EventArgs e)
        {
            // TC çekme
            lblTC.Text = tc;


            //  veri tabanından ad-soyad çekme

            NpgsqlCommand komut = new NpgsqlCommand("select \"ad\",\"soyad\" from \"Kisiler\" where \"TC\"=@p1", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@p1", lblTC.Text);


            NpgsqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {

                lblAdSoyad.Text = dr[0] + " " + dr[1];

            }
            bgl.db_baglanti().Close();


            //veritabanından randevu geçmişi çekme

            DataTable dt = new DataTable();

            // hastanın tc sine göre çektik 
            NpgsqlCommand komut5 = new NpgsqlCommand("Select * From \"Randevular\" where \"HastaTC\"=@p1", bgl.db_baglanti());

            komut5.Parameters.AddWithValue("@p1", tc);

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut5);

            da.Fill(dt); 

            dataGridView1.DataSource = dt; 


            // Branşları çekme

            NpgsqlCommand komut2 = new NpgsqlCommand("Select \"BransAd\" from \"Branslar\"", bgl.db_baglanti());

            NpgsqlDataReader dr2 = komut2.ExecuteReader();

            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }

            bgl.db_baglanti().Close();

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Branş seçince o bransın doktorlarını altta ki comboboxa getirme

            cmbDoktor.Items.Clear();
            NpgsqlCommand komut3 = new NpgsqlCommand("Select \"ad\",\"soyad\" from \"Doktor\" inner join \"Kisiler\" on \"Kisiler\".\"kisiID\"=\"Doktor\".\"kisiID\" inner join \"Branslar\" on \"Doktor\".\"BransID\"=\"Branslar\".\"BransID\" where \"BransAd\"=@p1", bgl.db_baglanti());
           
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);

            NpgsqlDataReader dr3 = komut3.ExecuteReader();

            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);

            }
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Doktoru da seçince aktif randevuları secilen brans ve doktora göre datagrid2 de görüntüleme

            DataTable dt2 = new DataTable();

            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("Select * from \"Randevular\" where \"BransAd\"='" + cmbBrans.Text + "'" + "and \"DoktorAdSoyad\"='" + cmbDoktor.Text + "' and \"RanDurum\"=true", bgl.db_baglanti());


            da2.Fill(dt2);

            dataGridView2.DataSource = dt2;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Randevu alma

            NpgsqlCommand komut = new NpgsqlCommand("update \"Randevular\" set \"RanDurum\"=true,\"HastaTC\"=@p1,\"HastaSikayet\"=@p2 where \"RanID\"=@p3", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", rtxtSikayet.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(txtID.Text));

            komut.ExecuteNonQuery();
            bgl.db_baglanti().Close();

            MessageBox.Show("Randevu Alındı", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
