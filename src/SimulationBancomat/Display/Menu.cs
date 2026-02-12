using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationBancomat;
using SimulationBancomat.Features;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Display
{
    class Menu
    {

        public void Choice(SuperConsole console, Transactions transaction, Account account, ref decimal bankMoneyAmount, ref string[] withdrawalMoneyLogs, string[] withdrawalMoneyOptions)
        {
            char keyChar;
            bool keyValidity = false;
            bool transactionsFinished = false;
            int timesDone = 0;
            int moneyWithdrawalIndex = 0;
            string[] bancomatOptions = new string[]
            {
                "Deposer de l'argent",
                "Retirer de l'argent",
                "Consulter mon solde",
                "Quitter"
            };

            do
            {
                console.Presentation();
                console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, bancomatOptions.Length + 8);
                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
                Console.WriteLine("Veuillez choisir l'action désirer !\n");
                for (int i = 0; i < bancomatOptions.Length; i++)
                {
                    Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 20, PasswordData.PASSWORD_SCREEN_Y + (i + 1) * 2);
                    Console.WriteLine($"\t{i + 1}. {bancomatOptions[i]}");
                }

                ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                keyChar = key.KeyChar;

                if (char.IsDigit(keyChar))
                {
                    keyValidity = true;

                    switch (keyChar)
                    {
                        case '1':
                            console.Presentation();
                            transaction.MoneyMovement(ref bankMoneyAmount, ref withdrawalMoneyLogs, withdrawalMoneyOptions, ref moneyWithdrawalIndex,true);
                            transactionsFinished = false;
                            break;
                        case '2':
                            console.Presentation();
                            transaction.MoneyMovement(ref bankMoneyAmount, ref withdrawalMoneyLogs, withdrawalMoneyOptions, ref moneyWithdrawalIndex,false);
                            transactionsFinished = false;
                            break;
                        case '3':
                            console.Presentation();
                            account.See(bankMoneyAmount);
                            transactionsFinished = false;
                            break;
                        case '4':
                            console.Presentation();
                            transactionsFinished = true;
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    keyValidity = false;
                    MessageBox(IntPtr.Zero, "La valeur attendue est un entier", "Erreur", 16);
                }
                timesDone++;
                Console.Clear();
            } while (keyValidity != true || transactionsFinished != true);
        }
    }
}
