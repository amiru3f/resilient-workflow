using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Temporalio.Client;
using Temporalio.Worker;

namespace TemporalTestSolution.WorkerInfrastructure;

public abstract class WorkerBase : BackgroundService
{
    protected readonly ILogger Logger;
    private readonly Task<TemporalClient> lazyClient;

    public WorkerBase(ILogger logger, Task<TemporalClient> client)
    {
        Logger = logger;
        this.lazyClient = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = await lazyClient;
        var temporalWorkerOptions = new TemporalWorkerOptions("work-queue");

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
