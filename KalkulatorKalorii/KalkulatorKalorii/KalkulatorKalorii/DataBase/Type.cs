using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace KalkulatorKalorii.DataBase
{
    class Type
    {
        public int Id { get; set; }
        public string TypeName { get; set; }


        public Type(IDataReader dataReader)
        {
            Id = (int)dataReader["id_typu"];
            TypeName = (string)dataReader["nazwa_typu"];
        }

        public override string ToString()
        {
            return $"{Id} {TypeName} ";
        }
    }
}
