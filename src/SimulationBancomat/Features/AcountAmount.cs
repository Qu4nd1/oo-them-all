using SimulationBancomat.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features
{
    static class AccountAmount
    {
        static public void See(decimal bankMoneyAmount)
        {
            string showAmount = $"Solde: {bankMoneyAmount:c}";
            int screenheigth = 5;
            int centerWriting = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - (showAmount.Length / 2));
            char keyChar;

            SuperConsole.Presentation();
            SuperConsole.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth);
            SuperConsole.DrawAtString(centerWriting, PasswordData.PASSWORD_SCREEN_Y, showAmount);
            SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 21, ((PasswordData.PASSWORD_SCREEN_Y + 2)), 62, "Appuyer sur 'Q' pour revenir au menu des options");

            ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
            keyChar = key.KeyChar;
            if (keyChar.ToString().ToUpper() == "Q")
                return;

        }
    }
}
