using SimulationBancomat.Display;
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features;

public static class Factory
{
    private static int _maxChildWithdrawal = 2000;

    public static int MaxChildWithdrawal
    {
        get { return _maxChildWithdrawal; }
    }
    private static int _maxTeenagerWithdrawal = 5000;
    public static int MaxTeenagerWithdrawal
    {
        get { return _maxTeenagerWithdrawal; }
    }
    private static int _maxAdultWithdrawal = 50000;
    public static int MaxAdultWithdrawal
    {
        get { return _maxAdultWithdrawal; }
    }
    public static string ChildValueCheck = "CHILD";
    public static string TeenagerValueCheck = "TEENAGER";
    public static string AdultValueCheck = "ADULT";
    public static string AdminValueCheck = "ADMIN";
    public static string[] AccountInformation = new string[10];
    public static bool IsValid
    {
        get
        {
            if (ChildValueCheck == AccountInformation[0].ToUpper())
                return false;
            else if (TeenagerValueCheck == AccountInformation[0].ToUpper())
                return false;
            else if (AdultValueCheck == AccountInformation[0].ToUpper())
                return false;
            else if (AdminValueCheck == AccountInformation[0].ToUpper())
                return false;
            else
                MessageBox(IntPtr.Zero, $"Votre choix: {AccountInformation[0]}, ne correspond pas aux choix possible !", "Type de compte invalide", 16);
                return true;
        }
    }
    

    public static Account CreateAccount(SuperConsole console, Verifications verification)
    {
        string stringStartAmount = "";
        int intStarAmount = 0;
        Console.CursorVisible = true;
        do
        {
            // Layout
            console.Presentation();
            console.DrawScreen(PasswordData.PASSWORD_SCREEN_X - 21, PasswordData.PASSWORD_SCREEN_Y - 1, 62, 20);

            // Account Type
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 18, PasswordData.PASSWORD_SCREEN_Y);
            Console.WriteLine("What type of account do you want to create ?");
            // Child
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 15, PasswordData.PASSWORD_SCREEN_Y + 2);
            Console.WriteLine("-Child: Max withdrawal: ");
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X + 15, PasswordData.PASSWORD_SCREEN_Y + 2);
            Console.WriteLine($"{_maxChildWithdrawal:c}");
            // Teenager
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 15, PasswordData.PASSWORD_SCREEN_Y + 3);
            Console.WriteLine("-Teenager: Max withdrawal: ");
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X + 15, PasswordData.PASSWORD_SCREEN_Y + 3);
            Console.WriteLine($"{_maxTeenagerWithdrawal:c}");
            // Adult
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 15, PasswordData.PASSWORD_SCREEN_Y + 4);
            Console.WriteLine("-Adult: Max withdrawal: ");
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X + 14, PasswordData.PASSWORD_SCREEN_Y + 4);
            Console.WriteLine($"{_maxAdultWithdrawal:c}");
            // Choice
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 18, PasswordData.PASSWORD_SCREEN_Y + 6);
            Console.Write("Choice: ");
            AccountInformation[0] = Console.ReadLine();
        // Tant que Type non valid BOUCLE
        } while (IsValid);

        // Si type pas admin
        if (AdminValueCheck != AccountInformation[0].ToUpper())
        {
            // Account Owner
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 18, PasswordData.PASSWORD_SCREEN_Y + 8);
            Console.WriteLine("What is the name of the owner of this account ?)");
            Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 18, PasswordData.PASSWORD_SCREEN_Y + 9);
            Console.Write("Firstname Lastname: ");
            AccountInformation[1] = Console.ReadLine();
            // Account start amount
            do
            {
                console.ClearAtForLength(PasswordData.PASSWORD_SCREEN_X - 10, PasswordData.PASSWORD_SCREEN_Y + 12, 15);
                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 18, PasswordData.PASSWORD_SCREEN_Y + 11);
                Console.Write("What amount would you like to start with on this account ?");
                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 18, PasswordData.PASSWORD_SCREEN_Y + 12);
                Console.Write("Amount: ");
                stringStartAmount = Console.ReadLine();
            } while (!verification.ValueIsInt(stringStartAmount));
            intStarAmount = Convert.ToInt32(stringStartAmount);
            
            do
            {
                console.ClearAtForLength(PasswordData.PASSWORD_SCREEN_X + 2, PasswordData.PASSWORD_SCREEN_Y + 15, 10);
                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 18, PasswordData.PASSWORD_SCREEN_Y + 14);
                Console.WriteLine("Quelle mot de passe choississez vous pour votre compte ?");
                Console.SetCursorPosition(PasswordData.PASSWORD_SCREEN_X - 18, PasswordData.PASSWORD_SCREEN_Y + 15);
                Console.Write("Votre mot de passe: ");
                AccountInformation[2] = Console.ReadLine();
            } while (!verification.PasswordIsValid(AccountInformation[2]));
        }
        Console.Clear();
        

        Account account;

        switch (AccountInformation[0].ToUpper())
        {
            case "CHILD":
                account = new Child(AccountInformation[0], AccountInformation[1], intStarAmount, AccountInformation[2]);
                break;
            case "TEENAGER":
                account = new Teenager(AccountInformation[0], AccountInformation[1], intStarAmount, AccountInformation[2]);
                break;
            case "ADULT":
                account = new Adult(AccountInformation[0], AccountInformation[1], intStarAmount, AccountInformation[2]);
                break;
            case "ADMIN":
                account = new Admin("Admin", "Sudo Root", 100000000, "404404");
                break;
            default:
                account = new Account(AccountInformation[0], AccountInformation[1], intStarAmount, AccountInformation[2]);
                break;
        }

        return account;
    }
}