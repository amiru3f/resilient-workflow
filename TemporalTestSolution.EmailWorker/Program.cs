using TemporalTestSolution.ActivityContracts;
using TemporalTestSolution.EmailWorker;
using TemporalTestSolution.WorkerInfrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTemporalClient(new()
{
    TargetHost = "localhost:7233",
});

EmailConfiguration emailConfiguration = new();
builder.Configuration.Bind("EmailConfiguration", emailConfiguration);
builder.Services.AddSingleton(emailConfiguration);

builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IPushService, PushEmailService>();

var host = builder.Build();
host.Run();
