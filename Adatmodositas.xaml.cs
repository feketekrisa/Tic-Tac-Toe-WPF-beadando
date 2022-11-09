using System;
using System.Collections.Generic;
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
    /// Interaction logic for Adatmodositas.xaml
    /// </summary>
    public partial class Adatmodositas : Window
    {
        // Az adatmódosítás gomb lenyomásakor a bejelentkezés jön elő először és be kell jelentkezni, 
        // ami előhoz egy új ablakot, majd csak ez után lehet adatot módosítani

        string nev;
        public Adatmodositas(string nev)
        {
            this.nev = nev;
            InitializeComponent();
        }

        private void Kesz(object sender, RoutedEventArgs e)
        {
            //A TextBox tartalmának változókba való mentése
            TextBox inputnev = (TextBox)FindName("ujnev");
            TextBox inputjelszo = (TextBox)FindName("ujjelszo");
            TextBox inputemail = (TextBox)FindName("ujemail");
            //A Regex az helyes email formátumot ellenőrzi
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            string ujinputnevtext = inputnev.Text;
            string ujinputjelszotext = inputjelszo.Text;
            string ujinputemailtext = inputemail.Text;

            bool validEmail = regex.IsMatch(ujinputemailtext);
            // Az adatmódosításnál a már meglévő adatokat a TextBoxból kivett értékekre módosítjuk az  SQL UPDATE lekérdezéssel
            string modositnevSQL = "UPDATE jatekosok SET Nev='"+ujinputnevtext+"' WHERE Nev='"+nev+"';";
            string modositjelszoSQL = "UPDATE jatekosok SET Jelszo='"+ujinputjelszotext+"' WHERE Nev='"+nev+"';";
            string modositemailSQL = "UPDATE jatekosok SET Email='"+ujinputemailtext+"' WHERE Nev = '"+nev+"'; ";
            //Ezeket a lekérdezés után kiírjuk
            string nevvizsgalatSQL = "SELECT * FROM jatekosok WHERE Nev='"+ujinputnevtext+"';";

            //A TextBox elmentett értékei alapján ellenőrzi a program a helyes értékeket,
            //és ettől függően dob visszajelzést a sikeres vagy sikertelen adatmódosításról
            //Egyszerre csak egy adat módoítható

            //Üres mező vizsgálat
            if((ujinputnevtext == "" || ujinputnevtext == null) &&
                (ujinputjelszotext == "" || ujinputjelszotext == null)&&
                (ujinputemailtext == "" || ujinputemailtext == null))
            {
                MessageBox.Show("Nincs módosítsandó érték!",
                    "Hiba!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            //Név változtatás
            else if ((ujinputnevtext != "" || ujinputnevtext != null)&&
                    (ujinputjelszotext == "" || ujinputjelszotext == null) &&
                (ujinputemailtext == "" || ujinputemailtext == null))
            {
                var vizsgalatTabla = ABKapcsolat.adatTabla(nevvizsgalatSQL);
                if (vizsgalatTabla.Rows.Count == 0)
                {
                    if (ABKapcsolat.lefuttatSQL(modositnevSQL))
                        MessageBox.Show("Sikeres névváltoztatás!",
                        "Módosítva!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    else
                        MessageBox.Show("Hiba történt!",
                            "Hiba!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                }
            }
            //Jelszó változtatás
            else if ((ujinputnevtext == "" || ujinputnevtext == null) &&
                (ujinputjelszotext != "" || ujinputjelszotext != null) &&
                (ujinputemailtext == "" || ujinputemailtext == null))
            {
                if (ujinputjelszotext.Length > 3)
                {
                    if (ABKapcsolat.lefuttatSQL(modositjelszoSQL))
                        MessageBox.Show("Sikeres jelszó módosítás!",
                            "Módosítva!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    else
                    {
                        MessageBox.Show("Hiba történt!",
                            "Hiba!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                else MessageBox.Show("A jelszónak legalább 3 karakteresnek kell lennie!",
                        "Hiba!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    
            }
            //email változtatás
            else if ((ujinputjelszotext == "" || ujinputjelszotext == null)&&
                (ujinputnevtext == "" || ujinputnevtext == null) &&
                (ujinputemailtext != "" || ujinputemailtext != null))
            {
                if (validEmail)
                {
                    if (ABKapcsolat.lefuttatSQL(modositemailSQL))
                        MessageBox.Show("Sikeres email módosítás!",
                            "Módosítva!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    else
                        MessageBox.Show("Hiba történt!",
                            "Hiba!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                }else MessageBox.Show("Nem megfelelő email formátum!",
                        "Hiba!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

            }
            //Egyszerre csak egy adat módoítható
            else MessageBox.Show("Egyszerre csak egy értéket módosíthat!",
                            "Hiba!",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
            ABKapcsolat.kapcsolatBezar();
            Close();
        }

        private void Megse(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
