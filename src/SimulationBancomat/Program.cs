//******************************************************************************************
// ETML
// Auteur : Kentin Fankhauser
// Date : 23/02/2026
// Description : Simulation qui reproduit l'interface d'un bancomat sans pour autant traiter de l'argent réel
//******************************************************************************************
using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
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

            PasswordData data = new PasswordData(false, 123456, 0);
            SuperConsole console = new SuperConsole();
            Verifications verification = new Verifications();
            Menu menu = new Menu();
            Transactions transaction = new Transactions();
            Account account = new Account(1000);

            console.Presentation();
            console.DrawNumPad(console.emptyButton, verification.keyChar);
            verification.Code(console, data, console.emptyButton, verification.keyChar);

            console.Presentation();
            menu.Choice(console, transaction, account, account.BankMoneyAmount, transaction.moneyLogs, transaction.MoneyOptions);

            Console.ReadLine();
        }
    }
}