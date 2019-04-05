﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CatDogCore.Services
{
    public interface IBlobstorageService
    {
        Task<List<string>> GetFiles(string containerName);

        void AddFile(string containerName, Stream stream);
    }

    public class BlobstorageService : IBlobstorageService
    {
        private IConfiguration _config;

        public BlobstorageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<string>> GetFiles(string containerName)
        {
            var connectionString = _config.GetValue<string>("ConnectionStrings:BlobStorage");
            var storageAccount = CloudStorageAccount.Parse(connectionString);

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
            int? maxResultsPerQuery = null;
            BlobContinuationToken continuationToken = null;

            var list = new List<string>();

            do
            {
                var response = await container.ListBlobsSegmentedAsync(string.Empty, true, BlobListingDetails.None, maxResultsPerQuery, continuationToken, null, null);
                continuationToken = response.ContinuationToken;
                foreach (var blob in response.Results.OfType<CloudBlockBlob>())
                {
                    list.Add(blob.Uri.AbsoluteUri);
                }
            } while (continuationToken != null);

            return list;
        }

        public void AddFile(string containerName, Stream stream)
        {
            // ToDo
        }
    }
}
