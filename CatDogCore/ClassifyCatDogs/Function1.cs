using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace ClassifyCatDogs
{
    public static class Function1
    {
        [FunctionName("ClassifyCatDogs")]
        public static void Run([BlobTrigger("uploads/{name}", Connection = "ConnectionStrings:BlobStorage")]Stream myBlob, string name, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var subscriptionKey = config.GetValue<string>("Values:CS_ApiKey");
            var computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = "https://westeurope.api.cognitive.microsoft.com"
            };

            var analysis = computerVision.AnalyzeImageInStreamAsync(myBlob, new List<VisualFeatureTypes> { VisualFeatureTypes.Categories, VisualFeatureTypes.Tags }).Result;
            if (analysis.Categories != null)
            {
                foreach (var analysisCategory in analysis.Categories.OrderByDescending(c => c.Score))
                {
                    if (analysisCategory.Name == "animal_cat")
                    {
                        MoveBlobToContainer(myBlob, "cats", log);
                        return;
                    }

                    if (analysisCategory.Name == "animal_dog")
                    {
                        MoveBlobToContainer(myBlob, "dogs", log);
                        return;
                    }
                }
            }
            if (analysis.Tags != null)
            {
                foreach (var analysisCategory in analysis.Tags.OrderByDescending(c => c.Confidence))
                {
                    if (analysisCategory.Name == "cat")
                    {
                        MoveBlobToContainer(myBlob, "cats", log);
                        return;
                    }

                    if (analysisCategory.Name == "dog")
                    {
                        MoveBlobToContainer(myBlob, "dogs", log);
                        return;
                    }
                }
            }
            MoveBlobToContainer(myBlob, "other", log);
        }

        private static void MoveBlobToContainer(Stream blob, string containerName, ILogger log)
        {
            log.LogInformation($"blob moved to {containerName}");
        }
    }
}
