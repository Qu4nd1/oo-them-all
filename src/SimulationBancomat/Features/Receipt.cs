//******************************************************************************************
// ETML
// Auteur : Kentin Fankhauser
// Date : 23/02/2026
// Description : Affichage du reçu des transactions effectuées
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
    class Receipt
    {
        public void Print(SuperConsole console, Account account, Transactions transaction)
        {
            int optionPosX = PasswordData.PASSWORD_SCREEN_X - 15;
            int optionPosY = PasswordData.PASSWORD_SCREEN_Y;
            int shift = transaction.MoneyOptions[0].Length;
            int centerWritingSolde = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - ($"{account.ShowAmount}".Length / 2));
            char keyChar;
            int receiptHeigth = transaction.withdrawalTimes * (2) + 5;

            
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y + 1);
            Console.WriteLine("Veuillez sélectionner une option: ");
            for (int i = 0; i < transaction.MoneyOptions.Length; i++)
            {
                Console.SetCursorPosition(optionPosX, optionPosY + 3);
                Console.WriteLine($"{i + 1}. {transaction.MoneyOptions[i]}");
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

                        console.Presentation();
                        console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 17, PasswordData.PASSWORD_SCREEN_Y - 1, screenWidth, receiptHeigth);
                        console.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, PasswordData.PASSWORD_SCREEN_Y, screenWidth, $"{account.ShowAmount}");
                        for (int i = 0; i < transaction.moneyMovementIndex; i++)
                        {
                            if (i == 0)
                            {
                                console.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, (PasswordData.PASSWORD_SCREEN_Y + 2 + (i * 2)), screenWidth, $"{transaction.moneyLogs[i]:c}");
                            }
                            else
                            {
                                console.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, (PasswordData.PASSWORD_SCREEN_Y + 2 + (i * 2)), screenWidth, $"{transaction.moneyLogs[i]:c}");
                            }
                        }
                        console.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, ((PasswordData.PASSWORD_SCREEN_Y - 1) + (receiptHeigth - 2)), screenWidth, "Appuyer sur 'Q' pour revenir au menu des options");
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
