using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalkulatorKalorii.DataBase;

namespace KalkulatorKalorii
{
    class Model
    {
        private static Model instance = null;
        public static Model Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Model();
                }
                return instance;
            }
        }

        public double obliczBMI(double masaBMI, double wzrostBMI)
        {
            
            double wynik;
            double wysokosc = wzrostBMI / 100;
            wynik = masaBMI / (wysokosc * wysokosc);
            wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
            return wynik;
        }

        public double obliczBMRkobieta(double masaBMR, double wzrostBMR ,double wiekBMR)
        {
            
            double wynik;
            wynik = (9.99 * masaBMR) + (6.25 * wzrostBMR) - (4.92 * wiekBMR) - 161;
            wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
            return wynik;
        }
        public double obliczBMRmezczyzna(double masaBMR, double wzrostBMR, double wiekBMR)
        {
            
            double wynik;
            wynik = (9.99 * masaBMR) + (6.25 * wzrostBMR) - (4.92 * wiekBMR) +5;
            wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
            return wynik;
        }

        public double zapotrzebowaniek(double masaDZ, double wzrostDZ, double wiekDZ, double wspl)
        {
            double wynik;
            wynik = (9.99 * masaDZ) + (6.25 * wzrostDZ) - (4.92 * wiekDZ) - 161;
            wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
            if (wspl == 0)
            {
                wynik = wynik * 1.0;
               
            }
            else if (wspl == 1)
            {
                wynik = wynik * 1.2;
                
            }
            else if (wspl == 2)
            {
                wynik = wynik * 1.4;
                
            }
            else if (wspl == 3)
            {
                wynik = wynik * 1.6;
                
            }
            else if (wspl == 4)
            {
                wynik = wynik * 1.8;
                
            }
            else if (wspl == 5)
            {
                wynik = wynik * 2.0;
                
            }
            return wynik;
        }
        public double zapotrzebowaniem(double masaDZ, double wzrostDZ, double wiekDZ, double wspl)
        {
            double wynik;
            wynik = (9.99 * masaDZ) + (6.25 * wzrostDZ) - (4.92 * wiekDZ) +5;
            wynik = Math.Round(wynik, 2, MidpointRounding.AwayFromZero);
            if (wspl == 0)
            {
                wynik = wynik * 1.0;

            }
            else if (wspl == 1)
            {
                wynik = wynik * 1.2;

            }
            else if (wspl == 2)
            {
                wynik = wynik * 1.4;

            }
            else if (wspl == 3)
            {
                wynik = wynik * 1.6;

            }
            else if (wspl == 4)
            {
                wynik = wynik * 1.8;

            }
            else if (wspl == 5)
            {
                wynik = wynik * 2.0;

            }
            return wynik;
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
