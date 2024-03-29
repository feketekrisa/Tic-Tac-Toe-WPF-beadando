﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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
    /// Interaction logic for Rekordok.xaml
    /// </summary>
    public partial class Rekordok : Window
    {
        private const string SQL = "SELECT jatekosok.Id ,Nev, Email, nyert, vesztett, dontetlen FROM jatekosok INNER JOIN jatszott ON jatekosok.Id=jatszott.jatekosId;";
        public Rekordok()
        {
            InitializeComponent();
        }

        //Adatbázisban szereplő játékosok statisztikáit jeleníti meg pl: mennyit nyert
        public void adatracsFeltoltAdatTablaval()
        {
            var adattabla = ABKapcsolat.adatTabla(SQL);
            rekordoktabla.DataContext = adattabla;
            if (rekordoktabla.CurrentColumn == null)
            {
                rekordoktabla.CanUserSortColumns = false;
            }
            ABKapcsolat.kapcsolatBezar();
        }

        private void betoltott(object sender, RoutedEventArgs e)
        {
            adatracsFeltoltAdatTablaval();
        }
    }
}
