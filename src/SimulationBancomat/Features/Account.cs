//******************************************************************************************
// ETML
// Auteur : Kentin Fankhauser
// Date : 23/02/2026
// Description : Visualisation du compte client et modification si nécessaire
//******************************************************************************************
using SimulationBancomat.Display;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features
{
    class Account
    {
        private decimal _bankMoneyAmount;
        public decimal bankMoneyAmount
        {
            get
            {
                return _bankMoneyAmount;
            }
        }

        public void Deposit(decimal value)
        {
            _bankMoneyAmount = _bankMoneyAmount + value;
        }

        public void Withdraw(decimal value)
        {
            if (value > 0 || value < _bankMoneyAmount)
                _bankMoneyAmount = _bankMoneyAmount - value;
            else
                MessageBox(IntPtr.Zero, $"Vous ne pouvez pas retirer plus que: {_bankMoneyAmount:c}", "Erreur", 16);
        }

        
        public string AmountFormatted
        {
            get
            {
                return $"Solde: {_bankMoneyAmount:c}";
            }
        }
        public Account(int moneyAmount)
        {
            _bankMoneyAmount = moneyAmount;
        }
        public void Show()
        {
            
            int screenheigth = 5;
            int centerWriting = (PasswordData.PASSWORD_SCREEN_X - 21) + ((62 / 2) - (AmountFormatted.Length / 2));
            char keyChar;

            SuperConsole console = new SuperConsole();

            console.Presentation();
            console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, screenheigth);
            console.DrawAtString(centerWriting, PasswordData.PASSWORD_SCREEN_Y, AmountFormatted);
            console.DrawAtCenterString(PasswordData.PASSWORD_SCREEN_X - 21, ((PasswordData.PASSWORD_SCREEN_Y + 2)), 62, "Appuyer sur 'Q' pour revenir au menu des options");

            ConsoleKeyInfo key = Console.ReadKey(true);  // true = ne pas afficher la touche
            keyChar = key.KeyChar;
            if (keyChar.ToString().ToUpper() == "Q")
                return;

        }
    }
}
