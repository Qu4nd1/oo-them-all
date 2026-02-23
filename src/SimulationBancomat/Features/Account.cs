//******************************************************************************************
// ETML
// Auteur : Kentin Fankhauser
// Date : 23/02/2026
// Description : Visualisation du compte client et modification si nécessaire
//******************************************************************************************
using SimulationBancomat.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features
{
    class Account
    {
        private decimal bankMoneyAmount;
        public decimal BankMoneyAmount
        {
            get
            {
                return this.bankMoneyAmount;
            }
            set 
            {
                if (value > 0 && value < this.bankMoneyAmount)
                    this.bankMoneyAmount = this.bankMoneyAmount - value;
                else
                {
                    MessageBox(IntPtr.Zero, $"Vous ne pouvez pas retirer plus que: {this.bankMoneyAmount:c}", "Erreur", 16);
                }
            }
        }
        private string showAmount;
        public string ShowAmount
        {
            get
            {
                return this.showAmount;
            }
        }

        public void Show(decimal bankMoneyAmount)
        {
            string showAmount = $"Solde: {bankMoneyAmount:c}";
            int screenheigth = 5;
            int centerWriting = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - (showAmount.Length / 2));
            char keyChar;

            SuperConsole console = new SuperConsole();

            console.Presentation();
            console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth);
            console.DrawAtString(centerWriting, PasswordData.PASSWORD_SCREEN_Y, showAmount);
            console.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 21, ((PasswordData.PASSWORD_SCREEN_Y + 2)), 62, "Appuyer sur 'Q' pour revenir au menu des options");

            ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
            keyChar = key.KeyChar;
            if (keyChar.ToString().ToUpper() == "Q")
                return;

        }
    }
}
