using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KalkulatorKalorii.DataBase
{
    class TypeRepository
    {
        #region queries
        private string SELECT_ALL_TYPES = "SELECT * FROM `typ`";
        #endregion

        List<Type> types = new List<Type>();
        MySqlConnection connection = DB_Connect.Instance.Connection;

        public void getAllTypes()
        {
            //tylko w tym zasiegu nigdzie indziej nie wychodzi
            using (MySqlCommand command = new MySqlCommand(SELECT_ALL_TYPES, connection))
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    types.Add(new Type(reader));
                }
                connection.Close();
            }
            foreach (var u in types)
            {
                Console.WriteLine(u);
            }
        }
    }
}