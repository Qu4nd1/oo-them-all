using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace SimulationBancomat
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]

        public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        struct PasswordData
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

            string[] emptyButton = new string[]
            {
                "┌───┐",
                "│   │",
                "└───┘"
            };

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
                Presentation();
                PasswordVisualSuppression();
                DrawNumPad(emptyButton, keyChar);

                data.passwordValidity = VerifyCode(ref data, emptyButton, keyChar);

                if (data.passwordValidity == true)
                {
                    Presentation();
                    MenuChoice(bancomatOptions, ref bankMoneyAmount, ref withdrawalMoneyLogs, withdrawalMoneyOptions);
                }
            } while (data.passwordValidity != true);

                Console.ReadLine();
        }

        static void DrawNumPad(string[] emptyButton, char keyChar)
        {
            int initialButtonX = PasswordData.PASSWORD_SCREEN_X - 1;
            int initalButtonY = PasswordData.PASSWORD_SCREEN_Y + 3;
            int buttonX = initialButtonX;
            int buttonY = initalButtonY;
            int SCREEN_X = PasswordData.PASSWORD_SCREEN_X - 5;
            int SCREEN_Y = PasswordData.PASSWORD_SCREEN_Y - 1;
            const int HORIZONTAL_SPACE = 7;
            const int VERTICAL_SPACE = 4;

            Console.CursorVisible = false;

            DrawScreen(SCREEN_X, SCREEN_Y, 27, 21);
            DrawScreen(PasswordData.PASSWORD_SCREEN_X, PasswordData.PASSWORD_SCREEN_Y, 17, 3);


            for (int i = 0; i < 10; i++)
            {
                char padNumber = (char)('0' + ((i + 1) % 10));

                if (i == 3 || i == 6 || i == 9)
                {
                    buttonX = initialButtonX;
                    buttonY += VERTICAL_SPACE;
                    if (i == 9)
                        buttonX += HORIZONTAL_SPACE;
                }
                if (keyChar == padNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    for (int j = 0; j < 3; j++)
                        DrawAtString(buttonX, buttonY + j, emptyButton[j]);
                    DrawAtChar(buttonX + 2, buttonY + 1, padNumber);
                    Console.ResetColor();
                    Thread.Sleep(50);
                    for (int j = 0; j < 3; j++)
                        DrawAtString(buttonX, buttonY + j, emptyButton[j]);
                    DrawAtChar(buttonX + 2, buttonY + 1, padNumber);
                    buttonX += HORIZONTAL_SPACE;
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                        DrawAtString(buttonX, buttonY + j, emptyButton[j]);
                    DrawAtChar(buttonX + 2, buttonY + 1, padNumber);
                    buttonX += HORIZONTAL_SPACE;
                }
            }
        }

        static void DrawScreen(int x, int y, int width, int height)
        {
            // Ligne du haut
            DrawHorizontalLine(x, y, width, '─');

            // Ligne du bas
            DrawHorizontalLine(x, y + height - 1, width, '─');

            // Côté gauche
            DrawVerticalLine(x, y, height, '│');

            // Côté droit
            DrawVerticalLine(x + width - 1, y, height, '│');

            // Coins
            Console.SetCursorPosition(x, y);
            Console.Write('┌');
            Console.SetCursorPosition(x + width - 1, y);
            Console.Write('┐');
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write('└');
            Console.SetCursorPosition(x + width - 1, y + height - 1);
            Console.Write('┘');
        }

        static void DrawAtString(int x, int y, string character)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(character);
        }
        static void DrawAtCenterString(int x, int y,int width , string character)
        {
            int centerX = x + (width/2) -(character.Length / 2);
            Console.SetCursorPosition(centerX, y);
            Console.Write(character);
        }

        static void DrawAtChar(int x, int y, char character)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(character);
        }
        static void ClearAt(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }

        static void DrawHorizontalLine(int startX, int y, int length, char character)
        {
            for (int x = startX; x < startX + length; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(character);
            }
        }

        static void DrawVerticalLine(int x, int startY, int length, char character)
        {
            for (int y = startY; y < startY + length; y++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(character);
            }
        }

        static void PasswordVisualSuppression()
        {
            for (int i = 0; i < PasswordData.PASSWORD_LENGTH; i++)
            {
                int passwordNumbersX = (PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2);
                DrawAtChar(passwordNumbersX, PasswordData.PASSWORD_SCREEN_Y + 1, '_');
            }
        }
        static void Presentation()
        {
            Console.Clear();
            Console.WriteLine(@"
                                 ____    _    _   _  ___  _   _ _____ 
                                | __ )  / \  | \ | |/ _ \| | | | ____|
                                |  _ \ / _ \ |  \| | | | | | | |  _|  
                                | |_) / ___ \| |\  | |_| | |_| | |___ 
                                |____/_/   \_\_| \_|\__\_\\___/|_____|
                                                      \_\

                     ____      _    ___ _____ _____ _____ ___ ____  _____ _   _ 
                    |  _ \    / \  |_ _|  ___|  ___| ____|_ _/ ___|| ____| \ | |
                    | |_) |  / _ \  | || |_  | |_  |  _|  | |\___ \|  _| |  \| |
                    |  _ <  / ___ \ | ||  _| |  _| | |___ | | ___) | |___| |\  |
                    |_| \_\/_/   \_\___|_|   |_|   |_____|___|____/|_____|_| \_|
            ");
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 7, PasswordData.PASSWORD_SCREEN_Y - 2);
            Console.WriteLine("Bienvenue à la banque Raiffeisen");
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
                        DrawAtChar((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1, keyChar);  // Afficher la valeur puis
                        DrawNumPad(emptyButton,keyChar);
                        Thread.Sleep(50);
                        DrawAtChar((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1, '*'); // Affichage des numéros entrés
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

        static void MenuChoice(string[] crtBancomatOptions, ref decimal bankMoneyAmount, ref decimal[] withdrawalMoneyLogs, string[] withdrawalMoneyOptions)
        {
            char keyChar;
            bool keyValidity = false;
            bool transactionsFinished = false;
            int timesDone = 0;
            int moneyWithdrawalIndex = 0;
            do
            {
                Presentation();
                DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, crtBancomatOptions.Length + 6);
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
                            Presentation();
                            GetOutMoney(ref bankMoneyAmount, ref withdrawalMoneyLogs, withdrawalMoneyOptions, ref moneyWithdrawalIndex);
                            transactionsFinished = false;
                            break;
                        case '2':
                            Presentation();
                            SeeAmount(bankMoneyAmount);
                            transactionsFinished = false;
                            break;
                        case '3':
                            Presentation();
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
        static void GetOutMoney(ref decimal bankMoneyAmount, ref decimal[] withdrawalMoneyLogs, string[] withdrawalMoneyOptions, ref int moneyWithdrawalIndex)
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
            

            DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth);
            DrawAtString(centerWriting, PasswordData.PASSWORD_SCREEN_Y, showAmount);
            GetOutMoneyAmount(ref bankMoneyAmount, ref withdrawalMoneyLogs, ref moneyWithdrawalIndex);
            showAmount = $"Solde: {bankMoneyAmount:c}";
            Presentation();
            DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth - 8);
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
                                        ClearAt((PasswordData.PASSWORD_SCREEN_X - 19) + question.Length + (i), PasswordData.PASSWORD_SCREEN_Y - 2 + (screenheigth - 2));
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
                            Presentation();
                            DrawScreen(PasswordData.PASSWORD_SCREEN_X - 17, PasswordData.PASSWORD_SCREEN_Y - 1, screenWidth, screenheigth);
                            DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, PasswordData.PASSWORD_SCREEN_Y, screenWidth, $"{showAmount}");
                            for (int i = 0; i < moneyWithdrawalIndex; i++)
                            {
                                if (i == 0)
                                {
                                    DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, (PasswordData.PASSWORD_SCREEN_Y+2 + (i*1)), screenWidth, $"n°{i + 1}: -{withdrawalMoneyLogs[i]:c}");
                                }
                                else
                                {
                                    DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, (PasswordData.PASSWORD_SCREEN_Y+2 + (i*1)), screenWidth, $"n°{i + 1}: -{withdrawalMoneyLogs[i]:c}");
                                }
                            }
                            DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 17, ((PasswordData.PASSWORD_SCREEN_Y - 1) + (screenheigth - 2)), screenWidth, "Appuyer sur 'Q' pour revenir au menu des options");
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

        static void SeeAmount(decimal bankMoneyAmount)
        {
            string showAmount = $"Solde: {bankMoneyAmount:c}";
            int screenheigth = 5;
            int centerWriting = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - (showAmount.Length / 2));
            char keyChar;

            Presentation();
            DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth);
            DrawAtString(centerWriting, PasswordData.PASSWORD_SCREEN_Y, showAmount);
            DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 21, ((PasswordData.PASSWORD_SCREEN_Y + 2)), 62, "Appuyer sur 'Q' pour revenir au menu des options");

            ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
            keyChar = key.KeyChar;
            if (keyChar.ToString().ToUpper() == "Q")
                return;

        }
    }
}