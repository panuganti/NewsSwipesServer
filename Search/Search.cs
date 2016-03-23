using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace Search
{
    public class Search
    {
        SearchServiceClient ServiceClient { get; set; }

        public Search() 
            : this(new SearchServiceClient("serviceName", new SearchCredentials("apiKey")))
        {
        }

        private Search(SearchServiceClient serviceClient)
        {
            ServiceClient = serviceClient;
        }        


        public void UploadDocuments(string indexName, IEnumerable<Document> documents)
        {
            SearchIndexClient indexClient = ServiceClient.Indexes.GetClient(indexName);

            try
            {
                IndexBatch batch = IndexBatch.Upload(documents);
                indexClient.Documents.Index(batch);
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Console.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }
        }

        public void SearchDocuments<T>(string indexName, string searchText, string filter = null) where T : class
        {
            SearchIndexClient indexClient = ServiceClient.Indexes.GetClient(indexName);

            // Execute search based on search text and optional filter
            var sp = new SearchParameters();

            if (!String.IsNullOrEmpty(filter))
            {
                sp.Filter = filter;
            }

            DocumentSearchResult<T> response = indexClient.Documents.Search<T>(searchText, sp);
            foreach (SearchResult<T> result in response.Results)
            {
                Console.WriteLine(result.Document);
            }
        }
    }
}