using ElasticPMTServer.Models;
using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        public static ElasticLowLevelClient client = new ElasticLowLevelClient();

        public  StringResponse createRequirement(Requirement requirement)
        {
            var result = client.Index<StringResponse>("elasticpmt", PostData.Serializable(requirement));
            return result;
        }
    }
}
