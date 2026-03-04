//******************************************************************************************
// ETML
// Auteur : Kentin Fankhauser
// Date : 23/02/2026
// Description : Simulation qui reproduit l'interface d'un bancomat sans pour autant traiter de l'argent réel
//******************************************************************************************
using System.Globalization;
using System.Runtime.InteropServices;
using SimulationBancomat.Display;
using SimulationBancomat.Features;

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

            PasswordData data = new PasswordData(123456);
            SuperConsole console = new SuperConsole();
            Verifications verification = new Verifications();
            Menu menu = new Menu();
            Transactions transaction = new Transactions();
            Account account = menu.AtmOpenning(console);
            console.DrawNumPad(console.emptyButton, verification.keyChar);
            verification.Code(console, data, console.emptyButton);

            console.Presentation();
            menu.Choice(console, transaction, account);

            Console.ReadLine();
        }
    }
}