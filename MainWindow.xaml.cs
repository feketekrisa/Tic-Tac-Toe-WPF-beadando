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
        public MainWindow()
        {
            InitializeComponent();
        }

        void button_Click(object sender, EventArgs e)
        {
            Button? bt = sender as Button;
            bt.Background = Brushes.Gray;
            var imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(new Uri("../../../képek/X_gomb.png", UriKind.Relative));
            bt.Background = imageBrush;
        }
    }
}
