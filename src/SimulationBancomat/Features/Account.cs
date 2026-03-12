//******************************************************************************************
// ETML
// Auteur : Kentin Fankhauser
// Date : 23/02/2026
// Description : Visualisation du compte client et modification si nécessaire
//******************************************************************************************
using SimulationBancomat.Display;
using System.Text.RegularExpressions;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features
{
    public class Account
    {
        public int passwordCheckCounter = 0;
        public Account(string ownerType, string ownerName, int moneyAmount, string pwd)
        {
            _ownerType = ownerType;
            _ownerName = ownerName;
            bankMoneyAmount = moneyAmount;
            _password = pwd;
        }

        private string _password;
        public string Password { get { return _password; } }
        protected decimal bankMoneyAmount;
        public decimal BankMoneyAmount
        {
            get
            {
                return bankMoneyAmount;
            }
        }

        public string OwnerType
        {
            get {return _ownerType;}
            set {
                if (value.ToUpper() == "CHILD")
                    _ownerType = value.ToUpper();
                else if (value.ToUpper() == "TEENAGER")
                    _ownerType = value.ToUpper();
                else if (value.ToUpper() == "ADULT")
                    _ownerType = value.ToUpper();
                else if (value.ToUpper() == "ADMIN")
                    _ownerType = value.ToUpper();
                else
                {
                    MessageBox(IntPtr.Zero, $"Votre choix de compte ne correspond pas aux choix possible !", "Erreur", 16);
                    _ownerType = "INVALID";
                }
            }
        }
        private string _ownerType;

        public string OwnerName { get { return _ownerName;} }
        private string _ownerName;

        public void Deposit(decimal value)
        {
            bankMoneyAmount = bankMoneyAmount + value;
        }

        public virtual void Withdraw(decimal value)
        {
            if (value > 0 || value < bankMoneyAmount)
                bankMoneyAmount = bankMoneyAmount - value;
            else
                MessageBox(IntPtr.Zero, $"Vous ne pouvez pas retirer plus que: {bankMoneyAmount:c}", "Erreur", 16);
        }

        
        public string AmountFormatted
        {
            get
            {
                return $"Solde: {bankMoneyAmount:c}";
            }
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
        public bool PasswordMatches(string passwordCandidate)
        {
            passwordCheckCounter++;
            if (Regex.IsMatch(passwordCandidate,@"[0-9]{6}"))
            {
                return passwordCandidate == _password;
            }

            return false;
        }
    }
}
