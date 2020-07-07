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

        private string SELECT_ALL_PRODUCTS = "SELECT * FROM `produkty`";

        #endregion

        List<Product> products = new List<Product>();
        MySqlConnection connection = DB_Connect.Instance.Connection;
        

        public void getAllProducts()
        {
            //tylko w tym zasiegu nigdzie indziej nie wychodzi
            using (MySqlCommand command = new MySqlCommand(SELECT_ALL_PRODUCTS, connection))
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product(reader));
                }
                connection.Close();
            }
            foreach (var u in products)
            {
                Console.WriteLine(u);
            }
        }
    }
}
