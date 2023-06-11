
using Temporalio.Client;
using Temporalio.Workflows;
using TemporalTestSolution.ActivityContracts;

namespace TemporalTestSolution.Workflows;

[Workflow]
public class SendPushNotificationWorkflow
{
    [WorkflowRun]
    public async Task<string> RunAsync(string email, string content)
    {
        await Workflow.ExecuteActivityAsync(
                   (IPushService pusher) => pusher.NotifyEmail(email, content),
                   new()
                   {
                       ScheduleToCloseTimeout = TimeSpan.FromMinutes(20),
                       TaskQueue = "email-queue"
                   });

        return "succesfuly sent";
    }
}