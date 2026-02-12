using SimulationBancomat.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationBancomat.Program;
using static SimulationBancomat.Display.SuperConsole;

namespace SimulationBancomat.Features
{
    class Verifications
    {
        public char keyChar { get; } = ' ';
        public void Code(SuperConsole console, PasswordData data, string[] emptyButton, char keyChar)
        {
            Console.CursorVisible = true;
            string input = " ";

            do
            {

                console.PasswordVisualSuppression();

                for (int i = 0; i < PasswordData.PASSWORD_LENGTH; i++)
                {
                    Console.SetCursorPosition((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1);

                    ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                    keyChar = key.KeyChar;

                    // Vérifier si c'est un chiffre
                    if (char.IsDigit(keyChar))
                    {
                        input += keyChar;
                        console.DrawAtChar((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1, keyChar);  // Afficher la valeur puis
                        console.DrawNumPad(emptyButton, keyChar);
                        Thread.Sleep(50);
                        console.DrawAtChar((PasswordData.PASSWORD_SCREEN_X + 3) + (i * 2), PasswordData.PASSWORD_SCREEN_Y + 1, '*'); // Affichage des numéros entrés
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
                }
            } while (data.passwordValidity == false);
        }
    }
    class PasswordData
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
}
