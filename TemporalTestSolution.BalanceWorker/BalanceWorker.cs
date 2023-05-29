using Temporalio.Activities;
using TemporalTestSolution.ActivityContracts;

namespace TemporalTestSolution.BalanceWorker;


public class BalanceWorker : IBalanceService
{
    [Activity]
    public string Deposit(string iban)
    {
        return nameof(Deposit) + " " + iban;
    }


    [Activity]
    public string Withdraw(string iban)
    {
        return nameof(Withdraw) + " " + iban;
    }
}