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
        private bool jatekos1 = true;
        private string jatekAllas = "";
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
            int sor = Grid.GetRow(gomb);
            int oszlop = Grid.GetColumn(gomb);


            if (gomb != null && jatekos1)
            {
                tabla[sor, oszlop] = 'X';
                kepEcset.ImageSource = new BitmapImage(new Uri("../../../képek/X_gomb.png", UriKind.Relative));
                gomb.Background = kepEcset;
                jatekos1 = false;
                jatekosKiir();
            }
            else if(gomb != null && !jatekos1)
            {
                tabla[sor, oszlop] = 'O';
                kepEcset.ImageSource = new BitmapImage(new Uri("../../../képek/O_gomb.png", UriKind.Relative));
                gomb.Background = kepEcset;
                jatekos1 = true;
                jatekosKiir();
                
            }
            if (gomb != null) gomb.IsEnabled = false;
            
            jatekAllas = jatekVizsgal(tabla);
            // Játék vége
            if (jatekAllas != "")
            {
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

        private void jatekosKiir()
        {
            TextBlock szovegDoboz = (TextBlock)this.FindName("jelenlegi");
            if (szovegDoboz != null)
            {
                if (jatekos1) szovegDoboz.Text = "Játékos: X";
                else szovegDoboz.Text = "Játékos: O";
            }
        }

        private void jatekVege()
        {
            for(int i = 0; i < 9; i++)
            {
                Button gombideiglenes = (Button)FindName("gomb"+(i+1));
                gombideiglenes.ClearValue(BackgroundProperty);
                gombideiglenes.IsEnabled = false;
            }
        }

        public void tombFeltolt(char[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = '\0';
                }
            }
            jatekos1 = true;
            jatekosKiir();
        }

        public string jatekVizsgal(char[,] array)
        {
            //Soronkénti vizsgálat - X
            if((array[0,0] == 'X' && array[0,1] == 'X' && array[0,2] == 'X') ||
                (array[1, 0] == 'X' && array[1, 1] == 'X' && array[1, 2] == 'X')||
                (array[2, 0] == 'X' && array[2, 1] == 'X' && array[2, 2] == 'X')||
                //Oszloponkénti vizsgálat - X
                (array[0, 0] == 'X' && array[1, 0] == 'X' && array[2, 0] == 'X')||
                (array[0, 1] == 'X' && array[1, 1] == 'X' && array[2, 1] == 'X')||
                (array[0, 2] == 'X' && array[1, 2] == 'X' && array[2, 2] == 'X')||
                //Átlónkénti vizsgálat) - X
                (array[0, 0] == 'X' && array[1, 1] == 'X' && array[2, 2] == 'X')||
                (array[0, 2] == 'X' && array[1, 1] == 'X' && array[2, 0] == 'X'))
            {
                return "Játékos 1 nyert";
            //Soronkénti vizsgálat - O
            }else if((array[0, 0] == 'O' && array[0, 1] == 'O' && array[0, 2] == 'O') ||
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
                return "Játékos 2 nyert";
            //Döntetlen
            }else if ((array[0, 0] != '\0' && array[0, 1] != '\0' && array[0, 2] != '\0')&&
                (array[1, 0] != '\0' && array[1, 1] != '\0' && array[1, 2] != '\0')&&
                (array[2, 0] != '\0' && array[2, 1] != '\0' && array[2, 2] != '\0'))
            {
                return "Döntetlen";
            }
            return "";
        }

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

        private void Rekordok(object sender, RoutedEventArgs e)
        {
            Rekordok rekordokablak = new Rekordok();
            rekordokablak.Show();
        }
    }
}
