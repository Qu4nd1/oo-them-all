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
    public class Verifications
    {
        public char keyChar { get; set; } = ' ';
        public void InputPasswordIsValid(SuperConsole console, Account account, string[] emptyButton)
        {
            Console.CursorVisible = true;
            string input;

            do
            {
                input = "";
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
            } while (!account.PasswordMatches(input));
        }
        public bool PasswordIsValid(string password)
        {
            int digit = 0;
            string digitToCheck;
            for (int i = 0; i < password.Length; i++)
            {
                digitToCheck = password[i].ToString();
                if (int.TryParse(digitToCheck, out digit))
                {

                }
                else
                {
                    MessageBox(IntPtr.Zero, $"Votre mot de passe ne contient pas uniquement des numéros, Merci de le corriger !", "Input invalid", 16);
                    return false;
                }
            }
            if (password.Length == 6)
            {
                return true;
            }
            else
            {
                MessageBox(IntPtr.Zero, $"Votre mot de passe ne convient à la longueur attendue qui doit être de 6, Merci de le corriger !", "Input invalid", 16);
                return false;
            }
        }
        public bool ValueIsInt(string password)
        {
            int digit = 0;
            string digitToCheck;
            for (int i = 0; i < password.Length; i++)
            {
                digitToCheck = password[i].ToString();
                if (int.TryParse(digitToCheck, out digit))
                {

                }
                else
                {
                    MessageBox(IntPtr.Zero, $"Votre mot de passe ne contient pas uniquement des numéros, Merci de le corriger !", "Input invalid", 16);
                    return false;
                }
            }
            return true;
        }
    }
    class PasswordData
    {
        
        public const int PASSWORD_SCREEN_X = 40;
        public const int PASSWORD_SCREEN_Y = 16;
        public const int PASSWORD_LENGTH = 6;


        
    }
}