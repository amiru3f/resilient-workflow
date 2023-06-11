namespace TemporalTestSolution.Web;
using System.ComponentModel.DataAnnotations;

public class SubmitRequestModel
{

    [EmailAddress]
    public string Email { set; get; }

    public string IBan { set; get; }

    public string BucketName { set; get; }

}