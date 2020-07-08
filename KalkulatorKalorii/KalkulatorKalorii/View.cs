using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KalkulatorKalorii.DataBase;

namespace KalkulatorKalorii
{
    public partial class View : Form, IView
    {
        #region Events
        public event Action<double,double> obliczBMI;
        public event Action<double, double, double> obliczBMRkobieta;
        public event Action<double, double, double> obliczBMRmezczyzna;
        public event Action<double, double, double, double> zapotrzebowaniek;
        public event Action<double, double, double, double> zapotrzebowaniem;
        public event Action<double, double, double, double> LiczenieKaloriiProduktu;
        #endregion

        #region Properties

        string[] produkt;
        string[] IView.Produkt
        {
            get { return produkt; }
            set { produkt = value; }
        }

        #endregion

        #region Variables

        #endregion

        private double suma_Tluszczu = 0.0;
        private double suma_Bialka = 0.0;
        private double suma_Weglowodanow = 0.0;
        private double suma_Kalorii = 0.0;
        static public string nazwa_produktu = "Arbuz";
        double wynik = 0.0;
        double IView.Wynik { get { return wynik; } set { wynik = value; } }

        public View()
        {
            InitializeComponent();
            DB_Connect conn = DB_Connect.Instance;
            loadComboBoxItems();
            ZaladujDoListy();
        }

        private void loadComboBoxItems()
        {
            wspolczynnikdz.DropDownStyle = ComboBoxStyle.DropDownList;
            wspolczynnikdz.Items.Add("1.0 - leżący lub siedzący tryb życia, brak aktywności fizycznej");
            wspolczynnikdz.Items.Add("1.2 - praca siedząca, aktywność fizyczna na niskim poziomie");
            wspolczynnikdz.Items.Add("1.4 - praca nie fizyczna, trening 2 razy w tygodniu");
            wspolczynnikdz.Items.Add("1.6 - lekka praca fizyczna, trening 3-4 razy w tygodniu");
            wspolczynnikdz.Items.Add("1.8 - praca fizyczna, trening 5 razy w tygodniu");
            wspolczynnikdz.Items.Add("2.0 - ciężka praca fizyczna, codzienny trening");
            wspolczynnikdz.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bladbmi.Text = "";
            zakresmasabmi.Text = "";
            zakreswzrostbmi.Text = "";
            wynikbmi.Text = "";
            double masa = 0;
            double wzrost = 0;


            if (masabmi.Text != "" && wzrostbmi.Text != "")
            {
                try { 
                    masa = Convert.ToDouble(masabmi.Text);
                    wzrost = Convert.ToDouble(wzrostbmi.Text);
                }
                catch (FormatException fEx)
                {
                    Console.WriteLine(fEx);
                }
                double wysokosc = wzrost / 100;
                

                    if (masa > 30 && masa < 300 && wysokosc > 0.90 && wysokosc < 2.50)
                    {
                        if (obliczBMI != null)
                            obliczBMI(masa, wzrost);
                        wynikbmi.Text = Convert.ToString(wynik);
                        if (wynik < 18.5)
                        {
                            opisbmi.Text = "Masz niedowagę!";
                        }
                        else if (wynik >= 18.5 && wynik < 25)
                        {
                            opisbmi.Text = "Waga prawdiłowa";
                        }
                        else if (wynik >= 25 && wynik < 30)
                        {
                            opisbmi.Text = "Masz nadwagę!";
                        }
                        else if (wynik >= 30)
                        {
                            opisbmi.Text = "Otyłość!";
                        }
                    }
                    else
                    {
                        if (masa < 30.00 || masa > 300.00)
                        {
                            bladbmi.Text = "Podano złą wartość!";
                            zakresmasabmi.Text = "Zakres: (30 < masa < 300)";
                            zakreswzrostbmi.Text = "Zakres: (0.90 < wzrost < 2.50)";
                        }
                        else if (wysokosc < 0.90 || wysokosc > 2.50)
                        {
                            bladbmi.Text = "Podano złą wartość!";
                            zakresmasabmi.Text = "Zakres: (30 < masa < 300)";
                            zakreswzrostbmi.Text = "Zakres: (0.90 < wzrost < 2.50)";
                        }
                    }

                }
                
            else
            {
                bladbmi.Text = "Wszystkie pola muszą być uzupełnione!";
            }
        } 
        

