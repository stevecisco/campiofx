using System.Net;
using fxDevOps.Common;
using fxDevOps.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fxDevOps
{
    public class fxHelloWorldPerson
    {
        private readonly ILogger _logger;

        public fxHelloWorldPerson(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<fxHelloWorldPerson>();
        }

        [Function("fxHelloWorldPerson")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string body = await new StreamReader(req.Body).ReadToEndAsync();
            Person? bodyPerson = JsonConvert.DeserializeObject<Person>(body);

            HttpResponseData response;
            if (bodyPerson == null)
            {
                response = req.CreateResponse(HttpStatusCode.NotFound);
                response.WriteString($"Welcome to Azure Functions! No body supplied");
            }
            else
            {
                response = req.CreateResponse(HttpStatusCode.OK);
                response.WriteString($"Welcome to Azure Functions! {bodyPerson.FirstName} {bodyPerson.LastName}");
            }
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //Test
            return response;
        }
    }
}
