using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulatorKalorii
{
    class Presenter
    {
        IView view;
        Model model;
        public Presenter(IView view, Model model)
        {
            this.model = model;
            this.view = view;
            view.obliczBMI += View_obliczBMI;
            view.obliczBMRkobieta += View_obliczBMRkobieta;
            view.obliczBMRmezczyzna += View_obliczBMRmezczyzna;
            view.zapotrzebowaniek += View_zapotrzebowaniek;
            view.zapotrzebowaniem += View_zapotrzebowaniem;
            view.LiczenieKaloriiProduktu += View_LiczenieKaloriiProduktu;
        }

        #region Functions
        private void View_obliczBMI(double masaBMI,double wzrostBMI)
        {
            view.Wynik = model.obliczBMI(masaBMI, wzrostBMI);
        }
        private void View_obliczBMRkobieta(double masaBMR, double wzrostBMR, double wiekBMR)
        {
            view.Wynik = model.obliczBMRkobieta(masaBMR, wzrostBMR, wiekBMR);
        }
        private void View_obliczBMRmezczyzna(double masaBMR, double wzrostBMR, double wiekBMR)
        {
            view.Wynik = model.obliczBMRmezczyzna(masaBMR, wzrostBMR, wiekBMR);
        }
        private void View_zapotrzebowaniek(double masaDZ,double wzrostDZ,double wiekDZ, double wspl)
        {
            view.Wynik = model.zapotrzebowaniek(masaDZ, wzrostDZ, wiekDZ, wspl);
        }
        private void View_zapotrzebowaniem(double masaDZ, double wzrostDZ, double wiekDZ, double wspl)
        {
            view.Wynik = model.zapotrzebowaniem(masaDZ, wzrostDZ, wiekDZ, wspl);
        }

        private void View_LiczenieKaloriiProduktu(double tluszcz, double bialko, double weglowodany,double waga)
        {
            view.Wynik = model.LiczenieKaloriiProduktu(tluszcz, bialko, weglowodany, waga);
        }

        #endregion
    }
}
