//******************************************************************************************
// ETML
// Auteur : Kentin Fankhauser
// Date : 23/02/2026
// Description : Regroupement des différents tests nécessaires au programme
//******************************************************************************************
using SimulationBancomat.Display;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features
{
    class Verifications
    {
        public char keyChar { get; set; } = ' ';
        public void Code(SuperConsole console, PasswordData data, string[] emptyButton)
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
                
            } while (!data.PasswordMatches(input));
        }
    }
    class PasswordData
    {
        private int _password;
        
        public const int PASSWORD_SCREEN_X = 40;
        public const int PASSWORD_SCREEN_Y = 16;
        public const int PASSWORD_LENGTH = 6;

        // Constructeur
        public PasswordData(int pwd)
        {
            _password = pwd;
        }

        public bool PasswordMatches(string passwordCandidate)
        {
            if (int.TryParse(passwordCandidate, out int passwordCandidateInt))
            {
                return passwordCandidateInt==_password;
            }

            return false;
        }
    }
}