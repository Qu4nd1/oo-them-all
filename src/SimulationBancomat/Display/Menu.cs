using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationBancomat;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Display
{
    static class Menu
    {
        static public void MenuChoice(string[] crtBancomatOptions, ref decimal bankMoneyAmount, ref decimal[] withdrawalMoneyLogs, string[] withdrawalMoneyOptions)
        {
            char keyChar;
            bool keyValidity = false;
            bool transactionsFinished = false;
            int timesDone = 0;
            int moneyWithdrawalIndex = 0;
            do
            {
                SuperConsole.Presentation();
                SuperConsole.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, crtBancomatOptions.Length + 6);
                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
                Console.WriteLine("Veuillez choisir l'action désirer !\n");
                for (int i = 0; i < crtBancomatOptions.Length; i++)
                {
                    Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 20, PasswordData.PASSWORD_SCREEN_Y + (i + 1) * 2);
                    Console.WriteLine($"\t{i + 1}. {crtBancomatOptions[i]}");
                }

                ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                keyChar = key.KeyChar;

                if (char.IsDigit(keyChar))
                {
                    keyValidity = true;

                    switch (keyChar)
                    {
                        case '1':
                            SuperConsole.Presentation();
                            GetOutMoney(ref bankMoneyAmount, ref withdrawalMoneyLogs, withdrawalMoneyOptions, ref moneyWithdrawalIndex);
                            transactionsFinished = false;
                            break;
                        case '2':
                            SuperConsole.Presentation();
                            SeeAmount(bankMoneyAmount);
                            transactionsFinished = false;
                            break;
                        case '3':
                            SuperConsole.Presentation();
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
