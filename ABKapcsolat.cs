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
            
            //Ha a kapcsolat még nincs nyitva akkor nyisson kapcsolatot az adatbázishoz.
            if (kapcsolat.State != ConnectionState.Open)
            {
                kapcsolat.Open();
            }
            //Visszaadjuk a jelenlegi kapcsolatot a többi függvénynek.
            return kapcsolat;
        }
        //SQL query: ezt a függvényt meghívva egy lekérdezést futtatunk le majd a lekérdezés
        //eredményeit egy adattáblával küldjük vissza (DataTable)
        public static DataTable adatTabla(string SQL)
        {
            SqlConnection kapcsolat = adatbazisKapcsolat();
            DataTable tabla = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(SQL, kapcsolat);

            adapter.Fill(tabla);
            return tabla;
        }


        //DML SQL parancsok lefuttatására használható
        public static bool lefuttatSQL(string SQL)
        {
            SqlConnection kapcsolat = adatbazisKapcsolat();
            SqlCommand sqlparancs = new SqlCommand(SQL, kapcsolat);

            if (sqlparancs.ExecuteNonQuery() > 0) return true;
            return false;
        }
        //Hasonló mint az előző viszont ez egy olyan ID-t ad vissza a lefuttatás végén amely
        //megmondja, hogy melyik id-be szúrta be a rekordot
        public static int lefuttatScalarSQL(string SQL)
        {
            SqlConnection kapcsolat = adatbazisKapcsolat();
            SqlCommand sqlparancs = new SqlCommand(SQL, kapcsolat);
           
            var id = (Int32)sqlparancs.ExecuteScalar();

            return id;
        }

        //Lezárjuk vele az adatbázis kapcsolatot
        public static void kapcsolatBezar()
        {
            string csatlakoz_String = @"server=(localdb)\MSSQLLocalDB;AttachDbFilename=" + new FileInfo(AppDomain.CurrentDomain.BaseDirectory).Directory.Parent.FullName.Replace(@"bin\Debug", "") + "rekordok.mdf ; Integrated Security = True";
            SqlConnection kapcsolat = new SqlConnection(csatlakoz_String);
            //Ha a kapcsolat még nincs lezárva akkor zárjuk le
            if (kapcsolat.State != ConnectionState.Closed) kapcsolat.Close();
        }
    }
}