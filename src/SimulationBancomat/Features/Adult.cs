
using static SimulationBancomat.Program;

namespace SimulationBancomat.Features;

public class Adult : Account
{
    public Adult(string ownerType, string ownerName, int moneyAmount, string pwd) 
        : base(ownerType, ownerName, moneyAmount, pwd)
    {
            
    }
    public override void Withdraw(decimal value)
    {
        if (value > 0 && value <= bankMoneyAmount && value <= Factory.MaxAdultWithdrawal)
            bankMoneyAmount = bankMoneyAmount - value;
        else
            MessageBox(IntPtr.Zero, $"Vous ne pouvez pas retirer plus que: {bankMoneyAmount:c}", "Erreur", 16);
    }
}