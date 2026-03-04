//******************************************************************************************
// ETML
// Auteur : Kentin Fankhauser
// Date : 23/02/2026
// Description : Menu de choix avec redirection vers les différentes options
//******************************************************************************************
using SimulationBancomat.Features;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Display
{
    class Menu
    {
        public Account AtmOpenning(SuperConsole console)
        {
           char keyChar;
            bool keyValidity;
            bool transactionsFinished = false;
            string[] bancomatOptions = new string[]
            {
                "Créer un compte (compte non-eistant)",
                "Se connecter (compte existant)"
            };

            do
            {
                console.Presentation();
                console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, bancomatOptions.Length + 8);
                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
                Console.WriteLine("Veuillez choisir l'action désirer !\n");
                for (int i = 0; i < bancomatOptions.Length; i++)
                {
                    Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 20, PasswordData.PASSWORD_SCREEN_Y + (i + 1) * 2);
                    Console.WriteLine($"\t{i + 1}. {bancomatOptions[i]}");
                }

                ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                keyChar = key.KeyChar;

                if (char.IsDigit(keyChar) && (keyChar == '1' || keyChar == '2'))
                {
                    keyValidity = true;

                    switch (keyChar)
                    {
                        case '1':
                            Account accountNew = Factory.CreateAccount(console);
                            return accountNew;
                        case '2':
                            Account account = new Account("ADULT","root, root", 100000);
                            return account;
                    }
                }
                else
                {
                    keyValidity = false;
                    MessageBox(IntPtr.Zero, "La valeur attendue est un entier entre 1 et 2", "Erreur", 16);
                }
                Console.Clear();
            } while (keyValidity != true);
            return null;
        }

        public void Choice(SuperConsole console, Transactions transaction, Account account)
        {
            char keyChar;
            bool keyValidity;
            bool transactionsFinished = false;
            string[] bancomatOptions = new string[]
            {
                "Deposer de l'argent",
                "Retirer de l'argent",
                "Consulter mon solde",
                "Quitter"
            };

            do
            {
                console.Presentation();
                console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, bancomatOptions.Length + 8);
                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 19, PasswordData.PASSWORD_SCREEN_Y);
                Console.WriteLine("Veuillez choisir l'action désirer !\n");
                for (int i = 0; i < bancomatOptions.Length; i++)
                {
                    Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 20, PasswordData.PASSWORD_SCREEN_Y + (i + 1) * 2);
                    Console.WriteLine($"\t{i + 1}. {bancomatOptions[i]}");
                }

                ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
                keyChar = key.KeyChar;

                if (char.IsDigit(keyChar))
                {
                    keyValidity = true;

                    switch (keyChar)
                    {
                        case '1':
                            console.Presentation();
                            transaction.MoneyMovement(account, transaction, true);
                            transactionsFinished = false;
                            break;
                        case '2':
                            console.Presentation();
                            transaction.MoneyMovement(account, transaction, false);
                            transactionsFinished = false;
                            break;
                        case '3':
                            console.Presentation();
                            account.Show();
                            transactionsFinished = false;
                            break;
                        case '4':
                            console.Presentation();
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
                Console.Clear();
            } while (keyValidity != true || transactionsFinished != true);
        }
    }
}
