using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Services
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task UploadImage(Stream imageStream, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("alea-task");
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(imageStream);
        }
    }
}
