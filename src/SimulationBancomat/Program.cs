using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using SimulationBancomat.Display;
using static SimulationBancomat.Display.SuperConsole;

namespace SimulationBancomat
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]

        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        public struct PasswordData
        {
            public bool passwordValidity;
            public int password;
            public int tempPassword;
            public const int PASSWORD_SCREEN_X = 40;
            public const int PASSWORD_SCREEN_Y = 16;
            public const int PASSWORD_LENGTH = 6;

            // Constructeur
            public PasswordData(bool validity, int pwd, int tempPwd)
            {
                passwordValidity = validity;
                password = pwd;
                tempPassword = tempPwd;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            PasswordData data = new PasswordData(false, 123456, 0);

            char keyChar = ' ';

            string[] bancomatOptions = new string[]
            {
                "Retirer de l'argent",
                "Consulter mon solde",
                "Quitter"
            };

            decimal bankMoneyAmount = 1000;

            decimal[] withdrawalMoneyLogs = new decimal[100];
            string[] withdrawalMoneyOptions = new string[]
            {
                "Avec reçu",
                "Sans reçu"
            };

            do
            {
                SuperConsole.Presentation();
                SuperConsole.PasswordVisualSuppression();
                SuperConsole.DrawNumPad(emptyButton, keyChar);

                data.passwordValidity = VerifyCode(ref data, emptyButton, keyChar);

                if (data.passwordValidity == true)
                {
                    SuperConsole.Presentation();
                    Menu.MenuChoice(bancomatOptions, ref bankMoneyAmount, ref withdrawalMoneyLogs, withdrawalMoneyOptions);
                }
            } while (data.passwordValidity != true);

                Console.ReadLine();
        }

        

        static bool VerifyCode(ref PasswordData data, string[] emptyButton, char keyChar)
        {
            Console.CursorVisible = true;
            string input = " ";

            do
            {
                for (int i = 0; i < PasswordData.PASSWORD_LENGTH; i++)
                {
                    Console.SetCursorPosition((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1);

                    ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                    keyChar = key.KeyChar;

                    // Vérifier si c'est un chiffre
                    if (char.IsDigit(keyChar))
                    {
                        input += keyChar;
                        SuperConsole.DrawAtChar((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1, keyChar);  // Afficher la valeur puis
                        SuperConsole.DrawNumPad(emptyButton,keyChar);
                        Thread.Sleep(50);
                        SuperConsole.DrawAtChar((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1, '*'); // Affichage des numéros entrés
                    }
                    else
                    {
                        MessageBox(IntPtr.Zero, "Veuillez entrer uniquement des chiffres", "Entrée invalide", 48);
                        i--;  // Recommencer cette position
                    }
                }
                // Après la boucle, vérifier le mot de passe complet
                data.passwordValidity = int.TryParse(input, out data.tempPassword);
                if (data.tempPassword == data.password)
                {
                    data.passwordValidity = true;
                }
                else
                {
                    data.passwordValidity = false;
                    input = "";
                    data.tempPassword = 0;
                    MessageBox(IntPtr.Zero, "Code incorrect", "Erreur", 16);

                    for (int i = 0; i < PasswordData.PASSWORD_LENGTH; i++)
                    {
                        Console.SetCursorPosition((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1);
                        Console.Write("_");
                    }
                }
            } while (data.passwordValidity == false);
            
            return data.passwordValidity;
        }

        
        static public void GetOutMoney(ref decimal bankMoneyAmount, ref decimal[] withdrawalMoneyLogs, string[] withdrawalMoneyOptions, ref int moneyWithdrawalIndex)
        {
            string showAmount = $"Solde: {bankMoneyAmount:c}";
            int centerWriting = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62/2) - (showAmount.Length/2));
            decimal[] getOutAmountOptions = new decimal[]
            {
                20,
                50,
                80,
                100
            };
            int customGetOutAmount = 0;
            int screenheigth = 16;
            int withdrawalTimes = 0;


            SuperConsole.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth);
            SuperConsole.DrawAtString(centerWriting, PasswordData.PASSWORD_SCREEN_Y, showAmount);
            GetOutMoneyAmount(ref bankMoneyAmount, ref withdrawalMoneyLogs, ref moneyWithdrawalIndex);
            showAmount = $"Solde: {bankMoneyAmount:c}";
            SuperConsole.Presentation();
            SuperConsole.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth - 8);
            PrintReceipt(ref withdrawalMoneyLogs, moneyWithdrawalIndex);

            void GetOutMoneyAmount (ref decimal bankMoneyAmount, ref decimal[] withdrawalMoneyLogs, ref int moneyWithdrawalIndex)
            {
                int moneyPosX = PasswordData.PASSWORD_SCREEN_X - 12;
                int moneyPosY = PasswordData.PASSWORD_SCREEN_Y;
                int shiftX =  25;
                int writePosX = moneyPosX;
                int shiftY = 2;
                int writePosY = moneyPosY;

                string customAmountIndex = "";
                string question = "Veuillez indiquer le motant a retirer: ";
                bool customGetOutAmountValidity = false;
                char keyChar;
                bool keyValidity = false;
                int amountIndex = 0;


                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
                Console.WriteLine("Veuillez sélectionner la quantité d'argent a retirer :");

                for (int i = 0; i < getOutAmountOptions.Length + 1; i++)
                {
                    
                    if (i <= (getOutAmountOptions.Length + 1)/2)
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
                        

                        if (keyChar.ToString() == customAmountIndex)
                        {
                            do
                            { 
                                Console.CursorVisible = true;
                                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                                Console.Write(question);
                                Console.SetCursorPosition((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length, PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                                customGetOutAmount = Convert.ToInt32(Console.ReadLine());

                                if (customGetOutAmount > bankMoneyAmount)
                                {
                                    customGetOutAmountValidity = false;
                                    MessageBox(IntPtr.Zero, $"Vous ne pouvez pas retirer plus que: {bankMoneyAmount:c}", "Erreur", 16);
                                    for (int i = 0; i < customGetOutAmount.ToString().Length; i++)
                                        SuperConsole.ClearAt((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length + (i), PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
                                }
                                else
                                    customGetOutAmountValidity = true;
                            } while (customGetOutAmountValidity != true);
                            bankMoneyAmount = bankMoneyAmount - customGetOutAmount;
                            withdrawalMoneyLogs[moneyWithdrawalIndex] = customGetOutAmount;
                            moneyWithdrawalIndex++;
                        }
                        else if(keyChar.ToString() == "1" || keyChar.ToString() == "2" || keyChar.ToString() == "3" || keyChar.ToString() == "4")
                        {
                            amountIndex = Convert.ToInt32(keyChar.ToString());
                            amountIndex -= 1;
                            bankMoneyAmount = bankMoneyAmount - getOutAmountOptions[amountIndex];
                            withdrawalMoneyLogs[moneyWithdrawalIndex] = getOutAmountOptions[amountIndex];
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
                } while (keyValidity != true);
            }
            void PrintReceipt(ref decimal[] withdrawalMoneyLogs, int moneyWithdrawalIndex)
            {
                int optionPosX = PasswordData.PASSWORD_SCREEN_X - 15;
                int optionPosY = PasswordData.PASSWORD_SCREEN_Y;
                int shift = withdrawalMoneyOptions[0].Length;
                int centerWritingSolde = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - (showAmount.Length / 2));
                char keyChar;
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
                            withdrawalTimes++;
                            SuperConsole.Presentation();
                            SuperConsole.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 17, PasswordData.PASSWORD_SCREEN_Y - 1, screenWidth, screenheigth);
                            SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, PasswordData.PASSWORD_SCREEN_Y, screenWidth, $"{showAmount}");
                            for (int i = 0; i < moneyWithdrawalIndex; i++)
                            {
                                if (i == 0)
                                {
                                    SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, (PasswordData.PASSWORD_SCREEN_Y+2 + (i*1)), screenWidth, $"n°{i + 1}: -{withdrawalMoneyLogs[i]:c}");
                                }
                                else
                                {
                                    SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, (PasswordData.PASSWORD_SCREEN_Y+2 + (i*1)), screenWidth, $"n°{i + 1}: -{withdrawalMoneyLogs[i]:c}");
                                }
                            }
                            SuperConsole.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, ((PasswordData.PASSWORD_SCREEN_Y - 1) + (screenheigth - 2)), screenWidth, "Appuyer sur 'Q' pour revenir au menu des options");
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

        static public void SeeAmount(decimal bankMoneyAmount)
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