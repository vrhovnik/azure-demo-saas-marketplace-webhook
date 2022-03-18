using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

            var payload = JsonConvert.DeserializeObject<WebhookPayload>(requestBody);

            log.LogInformation("Addding data to Azure Tables and console output");

            var currentMessage =
                $"Webhook was called at {DateTime.Now} with following details:{Environment.NewLine}" +
                $"action {payload.Action}, status {payload.Status}, on offer {payload.OfferId} on plan {payload.PlanId}";

            log.LogInformation("Action {0}, status {1}, offer id {2}, plan {3}",
                payload.Action, payload.Status, payload.OfferId, payload.PlanId);

            var logItem = new LogTableModel
            {
                PartitionKey = "webhooklogs",
                RowKey = Guid.NewGuid().ToString(),
                Text = currentMessage,
                LoggedDate = DateTime.Now
            };

            await tableCollector.AddAsync(logItem);

            stopwatch.Stop();

            log.LogInformation("Azure Function Webhook execution took {0}", stopwatch.ElapsedMilliseconds);

            return new OkObjectResult("Message was saved - acknowledged receive");
        }
        catch (Exception e)
        {
            log.LogError("Webhook call has failed, check error: {0}", e.Message);
            return new BadRequestResult();
        }
    }
}