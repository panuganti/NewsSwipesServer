using GoogleDatastore;
using Search;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using DataContracts;
using DataContracts.Search;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSearch();
        }

        static void TestUploadToIndex()
        {
            var feed = new Search.Feed();
            var article = new Article();
            article.CardStyle = CardType.Default.ToString();
            feed.UploadDocument(article, "2").Wait();
        }

        static void TestSearch()
        {
            var feed = new Search.Feed();
            var result = feed.Search("*").Result;
            Console.WriteLine("",result.Results.Count);
            Console.ReadKey();
        }

        static void TestIndexSearch()
        {
            var image = new Image();
            image.DateAdded = DateTime.Now;
            image.Id = Guid.NewGuid().ToString();
            image.SourceUrl = "http://i.ndtvimg.com/i/2016-02/arun-jaitley-budget_650x400_51456737432.jpg";
            image.Tags = new string[] { "Jaitley" };
            image.Url = "https://storage.googleapis.com/www.archishainnovators.com/images/image.jpeg";
            var imageIndex = new ImagesIndex();
            imageIndex.UploadDocument(image).Wait();
        }

        static void TestDatastoreUpload()
        {
            var ss = new Datastore();
            ss.Upload("image.jpeg", "http://i.ndtvimg.com/i/2016-02/arun-jaitley-budget_650x400_51456737432.jpg").Wait();
        }
    }
}
