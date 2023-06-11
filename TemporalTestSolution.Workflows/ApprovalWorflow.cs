using Temporalio.Workflows;
using TemporalTestSolution.ActivityContracts;

namespace TemporalTestSolution.Workflows;

[Workflow]
public sealed class ApprovalWorflow
{
    private bool approved = false;

    [WorkflowRun]
    public async Task<bool> RunAsync()
    {
        string withdrawResponse = await Workflow.ExecuteActivityAsync(

            (IApprovalService approver) => approver.RequestForApprove(Temporalio.Workflows.Workflow.Info.WorkflowID),
            new() { ScheduleToCloseTimeout = TimeSpan.FromHours(1), TaskQueue = "approval-queue" });

        await Workflow.WaitConditionAsync(() => approved);

        return approved;
    }

    [WorkflowSignal]
    public async Task ApproveSignal() => approved = true;
}