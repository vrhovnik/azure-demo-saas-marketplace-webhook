using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SaasFunctions;

public static class HealthCheck
{
    [FunctionName("HealthCheck")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req,
        ILogger log)
    {
        var message = "Azure Functions is alive at " + DateTime.Now;
        log.LogInformation("Azure Functions is alive and kicking at {0}", DateTime.Now);
        return new OkObjectResult(message);
    }
}