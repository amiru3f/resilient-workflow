namespace TemporalTestSolution.ActivityContracts;

using Temporalio.Activities;

public interface IApprovalService
{

    [Activity]
    public Task<string> RequestForApprove(string flowId);

}

