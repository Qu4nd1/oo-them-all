using System;
using System.Text;

namespace SimulationBancomat
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            bool passwordValidity = false;
            int password = 123456;
            string[] emptyButton = new string[]
                {
                "┌───┐", 
                "│   │",
                "└───┘"
                };

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

            DrawNumPad(emptyButton);

            //passwordValidity = VerifyCode(passwordValidity, password);
            
            if (passwordValidity == true)
            {
                MenuChoice(bancomatOptions);
            }
            

            Console.ReadLine();
        }

        static void DrawNumPad(string[] emptyButton)
        {
            
            int buttonX = 19;
            int buttonY = 11;
            const int SCREEN_X = 15;
            const int SCREEN_Y = 6;
            const int PASSWORD_SCREEN_X = 20;
            const int PASSWORD_SCREEN_Y = 7;
            const int PASSWORD_LENGTH = 6;
            const int HORIZONTAL_SPACE = 7;
            const int VERTICAL_SPACE = 4;

            Console.CursorVisible = false;

            DrawScreen(SCREEN_X, SCREEN_Y, 27,21);
            DrawScreen(PASSWORD_SCREEN_X, PASSWORD_SCREEN_Y, 17,3);

            for (int i = 0; i < PASSWORD_LENGTH; i++)
            {
                int passwordNumbersX = (PASSWORD_SCREEN_X + 3) + (i * 2);
                DrawAtChar(passwordNumbersX, PASSWORD_SCREEN_Y + 1, '_');
            }
            
            for (int i = 0; i < 10; i++)
            {
                char padNumber = (char)('0' + ((i + 1) % 10));

                if (i == 3 || i == 6 || i == 9)
                {
                    buttonX = 19;
                    buttonY += VERTICAL_SPACE;
                    if (i == 9)
                        buttonX += HORIZONTAL_SPACE;
                }
                   

                if (i < 3)
                {
                    for (int j = 0; j < 3; j++)
                        DrawAtString(buttonX, buttonY + j, emptyButton[j]);
                    DrawAtChar(buttonX + 2, buttonY + 1, padNumber);
                    buttonX += HORIZONTAL_SPACE;
                }
                else if (i < 6)
                {
                    for (int j = 0; j < 3; j++)
                        DrawAtString(buttonX, buttonY + j, emptyButton[j]);
                    DrawAtChar(buttonX + 2, buttonY + 1, padNumber);
                    buttonX += HORIZONTAL_SPACE;
                }
                else if (i < 9)
                {
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
            string title = "Raiffeisen Banque";
            Console.WriteLine($"\n\t\t{title}\n\n");
            Console.WriteLine("\tBienvenue à la banque Raiffeisen");
        }

        static bool VerifyCode(bool crtPasswordValidity, int crtPassword)
        {
            Console.CursorVisible = true;
            int tryCounter = 0;

            const int QUESTION_POS_X = 3;
            const int QUESTION_POS_Y = 5;

            int timeToWait = 10;
            
            do
            { 
                Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y);
                Console.Write("Veuillez saisir votre PIN (6 chiffres) : ");

                crtPasswordValidity = int.TryParse(Console.ReadLine(), out crtPassword);
                if (crtPasswordValidity == false)
                {
                    Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("PIN INCORRECT");
                    
                    tryCounter++;

                    Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y + 2);
                    Console.WriteLine($"Il vous reste {3 - tryCounter} chance avant un blockage temporaire des essais");
                    Console.ResetColor();

                    Thread.Sleep(2000);
                    Console.Clear();

                    if (tryCounter >= 3)
                    {
                        Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y + 1);
                        Console.WriteLine("Veuillez patienter avant de réessayer");
                        for (int i = 0; i < timeToWait; i++)
                        {
                            Console.CursorVisible = false;
                            Thread.Sleep(1000);
                            Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y + 2);
                            Console.WriteLine($"Temps restant a attendre {timeToWait - i} ");
                        }
                        tryCounter = 0;
                        timeToWait = timeToWait * 2;
                        Console.Clear();
                    }
                    
                }
                if (crtPasswordValidity == true)
                {
                    if (crtPassword != 123456)
                    {
                        Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y + 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("PIN INCORRECT");

                        crtPasswordValidity = false;
                        tryCounter++;

                        Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y + 2);
                        Console.WriteLine($"Il vous reste {3 - tryCounter} chance avant un blockage temporaire des essais");
                        Console.ResetColor();
                        Thread.Sleep(2000);
                        Console.Clear();

                        if (tryCounter >= 3)
                        {
                            Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y + 1);
                            Console.WriteLine("Veuillez patienter avant de réessayer");
                            for (int i = 0; i < timeToWait; i++)
                            {
                                Console.CursorVisible = false;
                                Thread.Sleep(1000);
                                Console.SetCursorPosition(QUESTION_POS_X, QUESTION_POS_Y + 2);
                                Console.WriteLine($"Temps restant a attendre {timeToWait - i} ");
                            }
                            tryCounter = 0;
                            timeToWait = timeToWait * 2;
                            Console.Clear();
                        }
                    }
                }
            } while (crtPasswordValidity == false);
            return crtPasswordValidity;
        }

        static void MenuChoice(string[] crtBancomatOptions)
        {
            Console.Clear();
            Presentation();

            Console.WriteLine("Veuillez choisir l'action désirer !\n");
            for (int i = 0; i < crtBancomatOptions.Length; i++)
            {
                Console.WriteLine($"\t{i+1}. {crtBancomatOptions[i]}");
            }
        }

        static void Menu()
        {

        }

        static void VerifyCode()
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