using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Regisztracio.xaml
    /// </summary>
    public partial class Regisztracio : Window
    {
        string inputnevtext;
        string inputjelszotext;
        string inputemailtext;

        public Regisztracio()
        {
            InitializeComponent();
        }

        private void ujJatekosRegisztral(object sender, RoutedEventArgs e)
        {
            TextBox inputnev = (TextBox)FindName("Nev");
            TextBox inputjelszo = (TextBox)FindName("Jelszo");
            TextBox inputemail = (TextBox)FindName("Email");
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            inputnevtext = inputnev.Text;
            inputjelszotext = inputjelszo.Text;
            inputemailtext = inputemail.Text;

            bool validEmail = regex.IsMatch(inputemailtext);
            string vizsgalatSQL = "SELECT * FROM jatekosok WHERE Nev='"+inputnevtext+"';";
            var vizsgalatTabla = ABKapcsolat.adatTabla(vizsgalatSQL);

            if (validEmail && (inputjelszotext != null && inputjelszotext != "" && inputjelszotext.Length >= 3))
            {
                if (vizsgalatTabla.Rows.Count == 0)
                {
                    string regisztralSQL = "INSERT INTO jatekosok (Nev,Jelszo,Email) VALUES ('" + inputnevtext + "','" + inputjelszotext + "','" + inputemailtext + "');SELECT CAST(scope_identity() AS int);";

                    int id = ABKapcsolat.lefuttatScalarSQL(regisztralSQL);
                    string betoltSQL = "INSERT INTO jatszott (jatekosId,nyert,vesztett,dontetlen) VALUES ('"+id+"',0,0,0);";
                    ABKapcsolat.lefuttatSQL(betoltSQL);
                    MessageBox.Show("Sikeres regisztráció!",
                    "Regisztráció kész!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ez a név már foglalt!",
                        "Hiba!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Hibás email cím vagy jelszó!",
                    "Hiba!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            ABKapcsolat.kapcsolatBezar();
        }

        private void betoltott(object sender, RoutedEventArgs e)
        {
            
        }
        private void megse(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
