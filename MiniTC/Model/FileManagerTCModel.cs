using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiniTC.Model
{
    class FileManagerTCModel : FileManagerTC
    {
        public void Copy(string skad, string nazwa, string dokad) 
        {

            if (nazwa != null && nazwa.Contains("<D>"))
            {
                MessageBox.Show("Nie mozna skopiować :(");
            }
            if(nazwa != null)
            {
                var sciezka1 = Path.Combine(skad, nazwa);
                var sciezka2 = Path.Combine(dokad, nazwa);
                File.Copy(sciezka1, sciezka2, true);
            }
            
        }
    }
}
