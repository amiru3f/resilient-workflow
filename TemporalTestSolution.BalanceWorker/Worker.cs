namespace TemporalTestSolution.BalanceWorker;

using Temporalio.Client;
using Temporalio.Worker;
using TemporalTestSolution.ActivityContracts;
using TemporalTestSolution.WorkerInfrastructure;
using TemporalTestSolution.Workflows;

public class Worker : WorkerBase
{
    private readonly IBalanceService balanceService;

    public Worker(ILogger<Worker> logger, IBalanceService balanceService, Task<TemporalClient> temporalLazyClient) : base(logger, temporalLazyClient, "balance-queue")
    {
        this.balanceService = balanceService;
    }

    protected override string WorkerKnownName => "Balance-Transfer-Worker";

    protected override void ConfigureWorkerOptions(TemporalWorkerOptions options)
    {
        options
            .AddActivity(balanceService.Deposit)
            .AddActivity(balanceService.Withdraw)
            .AddWorkflow<MoneyTransferWorkflow>()
            .AddWorkflow<SendPushNotificationWorkflow>();
    }
}
