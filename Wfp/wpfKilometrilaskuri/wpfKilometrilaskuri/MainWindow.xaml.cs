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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfKilometrilaskuri
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int KmMin= 0, KmMäärä= 0,TulosMin = 0,Tunnit = 0,Hmin = 0;

            try
            {
                KmMin = int.Parse(txtKmMin.Text);
                KmMäärä = int.Parse(txtKmMäärä.Text);

            }
            catch (Exception)
            {

                MessageBox.Show("jeeAnna numerotiedot!"); 
            }
            TulosMin = KmMäärä * KmMin;
            lblTulosMin.Content = TulosMin + " Minuuttia";
            if(TulosMin >= 60)
            {
            Tunnit = TulosMin / 60;
            Hmin = TulosMin - (Tunnit * 60);

            lblTulosHMin.Content = Tunnit + " tuntia ja " + Hmin + " Minuuttia"; 
            }
            else
            {
                Tunnit = 0;
                Hmin = TulosMin;

                lblTulosHMin.Content = Tunnit + " tuntia ja " + Hmin + " Minuuttia"; 
            }

            // Made by Jori Jalkanen e1501117
        }
    }
}
