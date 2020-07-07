using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkulatorKalorii
{
    interface IView
    {
        #region Propertisy
        string[] aa { get; set; }
        string[] bb { get; set; }
        string cc { get; set; }
        #endregion

        #region Events
        event Action a;
        event Action b;
        event Action c;
        event Action d;
        event Action e;
        #endregion
    }
}
