using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace KalkulatorKalorii.DataBase
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }


        public Product(IDataReader dataReader)
        {
            Id = (int)dataReader["id_produktu"];
            Name = (string)dataReader["nazwa"];
            //Type = (int)dataReader["typ"];
        }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}

