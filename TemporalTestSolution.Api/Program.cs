using Temporalio.Client;
using TemporalTestSolution.Workflows;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(ctx => TemporalClient.ConnectAsync(new("localhost:7233")));

var app = builder.Build();

app.MapGet("/approve", async (Task<TemporalClient> lazyTemporalClient, string id) =>
{
    var temporalClient = await lazyTemporalClient;
    var handle = temporalClient.GetWorkflowHandle<ApprovalWorflow>(id);
    await handle.SignalAsync(x => x.ApproveSignal(), new());

    return "approved";
});

app.Run();
