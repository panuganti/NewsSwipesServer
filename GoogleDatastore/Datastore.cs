
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
using System.Web;

namespace GoogleDatastore
{
    public class Datastore
    {
        private const string projectId = "wwwarchishainnovatorscom";
        private const string bucketName = "www.archishainnovators.com";

        public IConfigurableHttpClientInitializer GetApplicationDefaultCredentials()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());

            var docPath = HttpContext.Current.Server.MapPath("/bin/wwwarchishainnovatorscom-eaed27291ff7.json");
            //var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), @".\wwwarchishainnovatorscom-eaed27291ff7.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", docPath, EnvironmentVariableTarget.Process);

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

        public async Task<bool> UploadImageAsync(string filename, string imageUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    byte[] data = webClient.DownloadData(imageUrl);
                    MemoryStream mem = new MemoryStream(data);
                    return await UploadAsync(filename, mem, "image/jpeg", "images");
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public async Task<bool> UploadUserContactsAsync(string filename, string data)
        {
            MemoryStream mem = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(mem))
            {
                writer.Write(data);
                writer.Flush();
                mem.Position = 0;
                return await UploadAsync(filename, mem, "text/plain", "contacts");
            }
        }

        public async Task<bool> UploadStorageInfoAsync(string filename, string data)
        {
            MemoryStream mem = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(mem))
            {
                writer.Write(data);
                writer.Flush();
                mem.Position = 0;
                return await UploadAsync(filename, mem, "text/plain", "storage");
            }
        }

        public async Task<bool> UploadAsync(string filename, MemoryStream mem, string datatype, string dirName)
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

            filename = String.Format("{0}/{1}", dirName, filename);
            var fileobj = new Google.Apis.Storage.v1.Data.Object() { Name = filename, Acl = acl };
            await service.Objects.Insert(fileobj, bucketName, mem, datatype).UploadAsync();
            return true;
        }
    }
}
