﻿using System;
using System.Collections.Generic;
using System.Data;
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

            inputnevtext = inputnev.Text;
            inputjelszotext = inputjelszo.Text;
            inputemailtext = inputemail.Text;

            string vizsgalatSQL = "SELECT * FROM jatekosok WHERE Nev='"+inputnevtext+"';";
            var vizsgalatTabla = ABKapcsolat.adatTabla(vizsgalatSQL);

            if (vizsgalatTabla.Rows.Count == 0)
            {
                string regisztralSQL = "INSERT INTO jatekosok (Nev,Jelszo,Email) VALUES ('"+inputnevtext+"','"+inputjelszotext+"','"+inputemailtext+"');";
                ABKapcsolat.lefuttatSQL(regisztralSQL);
                MessageBox.Show("Sikeres regisztráció!","Regisztráció kész!",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ez a név már foglalt!","Hiba!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
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