        private void button7_Click(object sender, EventArgs e)
        {
            double wzrost = 0;
            double masa = 0;
            double wiek = 0;
            bladbmr.Text = "";
            zakresmasabmr.Text = "";
            zakreswzrostbmr.Text = "";
            wynikbmr.Text = "";
            if (masabmr.Text != "" && wzrostbmr.Text != "")
            {
                try
                {
                    wzrost = Convert.ToDouble(wzrostbmr.Text);
                    masa = Convert.ToDouble(masabmr.Text);
                    wiek = Convert.ToDouble(wiekbmr.Value);
                }
                catch (FormatException fEx)
                {
                    Console.WriteLine(fEx);
                }

                if (masa > 30 && masa < 300 && wzrost > 90 && wzrost < 250)
                {
                   
                    if (kobietabmr.Checked)
                    {
                        if (obliczBMRkobieta != null)
                            obliczBMRkobieta(masa, wzrost, wiek);
                        wynikbmr.Text = Convert.ToString(wynik + " kcla");                         
                    }

                    if (mezczyznabmr.Checked)
                    {
                        if (obliczBMRmezczyzna != null)
                            obliczBMRmezczyzna(masa, wzrost, wiek);
                        wynikbmr.Text = Convert.ToString(wynik + " kcla");
                    }
                }
                else
                {
                    if (masa < 30.00 || masa > 300.00)
                    {
                        bladbmr.Text = "Podano złą wartość!";
                        zakresmasabmr.Text = "Zakres: (30 < masa < 300)";
                        zakreswzrostbmr.Text = "Zakres: (0.90 < wzrost < 2.50)";
                    }
                    else if (wzrost < 90 || wzrost > 250)
                    {
                        bladbmr.Text = "Podano złą wartość!";
                        zakresmasabmr.Text = "Zakres: (30 < masa < 300)";
                        zakreswzrostbmr.Text = "Zakres: (0.90 < wzrost < 2.50)";
                    }
                }
            }
            else
            {
                bladbmr.Text = "Wszystkie pola muszą być uzupełnione!";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            double wzrost = 0;
            double masa = 0;
            double wiek = 0;
            double wspl = 0;
            bladdz.Text = "";
            zakresmasadz.Text = "";
            zakreswzrostdz.Text = "";
            wynikdz.Text = "";


            if (masadz.Text != "" && wzrostdz.Text != "")
            {
                try { 
                    wzrost = Convert.ToDouble(wzrostdz.Text);
                    masa = Convert.ToDouble(masadz.Text);
                    wiek = Convert.ToDouble(wiekdz.Value);
                    wspl = Convert.ToDouble(wspolczynnikdz.SelectedIndex);
                }
                catch (FormatException fEx)
                {
                    Console.WriteLine(fEx);
                }
            if (masa > 30 && masa < 300 && wzrost > 90 && wzrost < 250)
                {


                    if (kobietadz.Checked)
                    {
                        if (zapotrzebowaniek != null)
                            zapotrzebowaniek(masa, wzrost, wiek,wspl);
                        wynikdz.Text = Convert.ToString(wynik);
                        
                    }

                    if (mezczyznadz.Checked)
                    {
                        if (zapotrzebowaniem != null)
                            zapotrzebowaniem(masa, wzrost, wiek, wspl);
                        wynikdz.Text = Convert.ToString(wynik);
                    }
                }
                else
                {
                    if (masa < 30.00 || masa > 300.00)
                    {
                        bladdz.Text = "Podano złą wartość!";
                        zakresmasadz.Text = "Zakres: (30 < masa < 300)";
                        zakreswzrostdz.Text = "Zakres: (0.90 < wzrost < 2.50)";
                    }
                    else if (wzrost < 90 || wzrost > 250)
                    {
                        bladdz.Text = "Podano złą wartość!";
                        zakresmasadz.Text = "Zakres: (30 < masa < 300)";
                        zakreswzrostdz.Text = "Zakres: (0.90 < wzrost < 2.50)";
                    }
                }
            }
            else
            {
                bladdz.Text = "Wszystkie pola muszą być uzupełnione!";
            }
        }

        private void ObliczProdukt_Click(object sender, EventArgs e)
        {
            double tluszcz = 0.0;
            double bialko = 0.0;
            double weglowodany = 0.0;
            double waga = 0.0;

            kalorieProduktu.Text = "";

            if (tluszczProduktu.Text != ""  && bialkoProduktu.Text != "" && weglowodanyProduktu.Text != "" && wagaProduktu.Text != "")
            {
                try { 
                    tluszcz = Convert.ToDouble(tluszczProduktu.Text);
                    bialko = Convert.ToDouble(bialkoProduktu.Text);
                    weglowodany = Convert.ToDouble(weglowodanyProduktu.Text);
                    waga = Convert.ToDouble(wagaProduktu.Text);
                }
                    catch (FormatException fEx)
                {
                    Console.WriteLine(fEx);
                }

            if (tluszcz > 0 && bialko >0 && weglowodany >0 && waga > 0)
                {
                    if (LiczenieKaloriiProduktu != null)
                        LiczenieKaloriiProduktu(tluszcz, bialko, weglowodany, waga);
                    kalorieProduktu.Text = Convert.ToString(wynik);
                }
                else
                {
                    bladProdukty.Text = "Podaj odpowiednie wartości!";
                }
            }
            else
            {
                bladProdukty.Text = "Wszystkie pola muszą być uzupełnione!";
            }
        }

        private void DodajDoListyProdukt_Click(object sender, EventArgs e)
        {
            double tluszcz = 0.0;
            double bialko = 0.0;
            double weglowodany = 0.0;
            double waga = 0.0;
            double sumaKalorii = 0.0;

            kalorieProduktu.Text = "";

            if (tluszczProduktu.Text != "" && bialkoProduktu.Text != "" && weglowodanyProduktu.Text != "" && wagaProduktu.Text != "")
            {
                try { 
                    tluszcz = Convert.ToDouble(tluszczProduktu.Text);
                    bialko = Convert.ToDouble(bialkoProduktu.Text);
                    weglowodany = Convert.ToDouble(weglowodanyProduktu.Text);
                    waga = Convert.ToDouble(wagaProduktu.Text);
                }
                catch (FormatException fEx)
                {
                    Console.WriteLine(fEx);
                }
            double mnoznik = waga / 100;
                if (tluszcz > 0 && bialko > 0 && weglowodany > 0 && waga > 0)
                {

                    if (LiczenieKaloriiProduktu != null)
                        LiczenieKaloriiProduktu(tluszcz, bialko, weglowodany, waga);

                    kalorieProduktu.Text = Convert.ToString(wynik);

                }
                else
                {
                    bladProdukty.Text = "Podaj odpowiednie wartości!";
                }
            }
            else
            {
                bladProdukty.Text = "Wszystkie pola muszą być uzupełnione!";
            }

            string[] rowListView1 = { nazwaProduktu.Text, tluszczProduktu.Text, bialkoProduktu.Text, weglowodanyProduktu.Text, kalorieProduktu.Text };
            var listView1Item = new ListViewItem(rowListView1);
            listView1.Items.Add(listView1Item);

            suma_Tluszczu += Convert.ToDouble(tluszczProduktu.Text);
            suma_Bialka += Convert.ToDouble(bialkoProduktu.Text);
            suma_Weglowodanow += Convert.ToDouble(weglowodanyProduktu.Text);
            suma_Kalorii += Convert.ToDouble(kalorieProduktu.Text);

            string[] rowListView2 = { nazwaProduktu.Text, Convert.ToString(suma_Tluszczu), Convert.ToString(suma_Bialka), Convert.ToString(suma_Weglowodanow), Convert.ToString(suma_Kalorii) };
            var listView2Item = new ListViewItem(rowListView2);

            if (listView2.Items.Count > 0)
            {
                listView2.Items[0].Remove();
            }

            listView2.Items.Add(listView2Item);
        }

        private void UsunZListyProdukt_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var confirmation = MessageBox.Show("Czy na pewno chcesz usunąć wybrane elementy?", "Usunięcie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
                {
                    for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        ListViewItem itm = listView1.SelectedItems[i];
                        listView1.Items[itm.Index].Remove();
                    }
                }
            }
            else
            {
                MessageBox.Show("Brak wybranych elementów!", "Bład");
            }

