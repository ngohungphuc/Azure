#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"

using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.Text; 

public static async Task Run(CloudBlockBlob myBlob, string name, TraceWriter log)
{   
    log.Info($"C# Blob trigger function Processed blob\n Name:{name}");
    
    var uri = GetUri(myBlob);
    var analysis = await GetAnalysis(uri);
    
    log.Info($"results: {analysis}");
}

public static Uri GetUri(CloudBlockBlob blob) 
{
    var sasPolicy = new SharedAccessBlobPolicy
    {
        Permissions = SharedAccessBlobPermissions.Read,
        SharedAccessStartTime = DateTime.Now.AddMinutes(-30),
        SharedAccessExpiryTime = DateTime.Now.AddMinutes(30)
    };
    var sasToken = blob.GetSharedAccessSignature(sasPolicy);
    var uri = new Uri($"{blob.Uri.ToString()}{sasToken}");
    return uri;
}

public static async Task<string> GetAnalysis(Uri uri) 
{
    var client = new HttpClient();
    var data = JsonConvert.SerializeObject(new { url = uri.ToString() });    
    var request = new HttpRequestMessage
    {
        RequestUri = new Uri("https://api.projectoxford.ai/emotion/v1.0/recognize"),
        Method = HttpMethod.Post
    };
    
    request.Headers.Add("Ocp-Apim-Subscription-Key", "");
    request.Content = new StringContent(data, Encoding.UTF8, "application/json");

    var response = await client.SendAsync(request);
    var result = await response.Content.ReadAsStringAsync();
    return result;
}