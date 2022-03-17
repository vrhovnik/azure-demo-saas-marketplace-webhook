using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SaasFunctions.Models;

namespace SaasFunctions;

public static class MarketplaceWebhook
{
    [FunctionName("MarketplaceWebhook")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "webhook")]
        HttpRequest req,
        [Table("webhooklogs")] IAsyncCollector<LogTableModel> tableCollector,
        ILogger log)
    {
        log.LogInformation("Received request: {0}", req);

        try
        {
            var stopwatch = Stopwatch.StartNew();

            var requestBody = await new StreamReader(req.Body)
                .ReadToEndAsync();

            log.LogInformation("Request body as string {0}", requestBody);

            if (string.IsNullOrEmpty(requestBody))
            {
                log.LogError("Request body is empty, check values.");
                return new BadRequestResult();
            }
            
            //TODO: transform body to appropriate message and react based on that
            
            log.LogInformation("Addding data to Azure Tables for log purposes");
            var currentMessage = $"Webhook was called at {DateTime.Now}";
            var logItem = new LogTableModel
            {
                PartitionKey = "webhooklogs",
                RowKey = Guid.NewGuid().ToString(),
                Text = currentMessage,
                LoggedDate = DateTime.Now
            };

            await tableCollector.AddAsync(logItem);

            stopwatch.Stop();

            log.LogInformation("Webhook execution took {0}", stopwatch.ElapsedMilliseconds);

            return new OkObjectResult("Message was saved and ");
        }
        catch (Exception e)
        {
            log.LogError("Webhook has failed, check error: {0}", e.Message);
            return new BadRequestResult();
        }
    }
}