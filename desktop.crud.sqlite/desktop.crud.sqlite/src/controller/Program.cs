using desktop.crud.sqlite.src.ctrl;
using desktop.crud.sqlite.src.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * author		Antonio Membrides Espinosa
 * update       13/08/2019
 * version    	1.0
 */
namespace desktop.crud.sqlite
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CtrlMain controller = new CtrlMain();
            controller.setViewAdd(new FormAdd());
            controller.setViewMain(new FormMain());

            Application.Run(controller.getViewMain());
        }
    }
}