            suma_Tluszczu -= Convert.ToDouble(tluszczProduktu.Text);
            suma_Bialka -= Convert.ToDouble(bialkoProduktu.Text);
            suma_Weglowodanow -= Convert.ToDouble(weglowodanyProduktu.Text);
            suma_Kalorii -= Convert.ToDouble(kalorieProduktu.Text);

            string[] rowListView2 = { nazwaProduktu.Text, Convert.ToString(suma_Tluszczu), Convert.ToString(suma_Bialka), Convert.ToString(suma_Weglowodanow), Convert.ToString(suma_Kalorii) };
            var listView2Item = new ListViewItem(rowListView2);

            if (listView2.Items.Count > 0)
            {
                listView2.Items[0].Remove();
            }

            listView2.Items.Add(listView2Item);
        }


        private void ZaladujDoBazyProdukt_Click(object sender, EventArgs e)
        {
            ProductRepository us = new ProductRepository();

            string doZmiany = us.getProduct(nazwaProduktu.Text.ToString());
            string[] split = doZmiany.Split('/');

            nazwaProduktu.Text = split[0];
            tluszczProduktu.Text = split[1];
            bialkoProduktu.Text = split[2];
            weglowodanyProduktu.Text = split[3];
            
        }

        private void dodajDoBazy_Click(object sender, EventArgs e)
        {
            ProductRepository us = new ProductRepository();
            us.addProduct(nazwaProduktu.Text.ToString(), Convert.ToDouble(bialkoProduktu.Text) * 4 + Convert.ToDouble(weglowodanyProduktu.Text) * 4 + Convert.ToDouble(tluszczProduktu.Text) * 7, Convert.ToDouble(bialkoProduktu.Text), Convert.ToDouble(weglowodanyProduktu.Text), Convert.ToDouble(tluszczProduktu.Text));
        }

        private void ZaladujDoListy()
        {
            ProductRepository us = new ProductRepository();
            string[][] doZmiany = us.getProducts();

            for(int i = 0; i < doZmiany.Length; i++)
            {
                string[] rowListView3 = doZmiany[i];
                var listView3Item = new ListViewItem(rowListView3);
                listView3.Items.Add(listView3Item);
            }
        }
    }
}
