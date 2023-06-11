namespace TemporalTestSolution.BalanceWorker;

using Temporalio.Activities;
using Temporalio.Client;
using Temporalio.Worker;
using TemporalTestSolution.ActivityContracts;
using TemporalTestSolution.WorkerInfrastructure;
using TemporalTestSolution.Workflows;

public class BalanceWorker : WorkerBase
{
    private readonly IBalanceService balanceService;

    public BalanceWorker(ILogger<BalanceWorker> logger, IBalanceService balanceService, Task<TemporalClient> temporalLazyClient) : base(logger, temporalLazyClient, "balance-queue")
    {
        this.balanceService = balanceService;
    }

    protected override string WorkerKnownName => "Balance-Transfer-Worker";

    protected override void ConfigureWorkerOptions(TemporalWorkerOptions options)
    {
        options
            .AddActivity(balanceService.Deposit)
            .AddActivity(balanceService.Withdraw)
            .AddWorkflow<InfrastructureProvisionWorkFlow>()
            .AddWorkflow<SendPushNotificationWorkflow>();
    }
}

public class BalanceService : IBalanceService
{
    [Activity]
    public string Deposit(string iban)
    {
        return "Done";
    }

    [Activity]
    public string Withdraw(string iban)
    {
        return "Done";
    }
}
