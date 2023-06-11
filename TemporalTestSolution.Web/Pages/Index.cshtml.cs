using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Temporalio.Client;
using TemporalTestSolution.Workflows;

namespace TemporalTestSolution.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Task<TemporalClient> _lazyTemporalClient;

    public IndexModel(ILogger<IndexModel> logger, Task<TemporalClient> lazyTemporalClient)
    {
        _logger = logger;
        _lazyTemporalClient = lazyTemporalClient;
    }

    public void OnGet()
    {

    }

    [BindProperty]
    public SubmitRequestModel BucketRequest { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var temporalClient = await _lazyTemporalClient;

        var response = await temporalClient.StartWorkflowAsync(
            (InfrastructureProvisionWorkFlow wf) => wf.RunAsync(BucketRequest.IBan, BucketRequest.BucketName, BucketRequest.Email),
            new(id: Guid.NewGuid().ToString(), taskQueue: "balance-queue"));

        var result =  await response.GetResultAsync();


        ViewData["result"] = result;
        return Page();
    }
}
