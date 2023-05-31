
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

        await Workflow.ExecuteActivityAsync(
               (IPushService pusher) => pusher.NotifySlack(email, content),
               new()
               {
                   ScheduleToCloseTimeout = TimeSpan.FromMinutes(20),
                   TaskQueue = "slack-queue"
               });


        return "Push Notification Workflow";
    }


}