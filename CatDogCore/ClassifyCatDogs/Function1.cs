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
        private const string CategoryCat = "animal_cat";
        private const string CategoryDog = "animal_dog";
        private const string TagCat = "cat";
        private const string TagDog = "dog";
        private const string ContainerCats = "cats";
        private const string ContainerDogs = "dogs";
        private const string ContainerOther = "other";
        private const string CSApiKeyKey = "Values:CS_ApiKey";
        private static string _connectionstring = "";

        [FunctionName("ClassifyCatDogs")]
        public static void Run([BlobTrigger("uploads/{name}", Connection = "ConnectionStrings:BlobStorage")]Stream myBlob, string name, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

            var config = new ConfigurationBuilder()
                .SetBasePath(context.FunctionAppDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var subscriptionKey = config.GetValue<string>(CSApiKeyKey);
            _connectionstring = config.GetConnectionString("BlobStorage");
            var computerVision = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(subscriptionKey),
                new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = "https://westeurope.api.cognitive.microsoft.com"
            };

            using (var stream = new MemoryStream())
            {
                myBlob.CopyTo(stream);
                if (stream.CanSeek)
                    stream.Seek(0, SeekOrigin.Begin);
                if (myBlob.CanSeek)
                    myBlob.Seek(0, SeekOrigin.Begin);



                var analysis = computerVision.AnalyzeImageInStreamAsync(stream,
                    new List<VisualFeatureTypes> {VisualFeatureTypes.Categories, VisualFeatureTypes.Tags}).Result;
                if (analysis.Categories != null)
                {
                    foreach (var analysisCategory in analysis.Categories.OrderByDescending(c => c.Score))
                    {
                        switch (analysisCategory.Name)
                        {
                            case CategoryCat:
                                MoveBlobToContainer(myBlob, ContainerCats, name, log);
                                return;
                            case CategoryDog:
                                MoveBlobToContainer(myBlob, ContainerDogs, name, log);
                                return;
                        }
                    }
                }

                if (analysis.Tags != null)
                {
                    foreach (var analysisCategory in analysis.Tags.OrderByDescending(c => c.Confidence))
                    {
                        switch (analysisCategory.Name)
                        {
                            case TagCat:
                                MoveBlobToContainer(myBlob, ContainerCats, name, log);
                                return;
                            case TagDog:
                                MoveBlobToContainer(myBlob, ContainerDogs, name, log);
                                return;
                        }
                    }
                }

                MoveBlobToContainer(myBlob, ContainerOther, name, log);
            }
        }

        private static void MoveBlobToContainer(Stream blob, string containerName, string filename, ILogger log)
        {
            log.LogInformation($"blob moved to {containerName}");
            MoveFile("uploads", filename, containerName, blob).Wait();
        }

        public static async Task MoveFile(string oldContainerName, string fileName, string newContainerName, Stream stream)
        {
            var newContainer = await GetCloudBlobContainer(newContainerName);
            var newCloudBlockBlob = newContainer.GetBlockBlobReference(Guid.NewGuid().ToString());
            await newCloudBlockBlob.UploadFromStreamAsync(stream);
            
            var oldContainer = await GetCloudBlobContainer(oldContainerName);
            var oldCloudBlockBlob = oldContainer.GetBlockBlobReference(fileName);
            await oldCloudBlockBlob.DeleteIfExistsAsync();
        } 

        private static async Task<CloudBlobContainer> GetCloudBlobContainer(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(_connectionstring);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            if (!await container.ExistsAsync())
            {
                await container.CreateIfNotExistsAsync();
                await container.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
            return container;
        }
    }
}
