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

namespace Tic_Tac_Toe_WPF_beadando
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool jatekos1 = true;
        private char[,] tabla = new char[3, 3];
        public MainWindow()
        {
            InitializeComponent();
            tombFeltolt(tabla);
            jatekosKiir();
            
        }

        private void Gomb_Katt(object sender, EventArgs e)
        {
            Button? gomb = sender as Button;
            var kepEcset = new ImageBrush();
            int row = Grid.GetRow(gomb);
            int column = Grid.GetColumn(gomb);


            if (gomb != null && jatekos1)
            {
                tabla[row, column] = 'X';
                kepEcset.ImageSource = new BitmapImage(new Uri("../../../képek/X_gomb.png", UriKind.Relative));
                gomb.Background = kepEcset;
                jatekos1 = false;
                jatekosKiir();
            }
            else if(gomb != null && !jatekos1)
            {
                tabla[row, column] = 'O';
                kepEcset.ImageSource = new BitmapImage(new Uri("../../../képek/O_gomb.png", UriKind.Relative));
                gomb.Background = kepEcset;
                jatekos1 = true;
                jatekosKiir();
            }
            if (tabla[0, 0] == 'X' && tabla[0, 1] == 'X' && tabla[0, 2] == 'X') jatekVege(gomb);
        }

        private void kilep(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("teszt.txt"))
                for (int i = 0; i < 3; i++)
                {
                    sw.Write("{");
                    for (int j = 0; j < 3; j++)
                    {
                        sw.Write(tabla[i, j] + " ");
                    }
                    sw.Write("}\n");
                }
            System.Windows.Application.Current.Shutdown();
        }

        private void jatekosKiir()
        {
            TextBlock szovegDoboz = (TextBlock)this.FindName("jelenlegi");
            if (szovegDoboz != null)
            {
                if (jatekos1) szovegDoboz.Text = "Játékos: X";
                else szovegDoboz.Text = "Játékos: O";
            }
        }

        private void jatekVege(Button gomb)
        {
            gomb.Background = null;
        }

        public static void tombFeltolt(char[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = '\0';
                }
            }
        }
    }
}
