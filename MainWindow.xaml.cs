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

namespace Tic_Tac_Toe_WPF_beadando
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool jatekos1 = true;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        void Gomb_Katt(object sender, EventArgs e)
        {
            Button? gomb = sender as Button;
            var kepEcset = new ImageBrush();
            
            if (gomb != null && jatekos1)
            {
                kepEcset.ImageSource = new BitmapImage(new Uri("../../../képek/X_gomb.png", UriKind.Relative));
                gomb.Background = kepEcset;
                jatekos1 = false;
            }
            else if(gomb != null && !jatekos1)
            {
                kepEcset.ImageSource = new BitmapImage(new Uri("../../../képek/O_gomb.png", UriKind.Relative));
                gomb.Background = kepEcset;
                jatekos1 = true;
            }
        }

        private void kilep(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
