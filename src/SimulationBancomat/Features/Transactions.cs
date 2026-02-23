//******************************************************************************************
// ETML
// Auteur : Kentin Fnakhauser
// Date : 23/02/2026
// Description : Retrait ou Dépot d'argent
//******************************************************************************************
using SimulationBancomat.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationBancomat.Features;
using static SimulationBancomat.Program;
using System.Transactions;

namespace SimulationBancomat.Features
{
    class Transactions
    {
        private decimal[] amountOptions = new decimal[]
        {
            20,
            50,
            80,
            100
        };
        private int customAmount = 0;
        int screenheigth = 16;
        public string[] moneyLogs = new string[100];
        public string[] MoneyOptions = new string[]
        {
                "Avec reçu",
                "Sans reçu"
        };
        public int withdrawalTimes = 0;
        public bool moneyMovementDirection;
        public int moneyMovementIndex = 0;

        public void MoneyMovement(Account account, Transactions transaction, decimal BankMoneyAmount, string[] moneyLogs, string[] withdrawalMoneyOptions, ref int moneyWithdrawalIndex, bool moneyMovementDirection)
        {
            int centerWriting = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - ($"{account.ShowAmount}".Length / 2));

            SuperConsole console = new SuperConsole();
            Receipt receipt = new Receipt();

