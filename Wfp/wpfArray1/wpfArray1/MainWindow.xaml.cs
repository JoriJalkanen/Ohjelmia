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

namespace wpfArray1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] kk;
        int[] KuukaudenPvmLkm = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void lstKuukaudet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblTeksti.Content = kk[lstKuukaudet.SelectedIndex] + "ssa " + KuukaudenPvmLkm[lstKuukaudet.SelectedIndex] + " päivää";
            TxtKuukaudenNro.Text = (lstKuukaudet.SelectedIndex + 1).ToString();
        }

        private void TxtKuukaudenNro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                int kkIndeksi = 0;
                kkIndeksi = int.Parse(TxtKuukaudenNro.Text);
                if (kkIndeksi < 1 || kkIndeksi > 12)
                {
                    MessageBox.Show("Nyt meni päin mäntyjä");
                    kkIndeksi = 1;
                }
                kkIndeksi--;
                lblTeksti.Content = kk[kkIndeksi] + "ssa " + KuukaudenPvmLkm[kkIndeksi].ToString() + " päivää";
                lstKuukaudet.SelectedIndex = kkIndeksi;
                TxtKuukaudenNro.SelectedText = lstKuukaudet.SelectedIndex.ToString();
                TxtKuukaudenNro.Text = "";

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            kk = new string[12];
            kk[0] = "Tammikuu";
            kk[1] = "Helmikuu";
            kk[2] = "Maaliskuu";
            kk[3] = "Hutikuu";
            kk[4] = "Toukokuu";
            kk[5] = "Kesäkuu";
            kk[6] = "Heinäkuu";
            kk[7] = "Elokuu";
            kk[8] = "Syyskuu";
            kk[9] = "Lokakuu";
            kk[10] = "Marraskuu";
            kk[11] = "Joulukuu";

            for (int i = 0; i < 12; i++)
            {
                lstKuukaudet.Items.Add(kk[i]);
            }
            lstKuukaudet.SelectedIndex = 7;
            // lasketaan kaikkien päivien lkm yhteensä
            int päivienLkmYht = 0;
            for (int i = 0; i < 12; i++)
            {
                päivienLkmYht = päivienLkmYht + KuukaudenPvmLkm[i];
            }
            lblPäivienLkmYhteensä.Content = päivienLkmYht.ToString();


                txt1kvartaali.Text = lasku(0,2).ToString();
                txt4kvartaali.Text = lasku(9,11).ToString();
                
        }

        private void btnLaske_Click(object sender, RoutedEventArgs e)
        {
            tarkistaalkujaloppu();
        }

        public int lasku(int alkuKK,int loppuKK)
        {
            int päivienLkmYht = 0;
            for (int i = alkuKK; i <= loppuKK; i++)
            {
                päivienLkmYht = päivienLkmYht + KuukaudenPvmLkm[i];
            }
            return päivienLkmYht;
        }

private void tarkistaalkujaloppu() // aliohjelma napin jälkeen
{
    int alkuKK = 0, loppuKK = 0;
    try
    {
        alkuKK = int.Parse(txtalkuKK.Text);
        loppuKK = int.Parse(txtloppuKK.Text);

        if (alkuKK <= 0 || loppuKK > 12 || alkuKK > 12 || loppuKK <= 0)
        {
            throw new Exception(" ");

        }
    }
    catch (Exception vika)
    {

        MessageBox.Show("kuukausitiedon pitää olla numero 1-12 " + vika.Message);
        return;
    }
    alkuKK--;
    loppuKK--;
    lblPäivienLkmYhteensä.Content = lasku(alkuKK, loppuKK).ToString(); // Lisää päivien lukumäärän lasku aliohjelmasta



}

    }
}
