using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using DataContracts.Search;
using System.Threading.Tasks;

namespace Search
{
    public class SearchIndex
    {
        SearchIndexClient _indexClient;

        // ext-service newsswipesprod, FCCE9E142A7AD2BC492E1C9DB9F650FA
        protected SearchIndex(string indexName, string serviceName, string key)
            : this( new SearchServiceClient(serviceName, new SearchCredentials(key))
                  .Indexes.GetClient(indexName))
        {
        }

        private SearchIndex(SearchIndexClient indexClient)
        {
            _indexClient = indexClient;
        }

        public async Task<T> LookupDocument<T>(string key) where T : SearchDoc
        {
            try
            {
                return await _indexClient.Documents.GetAsync<T>(key);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<DocumentIndexResult> UploadDocument<T>(T doc) where T : SearchDoc
        {
            try
            {
                return await UploadDocuments(new[] { doc });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<DocumentIndexResult> UpdateDocument<T>(T doc) where T : SearchDoc
        {
            try
            {
                return await UploadDocuments(new[] { doc });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<DocumentIndexResult> UploadDocuments<T>(IEnumerable<T> docs) where T : SearchDoc
        {
            try
            {
                IndexBatch<T> batch = IndexBatch.MergeOrUpload(docs);
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

        public async Task<DocumentSearchResult<T>> SearchAsync<T>(string searchText, SearchParameters sp) where T : SearchDoc
        {
            return await _indexClient.Documents.SearchAsync<T>(searchText, sp);
        }

        public async Task<DocumentSearchResult<T>> Search<T>(string searchText, string filter = null) where T: SearchDoc
        {
            var sp = new SearchParameters();
            if (!string.IsNullOrEmpty(filter))
            {
                sp.Filter = filter;
                sp.IncludeTotalResultCount = true;
            }
            DocumentSearchResult<T> response = await _indexClient.Documents.SearchAsync<T>(searchText, sp);
            return response;
        }
    }
}
