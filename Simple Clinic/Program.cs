using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Clinic
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 LoginForm = new Form1();
            Application.Run(LoginForm);

            frmMain main = new frmMain(LoginForm.UserName);
            if (LoginForm.DialogResult == DialogResult.OK)
            {

                //When you want to start with Login Just in 'Run()' put main
                Application.Run(main);

            }
        }
    }
}
