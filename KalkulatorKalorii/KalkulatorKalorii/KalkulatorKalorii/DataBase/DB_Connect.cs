using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Configuration;
using System.Resources;
using KalkulatorKalorii.DataBase;

namespace KalkulatorKalorii.DataBase
{
    class DB_Connect
    {
        private static DB_Connect instance = null;

        public MySqlConnection Connection { get; private set; }
        public static DB_Connect Instance
        {
            get
            {
                return instance ?? (instance = new DB_Connect());
            }

        }
        private DB_Connect()
        {
            ResourceManager rm = new ResourceManager("KalkulatorKalorii.DataBase.DB_Info", Assembly.GetExecutingAssembly());

            MySqlConnectionStringBuilder conStringBuilder = new MySqlConnectionStringBuilder();

            conStringBuilder.Port = uint.Parse(DB_Info.port);
            conStringBuilder.UserID = DB_Info.user;
            conStringBuilder.Server = DB_Info.server;
            conStringBuilder.Password = DB_Info.password;
            conStringBuilder.Database = DB_Info.database;

            Connection = new MySqlConnection(conStringBuilder.ToString());
        }
    }
}
