using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ClassifyCatDogs
{
    public static class Function1
    {
        // CS categories and tags
        private const string CategoryCat = "animal_cat";
        private const string CategoryDog = "animal_dog";
        private const string TagCat = "cat";
        private const string TagDog = "dog";

        // blob storage containers
        private const string ContainerUploads = "uploads";
        private const string ContainerCats = "cats";
        private const string ContainerDogs = "dogs";
        private const string ContainerOther = "other";

        // cs configuration
        private const string CSApiKeyKey = "Values:CS_ApiKey";
        private const string CSEndpoint = "https://westeurope.api.cognitive.microsoft.com";
        private const double ClassificationThreshold = 0.5;

        // blob storage configuration
        private static string _connectionString = "";

        [FunctionName("ClassifyCatDogs")]
        public static void Run([BlobTrigger("uploads/{name}", Connection = "ConnectionStrings:BlobStorage")]Stream myBlob, string name, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            // get configuration (requires ExecutionContext)
            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // load values from configuration
            var subscriptionKey = config.GetValue<string>(CSApiKeyKey);
            _connectionString = config.GetConnectionString("BlobStorage");
            log.LogInformation($"CS_ApiKey: {subscriptionKey}");
            log.LogInformation($"BlobStorage: {_connectionString}");

            // initialize computer vision client
            var computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
            {
                Endpoint = CSEndpoint
            };
            
            // query computer vision api (https://docs.microsoft.com/en-us/javascript/api/azure-cognitiveservices-computervision/computervisionclient?view=azure-node-latest)
            var analysis = computerVision.AnalyzeImageInStreamAsync(myBlob,
                new List<VisualFeatureTypes>
                {
                        VisualFeatureTypes.Categories,
                        VisualFeatureTypes.Tags
                }).Result;
            
            var catDogProbability = new CatDogProbability();

            // search for category
            if (analysis.Categories != null)
            {
                foreach (var analysisCategory in analysis.Categories.OrderByDescending(c => c.Score))
                {
                    switch (analysisCategory.Name)
                    {
                        case CategoryCat:
                            catDogProbability.AddCatValue(analysisCategory.Score);
                            break;
                        case CategoryDog:
                            catDogProbability.AddDogValue(analysisCategory.Score);
                            break;
                    }
                }
            }

            // search for tag
            if (analysis.Tags != null)
            {
                foreach (var analysisCategory in analysis.Tags.OrderByDescending(c => c.Confidence))
                {
                    switch (analysisCategory.Name)
                    {
                        case TagCat:
                            catDogProbability.AddCatValue(analysisCategory.Confidence);
                            break;
                        case TagDog:
                            catDogProbability.AddDogValue(analysisCategory.Confidence);
                            break;
                    }
                }
            }

            log.LogInformation($"Cat Probability: {catDogProbability.GetCatValue()}");
            log.LogInformation($"Dog Probability: {catDogProbability.GetDogValue()}");

            // copy based on probability
            if (catDogProbability.GetCatValue() > ClassificationThreshold)
            {
                CopyBlobToContainer(ContainerCats, name, log);
            }
            if (catDogProbability.GetDogValue() > ClassificationThreshold)
            {
                CopyBlobToContainer(ContainerDogs, name, log);
            }
            if (catDogProbability.GetCatValue() <= ClassificationThreshold && catDogProbability.GetDogValue() <= ClassificationThreshold)
            {
                CopyBlobToContainer(ContainerOther, name, log);
            }

            // delete from default
            DeleteFileFromDefaultContainer(name, log).Wait();
        }

        private static void CopyBlobToContainer(string containerName, string filename, ILogger log)
        {
            log.LogInformation($"copy {ContainerUploads}/{filename} to {containerName}/{filename}");
            CopyFileFromDefaultContainer(containerName, filename).Wait();
        }

        public static async Task CopyFileFromDefaultContainer(string newContainerName, string originalFileName)
        {
            var newContainer = await GetCloudBlobContainer(newContainerName);
            var newCloudBlockBlob = newContainer.GetBlockBlobReference(originalFileName);

            if (await newCloudBlockBlob.ExistsAsync())
                newCloudBlockBlob = newContainer.GetBlockBlobReference($"{Guid.NewGuid()}{originalFileName}");

            var oldContainer = await GetCloudBlobContainer(ContainerUploads);
            var oldCloudBlockBlob = oldContainer.GetBlockBlobReference(originalFileName);

            await newCloudBlockBlob.StartCopyAsync(oldCloudBlockBlob);
        }

        public static async Task DeleteFileFromDefaultContainer(string originalFileName, ILogger log)
        {
            log.LogInformation($"delete {ContainerUploads}/{originalFileName}");
            var oldContainer = await GetCloudBlobContainer(ContainerUploads);
            var oldCloudBlockBlob = oldContainer.GetBlockBlobReference(originalFileName);
            await oldCloudBlockBlob.DeleteIfExistsAsync();
        }

        private static async Task<CloudBlobContainer> GetCloudBlobContainer(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(_connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            if (await container.ExistsAsync()) return container;
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            return container;
        }
    }
}
