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
        private DataGrid adatRacs;
        private const string SQL = "" +
            "SELECT jatekosok.Id, jatekosok.Nev, jatszott.nyert, jatszott.vesztett " +
            "FROM [jatekosok]" +
            "INNER JOIN [jatszott]" +
            "ON jatekosok.Id=jatszott.jatekosId;";
        public Rekordok()
        {
            InitializeComponent();
        }

        public void adatracsFeltoltAdatTablaval()
        {
            var adattabla = ABKapcsolat.adatTabla("SELECT jatekosok.Id ,Nev, nyert, vesztett FROM jatekosok,jatszott WHERE jatekosok.Id=jatszott.jatekosId;");
            adatRacs.Items.Add(adattabla);
        }

        private void betoltott(object sender, RoutedEventArgs e)
        {
            adatRacs = (DataGrid)FindName("rekordoktabla");
            adatracsFeltoltAdatTablaval();
        }
    }
}