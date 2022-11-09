using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tic_Tac_Toe_WPF_beadando
{
    /// <summary>
    /// Interaction logic for Bejelentkezes.xaml
    /// </summary>
    public partial class Bejelentkezes : Window
    {
        string felulet;
        string inputnevtext;
        string inputjelszotext;
        public Bejelentkezes(string felulet)
        {
            this.felulet = felulet;
            InitializeComponent();

            //Mindkét játékost belépteti a rendszer egymás után
            if (felulet == "belep" && MainWindow.jszam == 0) JatekosPanel.Content = "Játékos 1";
            else if (felulet == "belep" && MainWindow.jszam != 0) JatekosPanel.Content = "Játékos 2";
        }
        private void Kesz(object sender, RoutedEventArgs e)
        {
            //A TextBox tartalmának változókba való mentése
            TextBox inputnev = (TextBox)FindName("Nev");
            TextBox inputjelszo = (TextBox)FindName("Jelszo");

            inputnevtext = inputnev.Text;
            inputjelszotext = inputjelszo.Text;

            //A lekérdezés megnézi, hogy van-e ilyen adat az adatbázisban
            string belepesSQL = "SELECT * FROM jatekosok WHERE Nev='" + inputnevtext + "' AND Jelszo='"+inputjelszotext+"';";
            var vizsgalatTabla = ABKapcsolat.adatTabla(belepesSQL);

            //visszajelzés a sikeres vagy sikertelen bejelentkezésről
            if ((inputnevtext != null && inputnevtext != "") && (inputjelszotext != null && inputjelszotext != ""))
            {
                if(vizsgalatTabla.Rows.Count != 0)
                {
                    foreach (System.Data.DataRow row in vizsgalatTabla.Rows)
                    {
                        int id = Convert.ToInt32(row["Id"]);
                        BelepAblakEldont(id);
                    }
                }
                else
                {
                    MessageBox.Show("Hibás felhasználónév vagy jelszó!",
                        "Hiba!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else MessageBox.Show("Kitöltettlen mező(k)!",
                        "Hiba!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            ABKapcsolat.kapcsolatBezar();
        }

        //A BelepAblakEldont azt figyeli, hogy melyik gomb volt megnyomva,
        //mert az adatmódosításhoz is előjön a bejelentkezés felület
        //Ha a bejelentkezés nyomjuk meg két játékost léptet be,
        //de ha az adatmódosítást, akkor csak egyet és utána átdob egy másik felületre
        private void BelepAblakEldont(int id)
        {
            //adatmódosítás felület
            if (felulet == "adatm")
            {
                Adatmodositas adatmodositas = new Adatmodositas(inputnevtext);
                adatmodositas.Show();
                this.Close();
            }
            //bejelentkezés felület
            else if (felulet == "belep")
            {
                if (MainWindow.jszam == 0)
                {
                    MainWindow.jatekos1 = inputnevtext;
                    MainWindow.id1 = id;
                    MainWindow.jszam++;
                    this.Close();
                    Bejelentkezes bejelentkezes = new Bejelentkezes("belep");
                    bejelentkezes.Show();
                }
                else
                {
                    MainWindow.jatekos2 = inputnevtext;
                    MainWindow.id2 = id;
                    MainWindow.jszam = 0;
                    this.Close();
                }
            }
            //Visszajelzés a felhasználónak
            MessageBox.Show("Sikeres bejelentkezés!",
                "Bejelentkezve!",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Megse(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
