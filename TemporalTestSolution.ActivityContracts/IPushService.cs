namespace TemporalTestSolution.ActivityContracts;

using Temporalio.Activities;

public interface IPushService
{
    [Activity]
    public string NotifyEmail(string email, string content);

    [Activity]
    public string NotifyDiscord(string email, string content);
}
