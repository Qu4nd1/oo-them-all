using SimulationBancomat.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationBancomat.Features;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features
{
    static class Transactions
    {
        public static decimal[] getOutAmountOptions = new decimal[]
        {
            20,
            50,
            80,
            100
        };
        public static int customAmount = 0;
        public static int screenheigth = 16;
        public static decimal bankMoneyAmount = 1000;
        public static string[] withdrawalMoneyLogs = new string[100];
        public static string[] withdrawalMoneyOptions = new string[]
        {
                "Avec reçu",
                "Sans reçu"
        };
        public static string showAmount = $"Solde: {bankMoneyAmount:c}";
        public static int withdrawalTimes = 0;
        public static bool moneyMovementDirection;

        static public void MoneyMovement(ref decimal bankMoneyAmount, ref string[] withdrawalMoneyLogs, string[] withdrawalMoneyOptions, ref int moneyWithdrawalIndex, bool moneyMovementDirection)
        {
            int centerWriting = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - (showAmount.Length / 2));

            SuperConsole.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth);
            SuperConsole.DrawAtString(centerWriting, PasswordData.PASSWORD_SCREEN_Y, showAmount);
            switch (moneyMovementDirection)
            {
                case true:
                    PutInMoneyAmount(ref bankMoneyAmount, ref withdrawalMoneyLogs, ref moneyWithdrawalIndex);
                    break;
                case false:
                    GetOutMoneyAmount(ref bankMoneyAmount, ref withdrawalMoneyLogs, ref moneyWithdrawalIndex);
                    break;
            }
            showAmount = $"Solde: {bankMoneyAmount:c}";
            SuperConsole.Presentation();
            SuperConsole.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth - 8);
            Receipt.Print(ref withdrawalMoneyLogs, moneyWithdrawalIndex);
        }
        static public void GetOutMoneyAmount(ref decimal bankMoneyAmount, ref string[] withdrawalMoneyLogs, ref int moneyWithdrawalIndex)
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

            Console.CursorVisible = false;

            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
            Console.WriteLine("Veuillez sélectionner la quantité d'argent a retirer :");

            for (int i = 0; i < getOutAmountOptions.Length + 1; i++)
            {

                if (i <= (getOutAmountOptions.Length + 1) / 2)
                {
                    if (i <= getOutAmountOptions.Length)
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"\t{i + 1}: {getOutAmountOptions[i]:c}");
                    }

                }
                else
                {
                    if (i < getOutAmountOptions.Length)
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"{i + 1}: {getOutAmountOptions[i]:c}");
                    }
                    else
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"{i + 1}: Autre montant");
                    }
                }
                shiftY += 2;
                if (i == (getOutAmountOptions.Length) / 2)
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

                    customAmountIndex = Convert.ToString(getOutAmountOptions.Length + 1);
                    keyValidity = true;
                    withdrawalTimes++;


                    if (keyChar.ToString() == customAmountIndex)
                    {
                        do
                        {
                            Console.CursorVisible = true;
                            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                            Console.Write(question);
                            Console.SetCursorPosition((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                            customAmount = Convert.ToInt32(Console.ReadLine());

                            if (customAmount > bankMoneyAmount)
                            {
                                customGetOutAmountValidity = false;
                                MessageBox(IntPtr.Zero, $"Vous ne pouvez pas retirer plus que: {bankMoneyAmount:c}", "Erreur", 16);
                                for (int i = 0; i < customAmount.ToString().Length; i++)
                                    SuperConsole.ClearAt((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length + (i), PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                            }
                            else
                                customGetOutAmountValidity = true;
                        } while (customGetOutAmountValidity != true);
                        bankMoneyAmount = bankMoneyAmount - customAmount;
                        withdrawalMoneyLogs[moneyWithdrawalIndex] = $"- {customAmount:c}";
                        moneyWithdrawalIndex++;
                    }
                    else if (keyChar.ToString() == "1" || keyChar.ToString() == "2" || keyChar.ToString() == "3" || keyChar.ToString() == "4")
                    {
                        amountIndex = Convert.ToInt32(keyChar.ToString());
                        amountIndex -= 1;
                        bankMoneyAmount = bankMoneyAmount - getOutAmountOptions[amountIndex];
                        withdrawalMoneyLogs[moneyWithdrawalIndex] = $"- {getOutAmountOptions[amountIndex]:c}";
                        moneyWithdrawalIndex++;
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
       static public void PutInMoneyAmount(ref decimal bankMoneyAmount, ref string[] withdrawalMoneyLogs, ref int moneyWithdrawalIndex)
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

            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
            Console.WriteLine("Veuillez sélectionner la quantité d'argent a déposer :");

            for (int i = 0; i < getOutAmountOptions.Length + 1; i++)
            {

                if (i <= (getOutAmountOptions.Length + 1) / 2)
                {
                    if (i <= getOutAmountOptions.Length)
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"\t{i + 1}: {getOutAmountOptions[i]:c}");
                    }

                }
                else
                {
                    if (i < getOutAmountOptions.Length)
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"{i + 1}: {getOutAmountOptions[i]:c}");
                    }
                    else
                    {
                        Console.SetCursorPosition(writePosX, moneyPosY + shiftY);
                        Console.WriteLine($"{i + 1}: Autre montant");
                    }
                }
                shiftY += 2;
                if (i == (getOutAmountOptions.Length) / 2)
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

                    customAmountIndex = Convert.ToString(getOutAmountOptions.Length + 1);
                    keyValidity = true;


                    if (keyChar.ToString() == customAmountIndex)
                    {
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                        Console.Write(question);
                        Console.SetCursorPosition((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                        customAmount = Convert.ToInt32(Console.ReadLine());
                        bankMoneyAmount = bankMoneyAmount + customAmount;
                        withdrawalMoneyLogs[moneyWithdrawalIndex] = $"+ {customAmount:c}";
                        moneyWithdrawalIndex++;
                    }
                    else if (keyChar.ToString() == "1" || keyChar.ToString() == "2" || keyChar.ToString() == "3" || keyChar.ToString() == "4")
                    {
                        amountIndex = Convert.ToInt32(keyChar.ToString());
                        amountIndex -= 1;
                        bankMoneyAmount = bankMoneyAmount + getOutAmountOptions[amountIndex];
                        withdrawalMoneyLogs[moneyWithdrawalIndex] = $"+ {getOutAmountOptions[amountIndex]:c}";
                        moneyWithdrawalIndex++;
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
