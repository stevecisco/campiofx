using System.Globalization;
using System.Net;
using fxDevOps.Common;
using fxDevOps.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fxDevOps
{
    public class fxHello
    {
        private readonly ILogger _logger;

        public fxHello(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<fxHello>();
        }

        [Function("fxHelloWorld")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string? queryName = null;
            var queryValue = QueryStringHelper.TryGetQueryStringValue(req.Url.Query, "name");
            if (queryValue.Item1) {  queryName = queryValue.Item2; }

            var response = req.CreateResponse(!string.IsNullOrWhiteSpace(queryName) ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"Welcome to Azure Functions! QueryName[{queryName}]");

            return response;
        }


    }
}
