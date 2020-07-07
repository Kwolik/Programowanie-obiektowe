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
        public event Action a;
        public event Action b;
        public event Action c;
        public event Action d;
        public event Action e;
        #endregion

        #region Properties
        string[] IView.aa { get; set; }
        string[] IView.bb { get; set; }
        string IView.cc { get; set; }
        #endregion

        #region Variables
        string aaa;
        string[] bbb;
        string[] ccc;
        #endregion

        private int numeracja;

        public View()
        {
            InitializeComponent();
            DB_Connect conn = DB_Connect.Instance;
            loadComboBoxItems();
        }

        private void checkStatusDB_Click(object sender, EventArgs e)
        {
            ProductRepository us = new ProductRepository();
            TypeRepository tp = new TypeRepository();
            us.getAllProducts();
            Console.WriteLine();
            tp.getAllTypes();
            
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

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Add("Śniadanie");
            comboBox2.Items.Add("Lunch");
            comboBox2.Items.Add("Obiad");
            comboBox2.Items.Add("Podwieczorek");
            comboBox2.Items.Add("Kolacja");
            comboBox2.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double masa = 0;
            double wzrost = 0;
            double wynik = 0;
            bladbmi.Text = "";
            zakresmasabmi.Text = "";
            zakreswzrostbmi.Text = "";
            wynikbmi.Text = "";
            if (masabmi.Text != "" && wzrostbmi.Text != "")
            {
                masa = Convert.ToDouble(masabmi.Text);
                wzrost = Convert.ToDouble(wzrostbmi.Text);

                double wysokosc = wzrost / 100;

                if (masa > 30 && masa < 300 && wysokosc > 0.90 && wysokosc < 2.50)
                {
                    wynik = masa / (wysokosc * wysokosc);
                    wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
                    wynikbmi.Text = Convert.ToString(wynik);
                    if (wynik < 18.5)
                    {
                        opisbmi.Text = "Masz niedowagę!";
                    }
                    else if (wynik >= 18.5 && wynik < 25)
                    {
                        opisbmi.Text = "Wartość prawdiłowa";
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
            double wynik = 0;
            bladbmr.Text = "";
            zakresmasabmr.Text = "";
            zakreswzrostbmr.Text = "";
            wynikbmr.Text = "";
            if (masabmr.Text != "" && wzrostbmr.Text != "")
            {
                wzrost = Convert.ToDouble(wzrostbmr.Text);
                masa = Convert.ToDouble(masabmr.Text);
                wiek = Convert.ToDouble(wiekbmr.Value);
                if (masa > 30 && masa < 300 && wzrost > 90 && wzrost < 250)
                {

                    if (kobietabmr.Checked)
                    {
                        wynik = (9.99 * masa) + (6.25 * wzrost) - (4.92 * wiek) - 161;
                        wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
                        wynikbmr.Text = Convert.ToString(wynik + " kcla");
                    }

                    if (mezczyznabmr.Checked)
                    {
                        wynik = (9.99 * masa) + (6.25 * wzrost) - (4.92 * wiek) + 5;
                        wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
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
            double wynik = 0;
            bladdz.Text = "";
            zakresmasadz.Text = "";
            zakreswzrostdz.Text = "";
            wynikdz.Text = "";
            if (masadz.Text != "" && wzrostdz.Text != "")
            {
                wzrost = Convert.ToDouble(wzrostdz.Text);
                masa = Convert.ToDouble(masadz.Text);
                wiek = Convert.ToDouble(wiekdz.Value);
                if (masa > 30 && masa < 300 && wzrost > 90 && wzrost < 250)
                {

                    if (kobietadz.Checked)
                    {
                        wynik = (9.99 * masa) + (6.25 * wzrost) - (4.92 * wiek) - 161;
                        wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
                        if (wspolczynnikdz.SelectedIndex == 0)
                        {
                            wynik = wynik * 1.0;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 1)
                        {
                            wynik = wynik * 1.2;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 2)
                        {
                            wynik = wynik * 1.4;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 3)
                        {
                            wynik = wynik * 1.6;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 4)
                        {
                            wynik = wynik * 1.8;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 5)
                        {
                            wynik = wynik * 2.0;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                    }

                    if (mezczyznadz.Checked)
                    {
                        wynik = (9.99 * masa) + (6.25 * wzrost) - (4.92 * wiek) + 5;
                        wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
                        if (wspolczynnikdz.SelectedIndex == 0)
                        {
                            wynik = wynik * 1.0;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 1)
                        {
                            wynik = wynik * 1.2;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 2)
                        {
                            wynik = wynik * 1.4;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 3)
                        {
                            wynik = wynik * 1.6;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 4)
                        {
                            wynik = wynik * 1.8;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
                        else if (wspolczynnikdz.SelectedIndex == 5)
                        {
                            wynik = wynik * 2.0;
                            wynikdz.Text = Convert.ToString(wynik + " kcla");
                        }
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
            double sumaKalorii = 0.0;

            kalorieProduktu.Text = "";

            if (tluszczProduktu.Text != ""  && bialkoProduktu.Text != "" && weglowodanyProduktu.Text != "" && wagaProduktu.Text != "")
            {
                tluszcz = Convert.ToDouble(tluszczProduktu.Text);
                bialko = Convert.ToDouble(bialkoProduktu.Text);
                weglowodany = Convert.ToDouble(weglowodanyProduktu.Text);
                waga = Convert.ToDouble(wagaProduktu.Text);

                double mnoznik = waga / 100;
                if(tluszcz > 0 && bialko >0 && weglowodany >0 && waga > 0)
                {
                    sumaKalorii = (tluszcz * 7 + bialko * 4 + weglowodany * 4) * mnoznik;

                    kalorieProduktu.Text = Convert.ToString(sumaKalorii);
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

        private double suma_Tluszczu = 0;
        private double suma_Bialka = 0;
        private double suma_Weglowodanow = 0;
        private double suma_Kalorii = 0;

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
                tluszcz = Convert.ToDouble(tluszczProduktu.Text);
                bialko = Convert.ToDouble(bialkoProduktu.Text);
                weglowodany = Convert.ToDouble(weglowodanyProduktu.Text);
                waga = Convert.ToDouble(wagaProduktu.Text);

                double mnoznik = waga / 100;
                if (tluszcz > 0 && bialko > 0 && weglowodany > 0 && waga > 0)
                {
                    sumaKalorii = (tluszcz * 7 + bialko * 4 + weglowodany * 4) * mnoznik;

                    kalorieProduktu.Text = Convert.ToString(sumaKalorii);
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
            if (tluszczProduktu.Text != "" && bialkoProduktu.Text != "" && weglowodanyProduktu.Text != "" && wagaProduktu.Text != "")
            {
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
    }
}
