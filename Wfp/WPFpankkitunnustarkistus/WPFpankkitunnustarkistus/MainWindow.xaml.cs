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
         * 
         * Bugit:
         * 1234561466666 tarkistusnumero toistuu eikä mene nollaksi
         * 
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
                viitenumero = txtViitenumero.Text.Trim();
                tarkistus(viitenumero);
                
            }
        }


        private void tarkistus(string viitenumero)
        {
            string tempviitenumero = "";
           for (int i = 0; i < viitenumero.Length;i++ )
            {
                if(viitenumero[i] != ' ')
                {
                    tempviitenumero += viitenumero[i];
                }             
            }
           viitenumero = tempviitenumero;
           txtViitenumero.Text = viitenumero;
                try
                {

               //     int.Parse(viitenumero[i].ToString());
                    if (viitenumero.Length < 3 || viitenumero.Length > 20)
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
           // string tarkistusnumero = viitenumero[Pituus].ToString();
            int[] kertoimet = { 7, 3, 1 };
            int kierros = 0;
            int lasku = 0;
            //ESIMERKKI!:
            // string nimi ="456465465456"  viitenumero
            for (int i = viitenumero.Length - 1; i >= 0; i--)
            {
                int luku = int.Parse(viitenumero[i].ToString());
                if (kierros > 2)
                {
                    kierros = 0;
                }
                lasku = lasku + (kertoimet[kierros] * luku);
                kierros++;
                
              
            }

            lasku = lasku + laskeTarkisusNumero(lasku);
           
                if (lasku % 10 == 0) 
                {
                    MessageBox.Show("viitenumero Oikein!");
                }
                else
                {
                    MessageBox.Show("viitenumero väärin!");
                }


            return;

        }

        private int laskeTarkisusNumero(int lasku)   //tää ei oikeen toimi 6666
        {
            int tarkistusnumero = 0,i=0;
            for (i=0; (lasku + i) % 10 != 0; i++)
            {
            
            }

            tarkistusnumero = i;
            if (tarkistusnumero == 0)
            {
             //   MessageBox.Show(tarkistusnumero.ToString());
                return tarkistusnumero;
            }
            else
            {
             //   tarkistusnumero = tarkistusnumero + 1;
             //   MessageBox.Show("tarkistusnumero on:"+tarkistusnumero.ToString());
                lbltarkistusnumero.Content = tarkistusnumero;
                return tarkistusnumero;
            }
        }




    }
}
