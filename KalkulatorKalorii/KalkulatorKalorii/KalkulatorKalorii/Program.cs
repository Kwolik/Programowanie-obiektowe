using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KalkulatorKalorii
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Model model = new Model();
            IView view = new View();
            Presenter presenter = new Presenter(view, model);
            Application.Run((Form)view);
        }
    }
}
