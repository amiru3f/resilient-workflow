using TemporalTestSolution.ActivityContracts;
using TemporalTestSolution.BalanceWorker;
using TemporalTestSolution.WorkerInfrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTemporalClient(new()
{
    TargetHost = "localhost:7233",
});

builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IBalanceService, BalanceWorker>();

var host = builder.Build();
host.Run();
