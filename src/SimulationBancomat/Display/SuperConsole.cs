using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationBancomat;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Display
{
    static class SuperConsole
    {
        public static int initialButtonX = PasswordData.PASSWORD_SCREEN_X - 1;
        public static int initalButtonY = PasswordData.PASSWORD_SCREEN_Y + 3;
        public static int SCREEN_X = PasswordData.PASSWORD_SCREEN_X - 5;
        public static int SCREEN_Y = PasswordData.PASSWORD_SCREEN_Y - 1;
        public const int HORIZONTAL_SPACE = 7;
        public const int VERTICAL_SPACE = 4;
        public static string[] emptyButton = new string[]
            {
                "┌───┐",
                "│   │",
                "└───┘"
            };


        static public void DrawNumPad(string[] emptyButton, char keyChar)
        {
            int buttonX = initialButtonX;
            int buttonY = initalButtonY;

            Console.CursorVisible = false;

            DrawScreen(SCREEN_X, SCREEN_Y, 27, 21);
            DrawScreen(PasswordData.PASSWORD_SCREEN_X, PasswordData.PASSWORD_SCREEN_Y, 17, 3);

            for (int i = 0; i< 10; i++)
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
                    for (int j = 0; j< 3; j++)
                    {
                        DrawAtString(buttonX, buttonY + j, emptyButton[j]);
                    } 
                    DrawAtChar(buttonX + 2, buttonY + 1, padNumber);
                    Console.ResetColor();
                    Thread.Sleep(50);
                    for (int j = 0; j< 3; j++)
                    {
                        DrawAtString(buttonX, buttonY + j, emptyButton[j]);
                    }
                    DrawAtChar(buttonX + 2, buttonY + 1, padNumber);
                    buttonX += HORIZONTAL_SPACE;
                }
                else
                {
                    for (int j = 0; j < 3; j++)
                    {
                        DrawAtString(buttonX, buttonY + j, emptyButton[j]);
                    }
                    DrawAtChar(buttonX + 2, buttonY + 1, padNumber);
                    buttonX += HORIZONTAL_SPACE;
                }
            }
        }
        static public void DrawScreen(int x, int y, int width, int height)
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
        static public void DrawAtString(int x, int y, string character)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(character);
        }
        static public void DrawAtCenterString(int x, int y, int width, string character)
        {
            int centerX = x + (width / 2) - (character.Length / 2);
            Console.SetCursorPosition(centerX, y);
            Console.Write(character);
        }
        static public void DrawAtChar(int x, int y, char character)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(character);
        }
        static public void ClearAt(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
        static public void DrawHorizontalLine(int startX, int y, int length, char character)
        {
            for (int x = startX; x < startX + length; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(character);
            }
        }
        static public void DrawVerticalLine(int x, int startY, int length, char character)
        {
            for (int y = startY; y < startY + length; y++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(character);
            }
        }
        static public void PasswordVisualSuppression()
        {
            for (int i = 0; i < PasswordData.PASSWORD_LENGTH; i++)
            {
                int passwordNumbersX = (PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2);
                DrawAtChar(passwordNumbersX, PasswordData.PASSWORD_SCREEN_Y + 1, '_');
            }
        }
        static public void Presentation()
        {
            Console.Clear();
            Console.WriteLine(@"
                               ____    _    _   _  ___  _   _ _____ 
                              | __ )  / \  | \ | |/ _ \| | | | ____|
                              |  _ \ / _ \ |  \| | | | | | | |  _|  
                              | |_) / ___ \| |\  | |_| | |_| | |___ 
                              |____/_/   \_\_| \_|\__\_\\___/|_____|
                                                      
                     ____     _   ___ _____ _____ _____ ___ ____  _____ _   _ 
                    |  _ \   / \ |_ _|  ___|  ___| ____|_ _/ ___|| ____| \ | |
                    | |_) | / _ \ | || |_  | |_  |  _|  | |\___ \|  _| |  \| |
                    |  _ < / ___ \| ||  _| |  _| | |___ | | ___) | |___| |\  |
                    |_| \_/_/   \_\__|_|   |_|   |_____|___|____/|_____|_| \_|
            ");
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 7, PasswordData.PASSWORD_SCREEN_Y - 2);
            Console.WriteLine("Bienvenue à la banque Raiffeisen");
        }
        static public void ClearAtForLength(int x, int y, int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.Write(' ');
            }
            
        }
    }
}
