﻿using Nest;
using Task = ElasticPMTServer.Models.Task;
using System.Collections.Generic;

namespace ElasticPMTServer.Repositories
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository() : base("tasks")
        { }

        public Dictionary<string, long?> orderTasksPerCreator()
        {
            // key is email, value is number of occurances
            Dictionary<string, long?> returnValue = new Dictionary<string, long?>();
            var query = @"
                        {
                          ""aggs"" : {
                                ""tasks"" : {
                                        ""terms"" : {
                                            ""field"" : ""creator.email.keyword""
                                        }
                                }
                           }
                         }";

            var result = _elasticClient.LowLevel.Search<SearchResponse<Task>>(query).Aggregations.Terms("tasks");
            foreach (KeyedBucket<string> bucket in result.Buckets)
            {
                returnValue.Add(bucket.Key, bucket.DocCount);
            }
            return returnValue;
        }
    }
}
