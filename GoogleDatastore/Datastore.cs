
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace GoogleDatastore
{
    public class Datastore
    {
        private const string projectId = "wwwarchishainnovatorscom";
        private const string bucketName = "www.archishainnovators.com";

        public IConfigurableHttpClientInitializer GetApplicationDefaultCredentials()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\GitHub\\NewsSwipesServer\\GoogleDatastore\\wwwarchishainnovatorscom-eaed27291ff7.json", EnvironmentVariableTarget.Process);

            try {
                GoogleCredential credential =
                    GoogleCredential.GetApplicationDefaultAsync().Result;
                if (credential.IsCreateScopedRequired)
                {
                    credential = credential.CreateScoped(new[] {
                    StorageService.Scope.DevstorageReadWrite
                });
                }
                return credential;
            }
            catch(AggregateException exception)
            {
                throw new Exception(String.Join("\n",exception.Flatten().InnerExceptions.Select(t=>t.Message)));
            }
        }

        public async Task<bool> UploadAsync(string filename, string imageUrl)
        {
            IConfigurableHttpClientInitializer credentials = GetApplicationDefaultCredentials();
            StorageService service = new StorageService( 
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials,
                    ApplicationName = "NewsSwipes",
                });

            // To make public
            var acl = new List<ObjectAccessControl>
                {
                    new ObjectAccessControl
                    {
                        Role = "OWNER",
                        Entity = "allUsers"
                    }
                };

            filename = "images/" + filename;
            var fileobj = new Google.Apis.Storage.v1.Data.Object() {Name = filename, Acl = acl };
            using (WebClient webClient = new WebClient())
            {
                try {
                    byte[] data = webClient.DownloadData(imageUrl);
                    MemoryStream mem = new MemoryStream(data);
                    await service.Objects.Insert(fileobj, bucketName, mem, "image/jpeg").UploadAsync();
                    return true;
                    //var insmedia = new ObjectsResource.InsertMediaUpload(service, fileobj, bucketName, mem, "image/jpeg");
                    //await insmedia.UploadAsync();
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
