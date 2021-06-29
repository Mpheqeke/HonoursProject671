using System;
using Project.Core.Interfaces;
using Project.Core.Utilities;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Options;

namespace Project.Infrastructure.Utilities
{
    public class AzureFileStorageHelper : IFileStorageHelper
    {
        private IOptions<AppSettings> _appSettings;

        public AzureFileStorageHelper(IOptions<AppSettings> settings)
        {
            _appSettings = settings;
        }

        public byte[] DownloadFile(Uri filePath)
        {
            var blockblob = new CloudBlockBlob(filePath);
            blockblob.FetchAttributes();
            var file = new byte[blockblob.Properties.Length];
            blockblob.DownloadToByteArray(file, 0);
            return file;
        }

        public string GetFullPath(string fileName, string containerName)
        {
            var storageacc = CloudStorageAccount.Parse(_appSettings.Value.StorageConnectionString);
            var cloudBlobClient = storageacc.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
            var cloudBlob = cloudBlobContainer.GetBlobReference(fileName);
            cloudBlob.FetchAttributes();
            return cloudBlob.Uri.ToString();
        }

        public string UploadFile(string fileName, string containerName, byte[] fileData)
        {
			var storageacc = CloudStorageAccount.Parse(_appSettings.Value.StorageConnectionString);
			var blobClient = storageacc.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference(containerName);

	        container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

	        var blockBlob = container.GetBlockBlobReference(fileName);
	        blockBlob.UploadFromByteArray(fileData, 0, fileData.Length);

	        return blockBlob.Uri.ToString();
        }

        public void RemoveFile(string fileName, string containerName)
        {
            var storageacc = CloudStorageAccount.Parse(_appSettings.Value.StorageConnectionString);
			var blobClient = storageacc.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference(containerName);
			var blockBlob = container.GetBlockBlobReference(fileName);
			blockBlob.DeleteIfExists();
		}

        public string CheckExtension(string ext)
        {
            if (ext == "peg")
                ext = "jpg";

            return ext;
        }
    }
}
