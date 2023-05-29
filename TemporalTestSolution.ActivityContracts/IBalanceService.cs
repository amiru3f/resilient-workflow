namespace TemporalTestSolution.ActivityContracts;

using Temporalio.Activities;

public interface IBalanceService
{

    [Activity]
    public string Withdraw(string iban);

    [Activity]
    public string Deposit(string iban);
}


