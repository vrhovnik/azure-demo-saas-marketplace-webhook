using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

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
}