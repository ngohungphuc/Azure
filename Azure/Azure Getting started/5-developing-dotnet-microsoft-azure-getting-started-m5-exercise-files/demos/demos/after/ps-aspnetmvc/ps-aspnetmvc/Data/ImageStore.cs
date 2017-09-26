using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PSAspNetMvc.Data
{
    public class ImageStore
    {
        public ImageStore()
        {
            _client = new CloudBlobClient(_baseUri, new StorageCredentials("pscoursestore", "ZLaw3OvvxweteiPjsAmXK9u8eYMjgevyk25LASASeHP0lsfM9wv0HWVRy2GQdz6az+KqtIi11Y85tsKRn7IrWA=="));
        }

        public async Task<string>SaveImage(Stream stream)
        {
            var id = Guid.NewGuid().ToString();
            var container = _client.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(id);
            await blob.UploadFromStreamAsync(stream);
            return id;
        }

        public Uri UriFor(string imageId)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.Now.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(30)
            };
            var container = _client.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);
            var sasToken = blob.GetSharedAccessSignature(sasPolicy);

            return new Uri(_baseUri, $"/images/{imageId}{sasToken}");
        }

        Uri _baseUri = new Uri("https://pscoursestore.blob.core.windows.net/");
        CloudBlobClient _client;
    }
}