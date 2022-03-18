using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using SaasFunctions.Models;

namespace SaasFunctionsTests;

[TestFixture]
public class WebHookCalls
{
    private string BaseUrl = "";

    [SetUp]
    public void Init()
    {
        BaseUrl = Environment.GetEnvironmentVariable("SaaSUrl") ??
                  "https://saas-hackathon.azurewebsites.net/";
    }

    [Test(Description = "Checks if Webhooks handle empty body")]
    public async Task Check_If_Webhook_Handle_Empty_Request_Body()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl, UriKind.Absolute) };
        var responseMessage = await httpClient.PostAsync("webhook", new StringContent(string.Empty));
        Assert.AreEqual(responseMessage.IsSuccessStatusCode, false, "Webhook knows form is empty");
        Assert.AreEqual(responseMessage.StatusCode, HttpStatusCode.BadRequest,
            "Webhook returned bad request as a message");
    }

    [Test(Description = "Checks if calling webhook with request body with change plan")]
    public async Task Check_If_Webhook_Handle_Request_Body()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl, UriKind.Absolute) };
        string bodyObject = JsonConvert.SerializeObject(new WebhookPayload
        {
            Action = WebhookAction.ChangePlan,
            Status = OperationStatusEnum.InProgress,
            ActivityId = Guid.NewGuid(),
            OfferId = Guid.NewGuid().ToString(),
            OperationId = Guid.NewGuid(),
            PublisherId = Guid.NewGuid().ToString(),
            PlanId = "my-plan-id",
            Quantity = 10,
            SubscriptionId = Guid.NewGuid(),
            TimeStamp = DateTimeOffset.Now
        });

        var body = new StringContent(bodyObject, Encoding.UTF8, "application/json");
        var responseMessage = await httpClient.PostAsync("webhook", body);

        Assert.AreEqual(responseMessage.IsSuccessStatusCode, true, "Webhook form was submitted successfully");
    }
}