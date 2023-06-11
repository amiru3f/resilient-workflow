using TemporalTestSolution.ActivityContracts;
using TemporalTestSolution.BalanceWorker;
using TemporalTestSolution.WorkerInfrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTemporalClient(new()
{
    TargetHost = "localhost:7233",
});

builder.Services.AddHostedService<BalanceWorker>();
builder.Services.AddHostedService<ApprovalWorker>();

builder.Services.AddSingleton<IBalanceService, BalanceService>();
builder.Services.AddSingleton<IApprovalService, ApprovalService>();
builder.Services.AddHttpClient("discordClient", client =>
{
    client.BaseAddress = new("https://discord.com");

});

var host = builder.Build();
host.Run();
