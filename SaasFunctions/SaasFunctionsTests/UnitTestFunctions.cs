using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using SaasFunctions;

namespace SaasFunctionsTests;

public class Tests
{
    private string BaseUrl = "";

    [SetUp]
    public void Init()
    {
        BaseUrl = Environment.GetEnvironmentVariable("SaaSUrl") ?? 
                  "https://saas-hack-functions.azurewebsites.net/";
    }

    [Test(Description = "Checks if Azure Functions runs at that URL")]
    public async Task Check_If_Azure_FunctionsRuns()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl, UriKind.Absolute) };
        var responseMessage = await httpClient.GetAsync("health");
        Assert.AreEqual(responseMessage.IsSuccessStatusCode, true, "Azure Functions is up and running");
    }

    [Test(Description = "Checks, if Azure Functions sends email")]
    public async Task Check_If_Azure_Functions_Sends_Email()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(BaseUrl, UriKind.Absolute) };
        var emailObject = JsonConvert.SerializeObject(new EmailModel
        {
            Email = Environment.GetEnvironmentVariable("DefaultEmail"),
            Message = "Testing from local unit tests",
            Subject = "Sending email to receiver"
        });
        var responseMessage =
            await httpClient.PostAsync("email", new StringContent(emailObject, Encoding.UTF8, "application/json"));
        Assert.AreEqual(responseMessage.IsSuccessStatusCode, true, "Azure Functions has sent an email.");
    }
}