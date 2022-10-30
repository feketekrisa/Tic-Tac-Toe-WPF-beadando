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
    /// Interaction logic for JatekVege.xaml
    /// </summary>
    public partial class JatekVege : Window
    {
        private Label felirat;
        private readonly Button okgomb;
        
        public JatekVege()
        {
            InitializeComponent();
            felirat = (Label)FindName("eredmeny");
            okgomb = (Button)FindName("ok");
            okgomb.Click += ablakBezar;
        }

        public void feliratBeallit(string jatekAllas)
        {
            felirat.Content = jatekAllas;
        }

        private void ablakBezar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
