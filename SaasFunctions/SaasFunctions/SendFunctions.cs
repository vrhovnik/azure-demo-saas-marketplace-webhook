using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using SaasFunctions.Models;
using SendGrid.Helpers.Mail;

namespace SaasFunctions
{
    public static class SendFunctions
    {
        /// <summary>
        /// sending email to participants
        /// </summary>
        /// <param name="req"request>request with body</param>
        /// <param name="messageCollector">send grid output trigger</param>
        /// <param name="log">injected logger</param>
        /// <returns>ok, if email successful, or bad request if there is an error</returns>
        /// <example>
        /// {
        ///  "message": "This was sent from webhook",
        ///  "subject": "Sending from webhook",
        ///  "email": "tosomebody@email.com
        ///  }
        /// </example>>
        [FunctionName("SendEmail")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "email")] HttpRequest req,
            [SendGrid(ApiKey = "SendGridApiKey")] IAsyncCollector<SendGridMessage> messageCollector,
            ILogger log)
        {
            log.LogInformation("Sending email at {0}", DateTime.Now);

            try
            {
                var requestBody = string.Empty;
                using var streamReader = new StreamReader(req.Body);
                requestBody = await streamReader.ReadToEndAsync();

                if (string.IsNullOrEmpty(requestBody))
                {
                    log.LogError("Request body is empty");
                    return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
                }

                var emailModel = JsonConvert.DeserializeObject<EmailModel>(requestBody);

                var message = new SendGridMessage();
                message.AddTo(emailModel.Email);
                message.AddContent("text/html", emailModel.Message);
                message.SetFrom(new EmailAddress("info@saashackathon.net"));
                message.SetSubject(emailModel.Subject);
            
                log.LogInformation("Sending email to client via SendGrid...");
                await messageCollector.AddAsync(message);
                log.LogInformation("Email message sent at {0}", DateTime.Now);
                
                return new OkObjectResult($"Email to {emailModel.Email} sent");
            }
            catch (Exception e)
            {
                log.LogError("There has been an error {0}", e.Message);
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
