using System;
using System.Windows.Forms;
/*
 * author		Antonio Membrides Espinosa
 * email        tonykssa@gmail.com
 * update       27/08/2019
 * version    	1.0
 */
namespace KsRFS
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //.........................................................

            var view1 = new Form1();
            var ctr1 = new KsRFS.src.controller.Main();
            ctr1.setView(view1);

            //.........................................................
            Application.Run(view1);
        }
    }
}
