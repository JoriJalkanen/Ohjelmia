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
using System.Xml;

namespace wpfOtteluPVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int ottelumäärä = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LaskePelatutOttelut();
          //  ottelumäärä = 0;
           // MessageBox.Show(LaskePelatutOttelutPALAUTTAVA().ToString());
           
        }
        int LaskePelatutOttelutPALAUTTAVA()
        {


            XmlReader reader = XmlReader.Create("SMliigaOtteluPVM.xml");

            DateTime tänään = DateTime.Now; // tämän päivän päiväys datetime-muodossa



            // XmlTextReader reader = new XmlTextReader("SMliiga.xml"); (vanha)

            // suositelltu versio yhteyden avaamiseen xml-tiedostoon

            while (reader.Read())
            {

                if (reader.NodeType == XmlNodeType.Element
                    && reader.Name == "Ottelu")
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Pvm")
                        {
                            reader.Read();
                            if (tänään > DateTime.ParseExact(reader.Value, "d.m.yyyy", null))
                            {
                                ottelumäärä++;
                            }
                        }
                    }
                }
            }
            // MessageBox.Show(tänään.ToString("d.m.yyyy"));
            // MessageBox.Show("Otteluiden lukumäärä: " + ottelumäärä); // poista myöhemmin

            reader.Close();
            return ottelumäärä;

        }
        void LaskePelatutOttelut()
        {


            XmlReader reader = XmlReader.Create("SMliigaOtteluPVM.xml");
            string joukkue, pvm;


            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element
                    && reader.Name == "Ottelu")
                {
                    while (reader.Read())
                    {

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Pvm")
                        {
                            reader.Read();
                            pvm = reader.Value;
                            while (reader.Read())
                            {

                                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Kotijoukkue")
                                {
                                    reader.Read();
                                    joukkue = reader.Value;
                                    while (reader.Read())
                                    {

                                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Vierasjoukkue")
                                        {
                                            reader.Read();
                                            joukkue = pvm + "\t" + joukkue + " - " + reader.Value;                                     
                                            lstottelut.Items.Add(joukkue);
                                            joukkue = "";
                                            break;                                                                                                                                                                                  
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
                reader.Close();
                return;


            }
        
        

        private void lstottelut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lstottelut.SelectedIndex;
            HaeTiedot(index);
        }
        void HaeTiedot(int index)
        {
            lblPvm.Content = "";
            lblKoti.Content = "";
            lblmaalitkoti.Content = "";
            lblmaalitvieras.Content = "";
            lblottelutyyppi.Content = "";
            lblvieras.Content = "";
           
            XmlReader reader = XmlReader.Create("SMliigaOtteluPVM.xml");
            int i = 0;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element
                    && reader.Name == "Ottelu")
                {
                    if(i==index)
                    {
                        while(reader.Read())
                        {
                            if(reader.NodeType==XmlNodeType.EndElement && reader.Name.Equals("Ottelu"))
                            {
                                break;
                            }
                            if(reader.NodeType==XmlNodeType.Element)
                            {
                               
                                    switch (reader.Name)
                                   {
                                       case "Pvm":
                                           reader.Read();
                                           lblPvm.Content = reader.Value.ToString();
                                           break;
                                       case "Kotijoukkue":
                                           reader.Read();
                                           lblKoti.Content = reader.Value.ToString();
                                           break;
                                       case "Vierasjoukkue":
                                           reader.Read();
                                           lblvieras.Content = reader.Value.ToString();
                                           break;
                                       case "Ottelutyyppi":
                                           reader.Read();
                                           lblottelutyyppi.Content = reader.Value.ToString();
                                           break;
                                       case "Kotimaalit":
                                           reader.Read();
                                           lblmaalitkoti.Content = reader.Value.ToString();
                                           break;
                                       case "Vierasmaalit":
                                           reader.Read();
                                            lblmaalitvieras.Content = reader.Value.ToString();
                                           break;
                               } 
                            }
                        }
                        break;
                    }         
                    else
                    {
                        i++;
                    }
                    
                }
              
            }

        }

        private void txtJoukkue_KeyUp(object sender, KeyEventArgs e)
        {
            
            XmlTextReader reader = new XmlTextReader("SMliigaOtteluPVM.xml");

            string boxJoukkue = txtJoukkue.Text.ToString();

            if (e.Key == Key.Enter)
            {
                lstottelut.Items.Clear();
                
                   

                if (rbtAll.IsChecked == true) // RADIO KAIKKI
                {
                    HaeKaikkiJoukkueet(boxJoukkue);
                }

                else if (rbtKoti.IsChecked == true) // RADIO KOTI
                {
                    haeKotiJoukkue(boxJoukkue);
                    

                }

                else if (rbtVieras.IsChecked == true) // RADIO VIERAS
                {
                    haeVierasJoukkue(boxJoukkue);

                }
                else
                {
                    MessageBox.Show("Valitse ensin joku radio napeista!");
                }
               
            }
            reader.Close();
        }

        private void HaeKaikkiJoukkueet(string boxJoukkue)
        {
            XmlReader reader = XmlReader.Create("SMliigaOtteluPVM.xml");
            string joukkue, pvm;

            
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element
                    && reader.Name == "Ottelu")
                {
                    while (reader.Read())
                    {

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Pvm")
                        {
                            reader.Read();
                            pvm = reader.Value;
                            while (reader.Read())
                            {

                                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Kotijoukkue")
                                {
                                    reader.Read();
                                    if (reader.Value.ToUpper() == boxJoukkue.ToUpper())
                                    {
                                        joukkue = reader.Value;
                                        while (reader.Read())
                                        {

                                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Vierasjoukkue")
                                            {
                                                reader.Read();
                                                joukkue = pvm + "\t" + joukkue + " - " + reader.Value;
                                                lstottelut.Items.Add(joukkue);
                                                joukkue = "";
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        joukkue = reader.Value;
                                        while(reader.Read())
                                        {
                                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Vierasjoukkue")
                                            {
                                                reader.Read();
                                                if (reader.Value.ToUpper() == boxJoukkue.ToUpper())
                                                {
                                                    joukkue = pvm + "\t" + joukkue + " - " + reader.Value;
                                                    lstottelut.Items.Add(joukkue);
                                                    joukkue = "";
                                                    break;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }                                         
                                        }
                                        break;
                                    }
                                  
                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            return;
        }

        private void haeVierasJoukkue(string boxJoukkue)
        {
            XmlReader reader = XmlReader.Create("SMliigaOtteluPVM.xml");
            string joukkue, pvm;


            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element
                    && reader.Name == "Ottelu")
                {
                    while (reader.Read())
                    {

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Pvm")
                        {
                            reader.Read();
                            pvm = reader.Value;
                            while (reader.Read())
                            {

                                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Kotijoukkue")
                                {
                                    reader.Read();

                                        joukkue = reader.Value;

                                        joukkue = reader.Value;
                                        while (reader.Read())
                                        {
                                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Vierasjoukkue")
                                            {
                                                reader.Read();
                                                if (reader.Value.ToUpper() == boxJoukkue.ToUpper())
                                                {
                                                    joukkue = pvm + "\t" + joukkue + " - " + reader.Value;
                                                    lstottelut.Items.Add(joukkue);
                                                    joukkue = "";
                                                    break;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    

                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            return;
        }

        private void haeKotiJoukkue(string boxJoukkue)
        {
            XmlReader reader = XmlReader.Create("SMliigaOtteluPVM.xml");
            string joukkue, pvm;


            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element
                    && reader.Name == "Ottelu")
                {
                    while (reader.Read())
                    {

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Pvm")
                        {
                            reader.Read();
                            pvm = reader.Value;
                            while (reader.Read())
                            {

                                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Kotijoukkue")
                                {
                                    reader.Read();
                                    if (reader.Value.ToUpper() == boxJoukkue.ToUpper())
                                    {
                                        joukkue = reader.Value;
                                        while (reader.Read())
                                        {

                                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Vierasjoukkue")
                                            {
                                                reader.Read();
                                                joukkue = pvm + "\t" + joukkue + " - " + reader.Value;
                                                lstottelut.Items.Add(joukkue);
                                                joukkue = "";
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                    else
                                    {                                   
                                        break;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            reader.Close();
            return;
        }


    }
}
