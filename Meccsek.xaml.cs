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
    /// Interaction logic for Meccsek.xaml
    /// </summary>
    public partial class Meccsek : Window
    {
        private const string SQL = "SELECT meccsek.Id, tabla, meccsjatekos1Id, meccsjatekos2Id FROM meccsek;";
        public Meccsek()
        {
            InitializeComponent();
        }

        private void Betoltott(object sender, RoutedEventArgs e)
        {
            adatracsFeltoltAdatTablaval();
        }

        private void adatracsFeltoltAdatTablaval()
        {
            var adattabla = ABKapcsolat.adatTabla(SQL);
            meccsektabla.DataContext = adattabla;
            if (meccsektabla.CurrentColumn == null)
            {
                meccsektabla.CanUserSortColumns = false;
            }
            ABKapcsolat.kapcsolatBezar();
        }
    }
}
