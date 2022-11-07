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
            if (felulet == "belep" && MainWindow.jszam == 0) JatekosPanel.Content = "Játékos 1";
            else if (felulet == "belep" && MainWindow.jszam != 0) JatekosPanel.Content = "Játékos 2";
        }
        private void Kesz(object sender, RoutedEventArgs e)
        {
            TextBox inputnev = (TextBox)FindName("Nev");
            TextBox inputjelszo = (TextBox)FindName("Jelszo");

            inputnevtext = inputnev.Text;
            inputjelszotext = inputjelszo.Text;

            string belepesSQL = "SELECT * FROM jatekosok WHERE Nev='" + inputnevtext + "' AND Jelszo='"+inputjelszotext+"';";
            var vizsgalatTabla = ABKapcsolat.adatTabla(belepesSQL);

            if ((inputnevtext != null && inputnevtext != "") && (inputjelszotext != null && inputjelszotext != ""))
            {
                if(vizsgalatTabla.Rows.Count != 0)
                {
                    BelepAblakEldont();
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
        }

        private void BelepAblakEldont()
        {
            if (felulet == "adatm")
            {
                Adatmodositas adatmodositas = new Adatmodositas();
                adatmodositas.Show();
                this.Close();
            }
            else if (felulet == "belep")
            {
                if (MainWindow.jszam == 0)
                {
                    MainWindow.jatekos1 = inputnevtext;
                    MainWindow.jszam++;
                    this.Close();
                    Bejelentkezes bejelentkezes = new Bejelentkezes("belep");
                    bejelentkezes.Show();
                }
                else
                {
                    MainWindow.jatekos2 = inputnevtext;
                    MainWindow.jszam = 0;
                    this.Close();
                }
            }

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
