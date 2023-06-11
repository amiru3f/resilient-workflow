using Microsoft.Extensions.Logging;
using Temporalio.Workflows;
using TemporalTestSolution.ActivityContracts;

namespace TemporalTestSolution.Workflows;

[Workflow]
public class InfrastructureProvisionWorkFlow
{
    [WorkflowRun]
#pragma warning disable CA1822
    public async Task<string> RunAsync(string iban, string bucketName, string email)
    {
#pragma warning restore CA1822 

        string withdrawResponse = await Workflow.ExecuteActivityAsync(

            (IBalanceService balance) => balance.Withdraw(iban),
            new() { ScheduleToCloseTimeout = TimeSpan.FromHours(1) });


        Workflow.Logger.LogInformation("withdraw done successfuly with response: {response}", withdrawResponse);

        string depositResponse = await Workflow.ExecuteActivityAsync(

            (IBalanceService balance) => balance.Deposit("DestinationIban"),
            new()
            {
                ScheduleToCloseTimeout = TimeSpan.FromHours(1)
            });

        Workflow.Logger.LogInformation("deposit done successfuly with response: {response}", depositResponse);

        await Workflow.ExecuteChildWorkflowAsync<ApprovalWorflow>(x => x.RunAsync(),
            new ChildWorkflowOptions()
            {
                ParentClosePolicy = ParentClosePolicy.Terminate,
                TaskQueue = "approval-queue"
            }
        );


        await Workflow.ExecuteActivityAsync(

            (IBucketCreatorService bucketCreatorService) => bucketCreatorService.CreateBucket(bucketName),
            new()
            {
                ScheduleToCloseTimeout = TimeSpan.FromHours(1),
                TaskQueue = "infra-queue"
            });


        await Workflow.ExecuteChildWorkflowAsync<SendPushNotificationWorkflow>(x => x.RunAsync(email, "provision done"),
            new ChildWorkflowOptions()
            {
                ParentClosePolicy = ParentClosePolicy.Abandon
            }
        );

        return "done";
    }
}