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
    public partial class DoktorBilgiDuzenle : Form
    {
        public DoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string TCNO;

        baglanti bgl = new baglanti();
        private void DoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            // bilgi düzenle kısmına doktorun TC sini çekmek

            MskTC.Text = TCNO;

            // bilgi düzenle kısmına doktorun diğer bilgilerini çekmek

            NpgsqlCommand komut = new NpgsqlCommand("select \"ad\",\"soyad\",\"DrSifre\",\"BransAd\" from \"Kisiler\" inner join \"Doktor\" on \"Kisiler\".\"kisiID\"=\"Doktor\".\"kisiID\"\r\ninner join \"Branslar\" on \"Doktor\".\"BransID\"=\"Branslar\".\"BransID\" where \"TC\"=@p1", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@p1", MskTC.Text);

            NpgsqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                txtAd.Text = dr[0].ToString();
                txtSoyad.Text = dr[1].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[2].ToString();
            }

            //Branşı comboboxa aktarma

            NpgsqlCommand komut2 = new NpgsqlCommand("select \"BransAd\" from \"Branslar\"", bgl.db_baglanti());

            NpgsqlDataReader dr2 = komut2.ExecuteReader();

            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            


            bgl.db_baglanti().Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // doktor bilgi güncelleme

            NpgsqlCommand komut1 = new NpgsqlCommand("update \"Kisiler\" set \"ad\"=@d1,\"soyad\"=@d2 where \"kisiID\"=(select * from kisid_getir(@d4))", bgl.db_baglanti());

            komut1.Parameters.AddWithValue("@d1", txtAd.Text);
            komut1.Parameters.AddWithValue("@d2", txtSoyad.Text);
            komut1.Parameters.AddWithValue("@d4", MskTC.Text);

            komut1.ExecuteNonQuery();

            NpgsqlCommand komut = new NpgsqlCommand("update \"Doktor\" set \"DrSifre\"=@p1 where \"kisiID\"=(select * from kisid_getir(@p4))", bgl.db_baglanti());

            komut.Parameters.AddWithValue("@p1", txtSifre.Text);
            komut.Parameters.AddWithValue("@p4", MskTC.Text);

            komut.ExecuteNonQuery();


            NpgsqlCommand komut2 = new NpgsqlCommand("update \"Doktor\" set \"BransID\"=@z1 where \"kisiID\"=(select * from kisid_getir(@z4))", bgl.db_baglanti());

            komut2.Parameters.AddWithValue("@z1", cmbBrans.SelectedIndex);
            komut2.Parameters.AddWithValue("@z4", MskTC.Text);

            komut2.ExecuteNonQuery();


            bgl.db_baglanti().Close();

            MessageBox.Show("Kayıt Güncellendi");
        }
    }
}
