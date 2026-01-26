using System;
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
                "Consulter ma solde"
            };

            string[] retraitArgentOptions = new string[]
            {
                "Avec reçu",
                "Sans reçu"
            };

            Presentation();

            DrawNumPad(emptyButton, keyChar);

            data.passwordValidity = VerifyCode(ref data, emptyButton, keyChar);

            if (data.passwordValidity == true)
            {
                MenuChoice(bancomatOptions);
            }

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

            for (int i = 0; i < PasswordData.PASSWORD_LENGTH; i++)
            {
                int passwordNumbersX = (PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2);
                DrawAtChar(passwordNumbersX, PasswordData.PASSWORD_SCREEN_Y + 1, '_');
            }

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
                    buttonX += HORIZONTAL_SPACE;
                    Console.ResetColor();
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

        static void DrawAtChar(int x, int y, char character)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(character);
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

        static void Presentation()
        {
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
            int timeToWait = 10;
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
                        DrawAtChar((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1, '*');
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

        static void MenuChoice(string[] crtBancomatOptions)
        {
            Console.Clear();

            Console.WriteLine("Veuillez choisir l'action désirer !\n");
            for (int i = 0; i < crtBancomatOptions.Length; i++)
            {
                Console.WriteLine($"\t{i+1}. {crtBancomatOptions[i]}");
            }
        }
        
        static void Menu()
        {

        }
        static void GetOutMoney()
        {

        }

        static void SeeAmount()
        {

        }

        static void PrintReceipt()
        {

        }
    }
}