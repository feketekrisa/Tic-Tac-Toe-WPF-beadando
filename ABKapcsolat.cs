using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Tic_Tac_Toe_WPF_beadando
{
    public static class ABKapcsolat
    {
        public static SqlConnection adatbazisKapcsolat()
        {
            //Adatbázis MDF fájl elérési utvonala LocalDB-vel.
            string csatlakoz_String = @"server=(localdb)\MSSQLLocalDB;AttachDbFilename=" + new FileInfo(AppDomain.CurrentDomain.BaseDirectory).Directory.Parent.FullName.Replace(@"bin\Debug","") + "rekordok.mdf ; Integrated Security = True";

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

        public static bool lefuttatSQL(string SQL)
        {
            SqlConnection kapcsolat = adatbazisKapcsolat();
            SqlCommand sqlparancs = new SqlCommand(SQL, kapcsolat);

            if (sqlparancs.ExecuteNonQuery() > 0) return true;
            return false;
        }

        public static int lefuttatScalarSQL(string SQL)
        {
            SqlConnection kapcsolat = adatbazisKapcsolat();
            SqlCommand sqlparancs = new SqlCommand(SQL, kapcsolat);
           
            var id = (Int32)sqlparancs.ExecuteScalar();

            return id;
        }

        public static void kapcsolatBezar()
        {
            string csatlakoz_String = @"server=(localdb)\MSSQLLocalDB;AttachDbFilename=" + new FileInfo(AppDomain.CurrentDomain.BaseDirectory).Directory.Parent.FullName.Replace(@"bin\Debug", "") + "rekordok.mdf ; Integrated Security = True";
            SqlConnection kapcsolat = new SqlConnection(csatlakoz_String);

            if (kapcsolat.State != ConnectionState.Closed) kapcsolat.Close();
        }
    }
}