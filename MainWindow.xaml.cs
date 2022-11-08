using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.ComponentModel;

namespace Tic_Tac_Toe_WPF_beadando
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool jatekosCsere = true;
        public static string jatekos1 = "Játékos 1";
        public static int id1;
        public static int id2;
        public static string jatekos2 = "Játékos 2";
        public static int jszam = 0;
        private string jatekAllas = "";
        //Létrehozunk egy 3x3 mátrixot amelyben a játék celláinak értékét tároljuk el
        private char[,] tabla = new char[3, 3];

        public MainWindow()
        {
            InitializeComponent();
            tombFeltolt(tabla);
        }
        //Adott játékcella kattintásakor lefutó metódus
        private void Gomb_Katt(object sender, EventArgs e)
        {
            Button? gomb = sender as Button;
            var kepEcset = new ImageBrush();
            int sor = Grid.GetRow(gomb);
            int oszlop = Grid.GetColumn(gomb);

            //Cella beállítása - X
            if (gomb != null && jatekosCsere)
            {
                //Kattintáskor változtatjuk a jelenleg következő játékos nevét és szimbólumát
                //amelyet a jatekosKiir() függvénnyel oldjuk meg
                tabla[sor, oszlop] = 'X';
                kepEcset.ImageSource = new BitmapImage(new Uri("../../../képek/X_gomb.png", UriKind.Relative));
                gomb.Background = kepEcset;
                jatekosCsere = false;
                jatekosKiir();
            }
            //Cella beállítása - O
            else if (gomb != null && !jatekosCsere)
            {
                tabla[sor, oszlop] = 'O';
                kepEcset.ImageSource = new BitmapImage(new Uri("../../../képek/O_gomb.png", UriKind.Relative));
                gomb.Background = kepEcset;
                jatekosCsere = true;
                jatekosKiir();
            }
            if (gomb != null) gomb.IsEnabled = false;

            jatekAllas = jatekVizsgal(tabla);
            // Játék vége
            if (jatekAllas != "")
            {
                EredmenyFeltolt();
                tablaMentes();
                jatekVege();
                tombFeltolt(tabla);
                MessageBox.Show(jatekAllas, "Eredmény", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void kilepes(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        //A rács jobb oldali oszlopába íratja ki a játékost és a szimbólumot
        public void jatekosKiir()
        {
            TextBlock szovegDoboz = (TextBlock)this.FindName("jelenlegi");
            if (szovegDoboz != null)
            {
                if (jatekosCsere) szovegDoboz.Text = "Játékos:\nX - " +jatekos1;
                else szovegDoboz.Text = "Játékos:\nO - " +jatekos2;
            }
        }
        //Ha a játék végetér akkor a gombok értékeit töröljük és kattinthatatlanná tesszük
        private void jatekVege()
        {
            for (int i = 0; i < 9; i++)
            {
                Button gombideiglenes = (Button)FindName("gomb" + (i + 1));
                gombideiglenes.ClearValue(BackgroundProperty);
                gombideiglenes.IsEnabled = false;
            }
        }
        //Feltöltjük a mátrixot null értékekkel amelyel könnyen vizsgálhatunk
        //A játék végén ezt ugyanúgy meghívjuk mert az szeretnénk, hogy alaphelyzetben legyen a játék
        public void tombFeltolt(char[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = '\0';
                }
            }
            jatekosCsere = true;
            jatekosKiir();
        }
        //Megvizsgáljuk a tömböt a játék szabályai szerint
        public string jatekVizsgal(char[,] array)
        {
            //Soronkénti vizsgálat - X
            if ((array[0, 0] == 'X' && array[0, 1] == 'X' && array[0, 2] == 'X') ||
                (array[1, 0] == 'X' && array[1, 1] == 'X' && array[1, 2] == 'X') ||
                (array[2, 0] == 'X' && array[2, 1] == 'X' && array[2, 2] == 'X') ||
                //Oszloponkénti vizsgálat - X
                (array[0, 0] == 'X' && array[1, 0] == 'X' && array[2, 0] == 'X') ||
                (array[0, 1] == 'X' && array[1, 1] == 'X' && array[2, 1] == 'X') ||
                (array[0, 2] == 'X' && array[1, 2] == 'X' && array[2, 2] == 'X') ||
                //Átlónkénti vizsgálat) - X
                (array[0, 0] == 'X' && array[1, 1] == 'X' && array[2, 2] == 'X') ||
                (array[0, 2] == 'X' && array[1, 1] == 'X' && array[2, 0] == 'X'))
            {
                return jatekos1+" nyert";
                //Soronkénti vizsgálat - O
            }
            else if ((array[0, 0] == 'O' && array[0, 1] == 'O' && array[0, 2] == 'O') ||
               (array[1, 0] == 'O' && array[1, 1] == 'O' && array[1, 2] == 'O') ||
               (array[2, 0] == 'O' && array[2, 1] == 'O' && array[2, 2] == 'O') ||
               //Oszloponkénti vizsgálat - O
               (array[0, 0] == 'O' && array[1, 0] == 'O' && array[2, 0] == 'O') ||
               (array[0, 1] == 'O' && array[1, 1] == 'O' && array[2, 1] == 'O') ||
               (array[0, 2] == 'O' && array[1, 2] == 'O' && array[2, 2] == 'O') ||
               //Átlónkénti vizsgálat) - O
               (array[0, 0] == 'O' && array[1, 1] == 'O' && array[2, 2] == 'O') ||
               (array[0, 2] == 'O' && array[1, 1] == 'O' && array[2, 0] == 'O'))
            {
                return jatekos2+" nyert";
                //Döntetlen
            }
            else if ((array[0, 0] != '\0' && array[0, 1] != '\0' && array[0, 2] != '\0') &&
               (array[1, 0] != '\0' && array[1, 1] != '\0' && array[1, 2] != '\0') &&
               (array[2, 0] != '\0' && array[2, 1] != '\0' && array[2, 2] != '\0'))
            {
                return "Döntetlen";
            }
            return "";
        }
        //Táblamentés függvény tesztelésre
        private void tablaMentes()
        {
            using StreamWriter sw = new StreamWriter("teszt.txt");
            for (int i = 0; i < 3; i++)
            {
                sw.Write("{");
                for (int j = 0; j < 3; j++)
                {
                    sw.Write(tabla[i, j]);
                }
                sw.Write("}\n");
            }
        }
        //Akkor hívódik meg amikor az új játék gombra kattintunk
        //Alaphelyzetre állítja a játékállást
        private void ujJatek(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                Button gombideiglenes = (Button)FindName("gomb" + (i + 1));
                gombideiglenes.ClearValue(BackgroundProperty);
                gombideiglenes.IsEnabled = true;
                tombFeltolt(tabla);
            }
        }
        //Rekordok gomb és ablak megnyitása
        private void Rekordok(object sender, RoutedEventArgs e)
        {
            Rekordok rekordokablak = new Rekordok();
            rekordokablak.Show();
        }
        //Regisztrál gomb és ablak megnyitása
        private void regisztralGomb(object sender, RoutedEventArgs e)
        {
            Regisztracio regisztracio = new Regisztracio();
            regisztracio.Show();
        }
        //Módosítás ablak megnyitása (előbb be kell jelentkezni annak a felhasználónak akinek az
        //adatait módosítjuk)
        private void JatekosModosit(object sender, RoutedEventArgs e)
        {
            //Előbb be kell jelentkezni az adott felhasználónak
            Bejelentkezes bejelentkezes = new Bejelentkezes("adatm");
            bejelentkezes.Show();
        }

        //Belépés ablak megnyitása (előszőr az első játékos jelentkezik be majd
        //a második)
        //Ha a felhasználó nem jelentkezik be akkor helyette az alapértelmezett Játékos1/Játékos2
        //felhasználónevet használja a program
        private void JatekosBelepes(object sender, RoutedEventArgs e)
        {
            bool alaphelyzet = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabla[i, j] != '\0') alaphelyzet = false;
                }
            }
            if (alaphelyzet)
            {
                Bejelentkezes bejelentkezes = new Bejelentkezes("belep");
                bejelentkezes.Show();
            }
            else MessageBox.Show("Csak a játék elején adhatók meg a játékosok!",
                "Hiba!",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        //Metódus az eredmények feltöltésére adatbázisba
        private void EredmenyFeltolt()
        {
            
            if(jatekAllas == (jatekos1 + " nyert"))
            {
                string nyertSQL = "UPDATE jatszott SET nyert = nyert + 1 WHERE id='" + id1 + "';";
                string vesztettSQL = "UPDATE jatszott SET vesztett = vesztett + 1 WHERE id='" + id2 + "';";
                ABKapcsolat.lefuttatSQL(nyertSQL);
                ABKapcsolat.lefuttatSQL(vesztettSQL);
            }
            else if (jatekAllas == (jatekos2 + " nyert"))
            {
                string nyertSQL = "UPDATE jatszott SET nyert = nyert + 1 WHERE id='" + id2 + "';";
                string vesztettSQL = "UPDATE jatszott SET vesztett = vesztett + 1 WHERE id='" + id1 + "';";
                ABKapcsolat.lefuttatSQL(nyertSQL);
                ABKapcsolat.lefuttatSQL(vesztettSQL);
            }
            else
            {
                string dontetlen = "UPDATE jatszott SET dontetlen = dontetlen + 1 WHERE id='" + id1 + "';";
                string dontetlen2 = "UPDATE jatszott SET dontetlen = dontetlen + 1 WHERE id='" + id2 + "';";
                ABKapcsolat.lefuttatSQL(dontetlen);
                ABKapcsolat.lefuttatSQL(dontetlen2);
            }
            //Játéktábla adatainak kiolvasása, megformázása majd feltöltése
            string tablastring = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 2)
                    {
                        if (tabla[i, j] != '\0') tablastring += tabla[i, j];
                        else tablastring += "-";
                    }
                    else
                    {
                        if (tabla[i, j] != '\0') tablastring += tabla[i, j] + " ";
                        else tablastring += "- ";
                    }
                }
                if(i!=2) tablastring += "\n";
            }
            string tablaSQL = "INSERT INTO meccsek (tabla,meccsjatekos1Id,meccsjatekos2Id) VALUES('"+ tablastring + "','"+id1+"','"+id2+"');";
            ABKapcsolat.lefuttatSQL(tablaSQL);
            ABKapcsolat.kapcsolatBezar();
        }

        private void Betoltott(object sender, RoutedEventArgs e)
        {
            
        }
        //Meccsek gomb és ablak megnyitása
        private void MeccsekGomb(object sender, RoutedEventArgs e)
        {
            Meccsek meccsek = new Meccsek();
            meccsek.Show();
        }
    }
}
