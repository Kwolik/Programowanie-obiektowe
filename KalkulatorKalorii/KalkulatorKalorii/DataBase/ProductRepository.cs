using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KalkulatorKalorii.DataBase
{
    class ProductRepository
    {
        #region queries

        private static string SELECT_PRODUCT = "SELECT * FROM produkty p, wartosci_odzywcze w_o WHERE p.id_produktu = w_o.id_wartosci AND p.nazwa LIKE CONCAT('%',@Produkt,'%')";
        private static string SELECT_PRODUCTS = "SELECT p.id_produktu ID, p.nazwa Nazwa, w.kcal Kalorie, w.bialko Bialka, w.tluszcz Tluszcze, w.weglowodany Weglowodany, t.nazwa_typu Typ FROM `produkty` p, `produkty-wartosci` pw, wartosci_odzywcze w, `produkty-typ` pt,typ t  WHERE p.id_produktu = pw.id_produktu AND pw.id_wartosci = w.id_wartosci AND p.id_produktu = pt.id_produktu AND pt.id_typu = t.id_typu ORDER BY 1 ASC";

        private static string ADD_PRODUCT_PRODUKTY = "INSERT INTO produkty (nazwa) values (@NazwaProduktu)";
        private static string ADD_PRODUCT_WARTOSCI = "INSERT INTO wartosci_odzywcze (kcal,bialko,weglowodany,tluszcz) values (@Kalorie,@Bialko, @Weglowodany, @Tluszcze)";

        #endregion

        private static MySqlConnection connection = DB_Connect.Instance.Connection;


        public string getProduct(string produkt)
        {
            string nazwaKoncowa = "";
            string Nazwa = "";
            double Tluszcz = 0.0;
            double Weglowodany = 0.0;
            double Bialko = 0.0;

            //tylko w tym zasiegu nigdzie indziej nie wychodzi
            using (MySqlCommand command = new MySqlCommand(SELECT_PRODUCT, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Produkt", produkt);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Nazwa = (string)reader["nazwa"];
                    Tluszcz = Convert.ToDouble(reader["tluszcz"].ToString());
                    Weglowodany = Convert.ToDouble(reader["weglowodany"].ToString());
                    Bialko = Convert.ToDouble(reader["bialko"].ToString());
                }
                connection.Close();
                nazwaKoncowa = Nazwa + "/" + Tluszcz + "/" + Bialko + "/" + Weglowodany;
                return nazwaKoncowa;
            }
        }


        public void addProduct(string nazwaProduktu,double kcal, double bialko, double weglowodany, double tluszcze)
        {
            using (MySqlCommand command = new MySqlCommand(ADD_PRODUCT_PRODUKTY, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@NazwaProduktu", nazwaProduktu);

                command.ExecuteNonQuery();

                connection.Close();
            }

            using (MySqlCommand command = new MySqlCommand(ADD_PRODUCT_WARTOSCI, connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Kalorie", kcal);
                command.Parameters.AddWithValue("@Bialko", bialko);
                command.Parameters.AddWithValue("@Weglowodany", weglowodany);
                command.Parameters.AddWithValue("@Tluszcze", tluszcze);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public string[][] getProducts()
        {
            string Nazwa = "";
            double Tluszcz = 0.0;
            double Weglowodany = 0.0;
            double Bialko = 0.0;
            double Kalorie = 0.0;
            int i = 0;
            List < string[] > produkt = new List<string[]>();

            //tylko w tym zasiegu nigdzie indziej nie wychodzi
            using (MySqlCommand command = new MySqlCommand(SELECT_PRODUCTS, connection))
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    produkt.Add(new string[5]);
                    Nazwa = (string)reader["Nazwa"];
                    Tluszcz = Convert.ToDouble(reader["Tluszcze"].ToString());
                    Weglowodany = Convert.ToDouble(reader["Weglowodany"].ToString());
                    Bialko = Convert.ToDouble(reader["Bialka"].ToString());
                    Kalorie = LiczenieKaloriiProduktu(Tluszcz, Bialko, Weglowodany, 100); 
                    produkt[i][0] = Nazwa;
                    produkt[i][1] = Tluszcz.ToString();
                    produkt[i][2] = Bialko.ToString();
                    produkt[i][3] = Weglowodany.ToString();
                    produkt[i][4] = Kalorie.ToString();
                    i++;
                }
                connection.Close();
                return produkt.ToArray();
            }
        }

        public double LiczenieKaloriiProduktu(double tluszcz, double bialko, double weglowodany, double waga)
        {
            double wynik;
            double mnoznik = waga / 100;
            wynik = (tluszcz * 7 + bialko * 4 + weglowodany * 4) * mnoznik;
            return wynik;
        }
    }
}