            console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth);
            console.DrawAtString(centerWriting, PasswordData.PASSWORD_SCREEN_Y, account.ShowAmount);
            switch (moneyMovementDirection)
            {
                case true:
                    PutInMoneyAmount(console, account, transaction);
                    break;
                case false:
                    GetOutMoneyAmount(console, account, transaction);
                    break;
            }
            console.Presentation();
            console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth - 8);
            receipt.Print(console, account, transaction);
        }
        void GetOutMoneyAmount(SuperConsole console, Account account, Transactions transaction)
        {
            int moneyPosX = PasswordData.PASSWORD_SCREEN_X - 12;
            int moneyPosY = PasswordData.PASSWORD_SCREEN_Y;
            int shiftX = 25;
            int writePosX = moneyPosX;
            int shiftY = 2;
            int writePosY = moneyPosY;

            string customAmountIndex = "";
            string question = "Veuillez indiquer le montant a retirer: ";
            bool customGetOutAmountValidity = false;
            char keyChar;
            bool keyValidity = false;
            int amountIndex = 0;
            string customAmountAnswer;

            Console.CursorVisible = false;

            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
            Console.WriteLine("Veuillez sélectionner la quantité d'argent a retirer :");

            for (int i = 0; i < amountOptions.Length + 1; i++)
            {

                if (i <= (amountOptions.Length + 1) / 2)
                {
                    if (i <= amountOptions.Length)
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"\t{i + 1}: {amountOptions[i]:c}");
                    }

                }
                else
                {
                    if (i < amountOptions.Length)
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"{i + 1}: {amountOptions[i]:c}");
                    }
                    else
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"{i + 1}: Autre montant");
                    }
                }
                shiftY += 2;
                if (i == (amountOptions.Length) / 2)
                {
                    writePosX += shiftX;
                    shiftY = 2;
                }
            }
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                keyChar = key.KeyChar;

                if (char.IsDigit(keyChar))
                { 
                    customAmountIndex = Convert.ToString(amountOptions.Length + 1);
                    keyValidity = true;
                    withdrawalTimes++;

                    if (keyChar.ToString() == customAmountIndex)
                    {
                        do
                        {
                            Console.CursorVisible = true;
                            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                            Console.Write(question);
                            do
                            {
                                Console.SetCursorPosition((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                                customAmountAnswer = Console.ReadLine();
                                keyValidity = int.TryParse(customAmountAnswer, out customAmount);
                                if (keyValidity == false)
                                {
                                    MessageBox(IntPtr.Zero, "La valeur attendue est un entier", "Erreur", 16);
                                    console.ClearAtForLength((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2), customAmountAnswer.Length);
                                }
                            } while (keyValidity != true);

                            if (customAmount > account.BankMoneyAmount)
                            {
                                customGetOutAmountValidity = false;
                                
                                console.ClearAtForLength((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2), customAmount.ToString().Length);
                            }
                            else
                                customGetOutAmountValidity = true;
                        } while (customGetOutAmountValidity != true);
                        account.Withdraw(customAmount);
                        transaction.moneyLogs[moneyMovementIndex] = $"- {customAmount:c}";
                        moneyMovementIndex++;
                    }
                    else if (keyChar.ToString() == "1" || keyChar.ToString() == "2" || keyChar.ToString() == "3" || keyChar.ToString() == "4")
                    {
                        amountIndex = Convert.ToInt32(keyChar.ToString());
                        amountIndex -= 1;
                        account.Withdraw(amountOptions[amountIndex]);
                        transaction.moneyLogs[moneyMovementIndex] = $"- {amountOptions[amountIndex]:c}";
                        moneyMovementIndex++;
                    }
                    else
                    {
                        keyValidity = false;
                        MessageBox(IntPtr.Zero, "La valeur donnée ne fait pas partie des entiers attendus", "Erreur", 16);
                    }
                }
                else
                {
                    keyValidity = false;
                    MessageBox(IntPtr.Zero, "La valeur attendue est un entier", "Erreur", 16);
                }
                Console.CursorVisible = false;
            } while (keyValidity != true);
        }
       void PutInMoneyAmount(SuperConsole console, Account account, Transactions transaction)
        {
            int moneyPosX = PasswordData.PASSWORD_SCREEN_X - 12;
            int moneyPosY = PasswordData.PASSWORD_SCREEN_Y;
            int shiftX = 25;
            int writePosX = moneyPosX;
            int shiftY = 2;
            int writePosY = moneyPosY;
            string customAmountIndex = "";
            string question = "Veuillez indiquer le montant a déposer: ";
            bool customPutInAmountValidity = false;
            char keyChar;
            bool keyValidity = false;
            int amountIndex = 0;
            string customAmountAnswer;

            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
            Console.WriteLine("Veuillez sélectionner la quantité d'argent a déposer :");

            for (int i = 0; i < amountOptions.Length + 1; i++)
            {

                if (i <= (amountOptions.Length + 1) / 2)
                {
                    if (i <= amountOptions.Length)
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"\t{i + 1}: {amountOptions[i]:c}");
                    }

                }
                else
                {
                    if (i < amountOptions.Length)
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"{i + 1}: {amountOptions[i]:c}");
                    }
                    else
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"{i + 1}: Autre montant");
                    }
                }
                shiftY += 2;
                if (i == (amountOptions.Length) / 2)
                {
                    writePosX += shiftX;
                    shiftY = 2;
                }
            }
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                keyChar = key.KeyChar;
                withdrawalTimes++;

                if (char.IsDigit(keyChar))
                {

                    customAmountIndex = Convert.ToString(amountOptions.Length + 1);
                    keyValidity = true;


                    if (keyChar.ToString() == customAmountIndex)
                    {
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                        Console.Write(question);
                        do
                        {
                            Console.SetCursorPosition((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                            customAmountAnswer = Console.ReadLine();
                            keyValidity = int.TryParse(customAmountAnswer, out customAmount);
                            if (keyValidity == false)
                            {
                                MessageBox(IntPtr.Zero, "La valeur attendue est un entier", "Erreur", 16);
                                for (int i = 0; i < customAmountAnswer.Length; i++)
                                    console.ClearAt((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length + i, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                            }
                        } while (keyValidity != true);
                        account.Deposit(customAmount);
                        transaction.moneyLogs[moneyMovementIndex] = $"+ {customAmount:c}";
                        moneyMovementIndex++;
                    }
                    //deposit
                    else if (keyChar.ToString() == "1" || keyChar.ToString() == "2" || keyChar.ToString() == "3" || keyChar.ToString() == "4")
                    {
                        amountIndex = Convert.ToInt32(keyChar.ToString());
                        amountIndex -= 1;
                        account.Deposit(amountOptions[amountIndex]);
                        transaction.moneyLogs[moneyMovementIndex] = $"+ {amountOptions[amountIndex]:c}";
                        moneyMovementIndex++;
                    }
                    else
                    {
                        keyValidity = false;
                        MessageBox(IntPtr.Zero, "La valeur donnée ne fait pas partie des entiers attendus", "Erreur", 16);
                    }
                }
                else
                {
                    keyValidity = false;
                    MessageBox(IntPtr.Zero, "La valeur attendue est un entier", "Erreur", 16);
                }
                Console.CursorVisible = false;
            } while (keyValidity != true);
        }
    }
}
