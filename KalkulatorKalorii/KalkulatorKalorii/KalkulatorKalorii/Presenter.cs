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

        }

        #region Functions

        #endregion
    }
}
