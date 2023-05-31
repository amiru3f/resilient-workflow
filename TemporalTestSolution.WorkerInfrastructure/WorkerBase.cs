using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Temporalio.Client;
using Temporalio.Worker;

namespace TemporalTestSolution.WorkerInfrastructure;

public abstract class WorkerBase : BackgroundService
{
    protected abstract string WorkerKnownName { get; }
    protected readonly ILogger Logger;
    private readonly Task<TemporalClient> lazyClient;
    private readonly string queueName;

    public WorkerBase(ILogger logger, Task<TemporalClient> client, string queueName)
    {
        Logger = logger;
        this.lazyClient = client;
        this.queueName = queueName;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = await lazyClient;
        var temporalWorkerOptions = new TemporalWorkerOptions(queueName)
        {
            Identity = WorkerKnownName + "-" + Guid.NewGuid().ToString().Replace("-", "")
        };

        ConfigureWorkerOptions(temporalWorkerOptions);

        using var worker = new TemporalWorker(client, temporalWorkerOptions);

        try
        {
            await worker.ExecuteAsync(stoppingToken);
        }

        catch (OperationCanceledException)
        {
            Logger.LogError("worker stopped");
        }
    }

    protected abstract void ConfigureWorkerOptions(TemporalWorkerOptions options);
}
