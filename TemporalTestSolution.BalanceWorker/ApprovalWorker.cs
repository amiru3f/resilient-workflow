using System.Net.Http.Json;
using Temporalio.Activities;
using Temporalio.Client;
using Temporalio.Worker;
using TemporalTestSolution.ActivityContracts;
using TemporalTestSolution.WorkerInfrastructure;
using TemporalTestSolution.Workflows;

namespace TemporalTestSolution.BalanceWorker;


public class ApprovalWorker : WorkerBase
{
    private readonly IApprovalService approvalService;

    public ApprovalWorker(ILogger<ApprovalWorker> logger, Task<TemporalClient> client, IApprovalService approvalService) : base(logger, client, "approval-queue")
    {
        this.approvalService = approvalService;
    }

    protected override string WorkerKnownName => "Approval Worker";

    protected override void ConfigureWorkerOptions(TemporalWorkerOptions options)
    {
        options
            .AddActivity(approvalService.RequestForApprove)
            .AddWorkflow<ApprovalWorflow>();
    }
}

public class ApprovalService : IApprovalService
{
    private readonly IHttpClientFactory httpClientFactory;

    public ApprovalService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    [Activity]
    public async Task<string> RequestForApprove(string flowId)
    {
        using var client = httpClientFactory.CreateClient("discordClient");
        await client.PostAsJsonAsync("api/webhooks/1117090014543753218/BU1eHGWCLdhEzozvf3B2EDtTXClO2CGoN3Nm4atRuR-54PneNfAV10O_a6uDPa6a0frX", new
        {
            Content = "Please approve these request: " + "http://localhost:5208/approve?id=" + flowId
        });

        return "done";
    }
}