using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Tic_Tac_Toe_WPF_beadando
{
    public static class ABKapcsolat
    {
        public static SqlConnection adatbazisKapcsolat()
        {
            string csatlakoz_String = Properties.Settings.Default.connection_String;
            SqlConnection kapcsolat = new SqlConnection(csatlakoz_String);
            
            if (kapcsolat.State != ConnectionState.Open)
            {
                kapcsolat.Open();
            }
            return kapcsolat;
        }
        public static DataTable adatTabla(string SQL)

        {
            SqlConnection kapcsolat = adatbazisKapcsolat();
            DataTable tabla = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(SQL, kapcsolat);

            adapter.Fill(tabla);
            return tabla;
        }

        public static void lefuttatSQL(string SQL)

        {
            SqlConnection kapcsolat = adatbazisKapcsolat();
            SqlCommand sqlparancs = new SqlCommand(SQL, kapcsolat);

            sqlparancs.ExecuteNonQuery();
        }

        public static void kapcsolatBezar()

        {
            string csatlakoz_String = Properties.Settings.Default.connection_String;
            SqlConnection kapcsolat = new SqlConnection(csatlakoz_String);

            if (kapcsolat.State != ConnectionState.Closed) kapcsolat.Close();
        }
    }
}