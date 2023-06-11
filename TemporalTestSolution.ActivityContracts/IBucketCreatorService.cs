namespace TemporalTestSolution.ActivityContracts;
using Temporalio.Activities;


public interface IBucketCreatorService
{
    [Activity]
    public void CreateBucket(string bucketName);
}
