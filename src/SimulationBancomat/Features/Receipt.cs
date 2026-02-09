using SimulationBancomat.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationBancomat.Program;
using static SimulationBancomat.Features.Transactions;

namespace SimulationBancomat.Features
{
    static class Receipt
    {
        static public void Print(ref string[] withdrawalMoneyLogs, int moneyWithdrawalIndex)
        {
            int optionPosX = PasswordData.PASSWORD_SCREEN_X - 15;
            int optionPosY = PasswordData.PASSWORD_SCREEN_Y;
            int shift = withdrawalMoneyOptions[0].Length;
            int centerWritingSolde = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - (showAmount.Length / 2));
            char keyChar;
            int receiptHeigth =withdrawalTimes * (2) + 5;
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y + 1);
            Console.WriteLine("Veuillez sélectionner une option: ");
            for (int i = 0; i < withdrawalMoneyOptions.Length; i++)
            {
                Console.SetCursorPosition(optionPosX, optionPosY + 3);
                Console.WriteLine($"{i + 1}. {withdrawalMoneyOptions[i]}");
                optionPosX += (shift + 5);
            }
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                keyChar = key.KeyChar;
                if (char.IsDigit(keyChar))
                {
                    if (keyChar.ToString() == "1")
                    {
                        int screenWidth = 55;

                        SuperConsole.Presentation();
                        SuperConsole.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 17, PasswordData.PASSWORD_SCREEN_Y - 1, screenWidth, receiptHeigth);
                        SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, PasswordData.PASSWORD_SCREEN_Y, screenWidth, $"{showAmount}");
                        for (int i = 0; i < moneyWithdrawalIndex; i++)
                        {
                            if (i == 0)
                            {
                                SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, (PasswordData.PASSWORD_SCREEN_Y + 2 + (i * 2)), screenWidth, $"{withdrawalMoneyLogs[i]:c}");
                            }
                            else
                            {
                                SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, (PasswordData.PASSWORD_SCREEN_Y + 2 + (i * 2)), screenWidth, $"{withdrawalMoneyLogs[i]:c}");
                            }
                        }
                        SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, ((PasswordData.PASSWORD_SCREEN_Y - 1) + (receiptHeigth - 2)), screenWidth, "Appuyer sur 'Q' pour revenir au menu des options");
                    }
                    else if (keyChar.ToString() == "2")
                    {
                        return;
                    }
                    else
                    {
                        MessageBox(IntPtr.Zero, "La valeur attendue est un entier", "Erreur", 16);
                    }
                }
            } while (keyChar.ToString().ToUpper() != "Q");
        }
    }
}
