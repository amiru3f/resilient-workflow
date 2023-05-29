using Microsoft.Extensions.Logging;
using Temporalio.Workflows;
using TemporalTestSolution.ActivityContracts;

namespace TemporalTestSolution.Workflows;

[Workflow]
public class MoneyTransferWorkflow
{
    [WorkflowRun]
    public async Task<string> RunAsync(string sourceIban, string destinationIban)
    {
        string withdrawResponse = await Workflow.ExecuteActivityAsync(

            (IBalanceService balance) => balance.Withdraw(sourceIban),
            new() { ScheduleToCloseTimeout = TimeSpan.FromMinutes(5) });


        Workflow.Logger.LogInformation("withdraw done successfuly with response: {response}", withdrawResponse);

        string depositResponse = await Workflow.ExecuteActivityAsync(

            (IBalanceService balance) => balance.Deposit(destinationIban),
            new()
            {
                ScheduleToCloseTimeout = TimeSpan.FromMinutes(5)
            });

        Workflow.Logger.LogInformation("going to send email");

        await Workflow.StartChildWorkflowAsync<SendPushNotificationWorkflow>(x => x.RunAsync("solhi.amir1371@gmail.com", "done"),
            new ChildWorkflowOptions()
            {
                ParentClosePolicy = ParentClosePolicy.Abandon
            }
        );

        return withdrawResponse + " " + depositResponse;
    }
}