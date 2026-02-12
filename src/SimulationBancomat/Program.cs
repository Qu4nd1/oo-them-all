using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using SimulationBancomat.Display;
using SimulationBancomat.Features;
using static SimulationBancomat.Display.SuperConsole;
using static SimulationBancomat.Display.Menu;
using static SimulationBancomat.Features.Transactions;
using static SimulationBancomat.Features.Verifications;

namespace SimulationBancomat
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]

        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //***** Code donner par Anthropic Claude (IA) pour changer le format d'affichage monétaire *****
            CultureInfo culture = (CultureInfo)CultureInfo.GetCultureInfo("fr-CH").Clone();
            culture.NumberFormat.CurrencySymbol = "CHF";
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            //**********************************************************************************************

            PasswordData data = new PasswordData(false, 123456, 0);
            SuperConsole console = new SuperConsole();
            Verifications verification = new Verifications();
            Menu menu = new Menu();
            Transactions transaction = new Transactions();
            Account account = new Account();
            console.Presentation();
            console.DrawNumPad(console.emptyButton, verification.keyChar);
            verification.Code(console, data, console.emptyButton, verification.keyChar);

            console.Presentation();
            menu.Choice(console, transaction, account, ref bankMoneyAmount, ref withdrawalMoneyLogs, withdrawalMoneyOptions);

            Console.ReadLine();
        }
    }
}