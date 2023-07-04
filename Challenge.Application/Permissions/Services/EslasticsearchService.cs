using Azure;
using Challenge.Core;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Challenge.Application.Permissions.Services
{
    public class EslasticsearchService : IEslasticsearchService
    {
        private readonly IElasticClient _elasticClient;
        private readonly IConfiguration _config;
        private readonly string _indexName = string.Empty;

        public EslasticsearchService(IElasticClient elasticClient, IConfiguration config)
        {
            _elasticClient = elasticClient;
            _config = config;
            _indexName = _config["Elasticsearch:IndexName"] ?? "";
        }

        public async Task Add<T>(T obj) where T : class 
        {

            var response = await _elasticClient.IndexDocumentAsync(obj);

            if (!response.IsValid)
                throw new Exception($"Error: {response.DebugInformation}");
        }


        public async Task Update<T>(T obj) where T: EntityBase
        {
            var response = await _elasticClient.UpdateAsync<T>(obj.Id, u => u.Index(_indexName).Doc(obj));

            if (!response.IsValid)
                throw new Exception($"Error: {response.DebugInformation}");
        }

        public async Task<IEnumerable<T>> Search<T>(string query, int page = 1, int pageSize = 5) where T : class
        {
            query = string.IsNullOrEmpty(query) ? "*" : $"*{query}*";

            var response = await _elasticClient.SearchAsync<T>(s => s.Index(_indexName)
                                                                            .Query(q => q.QueryString(d => d.Query(query)))
                                                                            .From((page - 1) * pageSize)
                                                                            .Size(pageSize));
            if (!response.IsValid)
                throw new Exception($"Error: {response.DebugInformation}");

            return response.Documents;
        }

        public async Task Reset()
        {
            var response = await _elasticClient.Indices.DeleteAsync(Indices.All);
                        
            if (!response.IsValid)
                throw new Exception($"Error: {response.DebugInformation}");
        }
    }
}
