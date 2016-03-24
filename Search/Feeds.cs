using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using DataContracts.Search;
using DataContracts;
using System.Threading.Tasks;

namespace Search
{
    public class Feeds
    {
        SearchServiceClient ServiceClient { get; set; }
        SearchIndexClient _indexClient;


        public Feeds() 
            : this(new SearchServiceClient("serviceName", new SearchCredentials("apiKey")).Indexes.GetClient("feeds"))
        {
        }

        private Feeds(SearchIndexClient indexClient)
        {
            _indexClient = indexClient;
        }

        public async Task<DocumentIndexResult> UploadDocuments(IEnumerable<Article> articles)
        {
            try
            {
                IndexBatch<Feed> batch = IndexBatch.MergeOrUpload(articles.Select(article => article.ToFeed("1")));
                var result = await _indexClient.Documents.IndexAsync(batch);
                return result;
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                string exceptionMessage = String.Format(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
                throw new Exception(exceptionMessage);
            }
        }

        public async Task<DocumentSearchResult<Feed>> SearchFeeds(string searchText, string filter = null)
        {
            // Execute search based on search text and optional filter
            var sp = new SearchParameters();

            if (!String.IsNullOrEmpty(filter))
            {
                sp.Filter = filter;
            }

            DocumentSearchResult<Feed> response = await _indexClient.Documents.SearchAsync<Feed>(searchText, sp);
            return response;
        }
    }
}
