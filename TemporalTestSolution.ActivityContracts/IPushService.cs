namespace TemporalTestSolution.ActivityContracts;

using Temporalio.Activities;

public interface IPushService
{
    [Activity]
    public string NotifyEmail(string email, string content);

    [Activity]
    public string NotifySlack(string email, string content);
}
