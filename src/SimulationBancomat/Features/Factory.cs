using SimulationBancomat.Display;

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
    public static string[] AccountInformation = new string[10];
    public static bool IsNotValid
    {
        get
        {
            if (AccountInformation[0].ToUpper() != "CHILD")
                return true;
            else if (AccountInformation[0].ToUpper() != "TEENAGER")
                return true;
            else if (AccountInformation[0].ToUpper() != "ADULT")
                return true;
            else
                return false;
        }
    }

    public static Account CreateAccount(SuperConsole console)
    {
        int startAmount;
        Console.CursorVisible = false;

        do
        {
            console.Presentation();
            Console.WriteLine("\nWhat type of account do you want to create ?");
            Console.WriteLine($"\t-Child: Max withdrawal being {_maxChildWithdrawal}");
            Console.WriteLine($"\t-Teenager: Max withdrawal being {_maxTeenagerWithdrawal}");
            Console.WriteLine($"\t-Adult: Max withdrawal being {_maxAdultWithdrawal}");
            Console.Write("Choice: ");
            AccountInformation[0] = Console.ReadLine();
            Console.WriteLine("What is the name of the owner of this account ?)");
            Console.Write("Firstname Lastname: ");
            AccountInformation[1] = Console.ReadLine();
            Console.Write("What amount would you like to start with on this account: ");
            Console.Write("Amount: ");
            startAmount = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
        } while (!IsNotValid);
        Account account = new Account(AccountInformation[0], AccountInformation[1], startAmount);
        return account;
    }
}