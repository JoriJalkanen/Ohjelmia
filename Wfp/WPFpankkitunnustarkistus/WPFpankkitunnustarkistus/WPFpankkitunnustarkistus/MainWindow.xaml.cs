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

namespace WPFpankkitunnustarkistus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*ESIMERKKI!:
         * string nimi ="456465465456"
         * for(int i=0; i<nimi.lenght;i++)
         * {
         * int luku = int.parse((string)nimi[i]);
         * }
         * console.readkey();
         */
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtViitenumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string viitenumero;
                viitenumero = txtViitenumero.Text;

                tarkistus(viitenumero);
                
            }
        }

        private void tarkistus(string viitenumero)
        {
                try
                {
                    
                    int.Parse(viitenumero);
                    if (viitenumero.Length < 3 || viitenumero.Length >= 20)
                    {
                        throw new Exception(" ");
                    }
                    else
                    {
                        laske(viitenumero.ToString());
                    }
                }
                    catch (Exception virhe)
                {
                    MessageBox.Show("Viite numeron oltava 4-20 numeroa pitkä! " + virhe.Message);
                    return;
                  
                }
                return;
        }

        private void laske(string viitenumero)
        {
            int viitenumeroPituus = viitenumero.Length;
            int Pituus = viitenumeroPituus - 1; // viitenumeroPituus--; ei toiminut?!??!????
            string tarkistusnumero = viitenumero[Pituus].ToString();
            MessageBox.Show(tarkistusnumero);

            return;

        }

    }
}
