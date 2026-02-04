using SimulationBancomat.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features
{
    static class Verifications
    {
        public static char keyChar = ' ';
        static public bool Code(ref PasswordData data, string[] emptyButton, char keyChar)
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
                        SuperConsole.DrawNumPad(emptyButton, keyChar);
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
    }
}
