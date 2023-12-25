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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_Hastane_Proje_VTYS
{
    public partial class SekreterDetay : Form
    {
        public SekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
        baglanti bgl = new baglanti();
        private void SekreterDetay_Load(object sender, EventArgs e)
        {
            // tablo donduren parametreli fonksiyon
            //DataTable dt1 = new DataTable();
            //NpgsqlCommand komut = new NpgsqlCommand("select * from brans_dr_getir(@p1)", bgl.db_baglanti());
            //komut.Parameters.AddWithValue("@p1", textBox1.Text);
            //NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(komut);
            //da1.Fill(dt1);
            //dataGridView1.DataSource = dt1;
            //bgl.db_baglanti().Close();

            // TC çekme

            lblTC.Text = TCnumara;

            // Ad Soyad çekme

            NpgsqlCommand komut1 = new NpgsqlCommand("select \"ad\",\"soyad\" from sekreterlistesi where \"TC\"=@p1", bgl.db_baglanti());


            komut1.Parameters.AddWithValue("@p1", lblTC.Text);

            NpgsqlDataReader dr1 = komut1.ExecuteReader();

            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0]+ " "+dr1[1];
            }
            bgl.db_baglanti().Close();


            //Branşları datagride çekme

            DataTable dt1 = new DataTable();

            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("select * from \"Branslar\"", bgl.db_baglanti());

            da1.Fill(dt1);

            dataGridView1.DataSource = dt1;



            // Doktorları datagride çekme

            DataTable dt2 = new DataTable();
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter("select \"Doktorun Adı\",\"Doktorun Soyadı\",\"Poliklinik Adı\",\"Branş Adı\",\"OdaNo\" from doktorlistesi", bgl.db_baglanti());

            da2.Fill(dt2);

            dataGridView2.DataSource = dt2;



            //Branşı comboboxa aktarma

            NpgsqlCommand komut2 = new NpgsqlCommand("select \"BransAd\" from \"Branslar\"", bgl.db_baglanti());

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
            NpgsqlCommand komut3 = new NpgsqlCommand("select \"Doktorun Adı\",\"Doktorun Soyadı\" from doktorlistesi where \"Branş Adı\"=@p1", bgl.db_baglanti());

            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);

            NpgsqlDataReader dr3 = komut3.ExecuteReader();

            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);

            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            //Randevu ekleme

            NpgsqlCommand komut = new NpgsqlCommand("insert into \"Randevular\" (\"RanTarih\",\"RanSaat\",\"DoktorAdSoyad\",\"BransAd\") values (@p1,@p2,@p3,@p4)", bgl.db_baglanti());


            komut.Parameters.AddWithValue("@p1", mskTarih.Text);
            komut.Parameters.AddWithValue("@p2", mskSaat.Text);
            komut.Parameters.AddWithValue("@p3", cmbDoktor.Text);
            komut.Parameters.AddWithValue("@p4", cmbBrans.Text);

            komut.ExecuteNonQuery();

            bgl.db_baglanti().Close();

            MessageBox.Show("Randevu oluşturuldu");
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            DoktorPaneli dd = new DoktorPaneli();
            dd.Show();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            //Duyuru oluşturma

            NpgsqlCommand komut = new NpgsqlCommand("insert into \"Duyurular\" (\"Duyuru\") values (@d1)", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@d1", rchDuyuru.Text);

            komut.ExecuteNonQuery();
            bgl.db_baglanti().Close();
            MessageBox.Show("Duyuru oluşturuldu");
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            Branslar br = new Branslar();
            br.Show();
        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            RandevuListesi rl = new RandevuListesi();
            rl.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            Duyurular dy = new Duyurular();
            dy.Show();
        }
    }
}
