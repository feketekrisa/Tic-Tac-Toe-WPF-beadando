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
            //Adatbázis MDF fájl elérési utvonala LocalDB-vel.
            string csatlakoz_String = @"server=(localdb)\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "rekordok.mdf ; Integrated Security = True";

            SqlConnection kapcsolat = new SqlConnection(csatlakoz_String);
            
            //Ha a kapcsolat nincs még nyitva akkor nyisson kapcsolatot az adatbázishoz.
            if (kapcsolat.State != ConnectionState.Open)
            {
                kapcsolat.Open();
            }
            //Visszaadjuk a jelenlegi kapcsolatot a többi függvénynek.
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
            string csatlakoz_String = System.Configuration.ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString; ;
            SqlConnection kapcsolat = new SqlConnection(csatlakoz_String);

            if (kapcsolat.State != ConnectionState.Closed) kapcsolat.Close();
        }
    }
}