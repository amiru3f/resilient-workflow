using Temporalio.Client;
using TemporalTestSolution.Workflows;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(ctx => TemporalClient.ConnectAsync(new("localhost:7233")));

var app = builder.Build();

app.MapGet("/transfer", async (Task<TemporalClient> lazyTemporalClient, string sourceIban, string destinationIban) =>
{
    var temporalClient = await lazyTemporalClient;

    var result = await temporalClient.StartWorkflowAsync(
        (MoneyTransferWorkflow wf) => wf.RunAsync(sourceIban, destinationIban),
        new(id: Guid.NewGuid().ToString(), taskQueue: "work-queue"));

    return await result.GetResultAsync();

});

app.Run();
