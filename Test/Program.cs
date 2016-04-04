using GoogleDatastore;
using Search;
using DataContracts.Search;
using System;
using NewsSwipesLibrary;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNewFeed().Wait();
        }

        static void TestIndexSearch()
        {
            var image = new ImagesIndexDoc();
            image.DateAdded = DateTime.Now;
            image.Id = Guid.NewGuid().ToString();
            image.SourceUrl = "http://i.ndtvimg.com/i/2016-02/arun-jaitley-budget_650x400_51456737432.jpg";
            image.Tags = new string[] { "Jaitley" };
            image.Url = "https://storage.googleapis.com/www.archishainnovators.com/images/image.jpeg";
            var imageIndex = IndexFactory.ImagesIndex;
            imageIndex.UploadDocument(image).Wait();
        }

        static void TestDatastoreUpload()
        {
            var ss = new Datastore();
            ss.Upload("image.jpeg", "http://i.ndtvimg.com/i/2016-02/arun-jaitley-budget_650x400_51456737432.jpg").Wait();
        }

        static async Task TestNewFeed()
        {
            var feeds = new Feeds(new Utils(), IndexFactory.FeedsIndex, IndexFactory.SkippedUrlsIndex);
            var f = await feeds.LoadFeeds("http://news.google.co.in/news?cf=all&ned=hi_in&hl=en&output=rss");
            Console.WriteLine(f.Length);
            Console.WriteLine(f[0].Title);
            Console.WriteLine(f[0].Description);
            Console.ReadKey();
        }
    }
}
