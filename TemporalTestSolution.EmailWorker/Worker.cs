namespace TemporalTestSolution.EmailWorker;

using Temporalio.Client;
using Temporalio.Worker;
using TemporalTestSolution.ActivityContracts;
using TemporalTestSolution.WorkerInfrastructure;
using TemporalTestSolution.Workflows;

public class Worker : WorkerBase
{
    private readonly IPushService pushService;

    public Worker(ILogger<Worker> logger, IPushService pushService, Task<TemporalClient> temporalLazyClient) : base(logger, temporalLazyClient, "email-queue")
    {
        this.pushService = pushService;
    }

    protected override string WorkerKnownName => "Email-Worker";

    protected override void ConfigureWorkerOptions(TemporalWorkerOptions options)
    {
        options
            .AddWorkflow<SendPushNotificationWorkflow>()
            .AddActivity(pushService.NotifyEmail);
    }
}
