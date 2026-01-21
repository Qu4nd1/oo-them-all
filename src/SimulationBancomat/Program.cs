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
            Thread.Sleep(5000);
            Console.Clear();

            passwordValidity = VerifyCode(passwordValidity, password);
            
            if (passwordValidity == true)
            {
                MenuChoice(bancomatOptions);
            }
            

            Console.ReadLine();
        }

        static void Presentation()
        {
            string title = "Raiffeisen Banque";
            Console.WriteLine($"\n\t\t{title}\n\n");
            Console.WriteLine("\tBienvenue à la banque Raiffeisen");
            Console.WriteLine("===============================================================");
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