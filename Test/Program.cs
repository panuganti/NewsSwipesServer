using GoogleCloudSamples;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ss = new Datastore();
            ss.Upload("image.jpeg", "http://i.ndtvimg.com/i/2016-02/arun-jaitley-budget_650x400_51456737432.jpg").Wait();
        }
    }
}
