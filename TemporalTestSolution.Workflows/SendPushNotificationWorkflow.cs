
using Temporalio.Workflows;
using TemporalTestSolution.ActivityContracts;

namespace TemporalTestSolution.Workflows;

[Workflow]
public class SendPushNotificationWorkflow
{
    [WorkflowRun]
    public Task<string> RunAsync(string email, string content) =>

        Workflow.ExecuteActivityAsync(
           (IPushService pusher) => pusher.NotifyEmail(email, content),
           new()
           {
               ScheduleToCloseTimeout = TimeSpan.FromMinutes(20)
           });
}