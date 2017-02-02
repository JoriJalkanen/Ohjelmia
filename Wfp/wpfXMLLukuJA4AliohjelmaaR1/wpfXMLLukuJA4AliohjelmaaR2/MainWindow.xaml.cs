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
//**
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Xml;

namespace wpfXMLLukuJA4AliohjelmaaR2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int otteluidenLkm = 0;
        const string XMLtiedosto = @"U:\public_html\2016Kohj1\SMliigaOtteluPVM.xml";
        //  @"U:\public_html\2016Kohj1\SMliigaOtteluPVM.xml";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Tee otteluiden laskenta aliohjelmamenettelyä käyttäen
            // Nimi: LaskeKaikkiPelaajat
            // Saa: 0
            // Tuottaa: 0
            // Tehtävä: Näytä MessageBox-ilmoituksella
            // koko liigan pelaajien lukumäärä.
            LaskePelatutOttelut();
            // kutsutaan palauttavaa aliohjelmaa
            otteluidenLkm = 0;
            int uusiOtteluLkm = LaskePelatutOttelutPALAUTTAVA();
            
            // hae listalaatikkoon otteluista
            // Kotijoukkue - Vierasjoukkue
            LaitaOttelutListaLaatikkoon();
        }
        void LaitaOttelutListaLaatikkoon()
        {
            string kotiJoukkue = "", vierasJoukkue = "";
            DateTime tänään = DateTime.Now;

            // avaa yhteyden XML-tiedostoon
            XmlReader reader = XmlReader.Create(XMLtiedosto);

            // luetaan XML-tiedosto ensimmäisestä rivistä loppuun saakka
            while (reader.Read())
            {
                reader.MoveToContent();
                if (reader.NodeType == XmlNodeType.Element &&
                    reader.Name == "Kotijoukkue")
                {

                    reader.Read();
                    kotiJoukkue = reader.Value;
                }   
                if (reader.NodeType == XmlNodeType.Element &&
                reader.Name == "Vierasjoukkue")
                {
                    reader.Read();
                    vierasJoukkue = reader.Value;
                    lstOttelut.Items.Add(kotiJoukkue + " - " + vierasJoukkue);
                }
               
            }
            // sulkee yhteyden XML-tiedostoon
            reader.Close();
            return;
        }
        void LaskePelatutOttelut()
        {
            DateTime tänään = DateTime.Now;
            
            // avaa yhteyden XML-tiedostoon
            XmlReader reader = XmlReader.Create(XMLtiedosto);

            // luetaan XML-tiedosto ensimmäisestä rivistä loppuun saakka
            while (reader.Read())
            {
                reader.MoveToContent();
                if (reader.NodeType == XmlNodeType.Element &&
                    reader.Name == "Ottelu")
                {
                    // hae kaikkien pelaajien lukumäärä
                    otteluidenLkm++;
                }
            }
            // sulkee yhteyden XML-tiedostoon
            reader.Close();
            return;
        }
        int LaskePelatutOttelutPALAUTTAVA()
        {
            DateTime tänään = DateTime.Now;
            // avaa yhteyden XML-tiedostoon
            XmlReader reader = XmlReader.Create(XMLtiedosto);
            // luetaan XML-tiedosto ensimmäisestä rivistä loppuun saakka
            while (reader.Read())
            {
                reader.MoveToContent();
                if (reader.NodeType == XmlNodeType.Element &&
                    reader.Name == "Ottelu")
                {
                    // hae kaikkien pelaajien lukumäärä
                    otteluidenLkm++;
                }
            }
            // sulkee yhteyden XML-tiedostoon
            reader.Close();
            return otteluidenLkm;
        }


    }
}
