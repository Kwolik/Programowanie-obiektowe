using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulatorKalorii
{
    interface IView
    {
        #region Properties
        string[] Produkt { get; set; }
        double Wynik { get; set; }

        #endregion

        #region Events
        event Action <double,double> obliczBMI;
        event Action<double, double, double> obliczBMRkobieta;
        event Action<double, double, double> obliczBMRmezczyzna;
        event Action<double, double, double, double> zapotrzebowaniek;
        event Action<double, double, double, double> zapotrzebowaniem;
        event Action<double, double, double, double> LiczenieKaloriiProduktu;
        #endregion
    }
}
