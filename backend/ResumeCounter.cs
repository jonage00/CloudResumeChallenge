using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

// The following will use the SQL API to retrieve the Azure Cosmos DB item and lookup the ID from a query string. 

namespace CosmosDBFunction
{
    public static class ResumeCounterFromQueryString
    {
        [FunctionName("ResumeCounterFromQueryString")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
            databaseName:"cloudresume", 
            containerName:"counter", 
            Connection ="CosmosDbConnectionString", 
            Id = "1", 
            PartitionKey = "1")] 
            Counter counter,
            [CosmosDB(
                databaseName:"cloudresume", 
                containerName:"counter", 
                Connection ="CosmosDbConnectionString", 
                Id = "1", 
                PartitionKey = "1")] 
                out Counter updatedcounter,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // This will increment the count by 1. 

            updatedcounter = counter;
            updatedcounter.Count += 1;

            var Jsontoreturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(Jsontoreturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
