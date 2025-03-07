using Azure;
using Azure.Search.Documents;

namespace SearchEngine.Web.Infrastructure.Services.SearchService
{
    public class BaseSearchService(string indexName, string endpoint, string adminKey)
    {
        public readonly SearchClient SearchClient = new(new Uri(endpoint), indexName, new AzureKeyCredential(adminKey));
    }
}
