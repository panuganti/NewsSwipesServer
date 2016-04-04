using GoogleDatastore;
using Search;
using DataContracts.Search;
using System;
using NewsSwipesLibrary;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestNewFeed();
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

        static void TestNewFeed()
        {
            var feeds = new Feeds(new Utils(), IndexFactory.FeedsIndex, IndexFactory.SkippedUrlsIndex);
            var f = feeds.LoadFeeds("loksatta");
        }
}
}
