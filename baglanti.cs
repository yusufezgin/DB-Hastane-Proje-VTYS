using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Hastane_Proje_VTYS
{


    // tum formlar veritabanından çekilecek ondan her forma surekli npgsqlconnection yazmamak için burada sınıf içinde metot olarak yazdık
    class baglanti
    {

        public NpgsqlConnection db_baglanti()
        {
            //postgresql veritabanını baglama
            NpgsqlConnection baglan = new NpgsqlConnection("server=localHost; port=5432; Database=HastaneDB; user Id=postgres; password=2424");

            baglan.Open();

            return baglan;
        }
    }
}
