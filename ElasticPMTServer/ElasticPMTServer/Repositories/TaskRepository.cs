﻿using Nest;
using Task = ElasticPMTServer.Models.Task;

namespace ElasticPMTServer.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public static ConnectionSettings settings = new ConnectionSettings()
                                                       .DefaultMappingFor<Task>(m => m
                                                       .IndexName("tasks"));

        public static ElasticClient client = new ElasticClient(settings);

        public bool checkIfIndexExists()
        {
            return client.Indices.Exists("tasks").Exists;
        }

        public CreateIndexResponse createIndex()
        {
            return client.Indices.Create("tasks", c => c
                     .Settings(s => s
                         .NumberOfShards(1)
                     )
                     .Map(m => m
                         .Properties(p => p
                             .Text(t => t
                                 .Name("task_summary")
                                 .Name("task_component")
                                 .Name("task_type")
                                 .Name("task_priority")
                                 .Name("task_assignee")
                                 .Name("task_environment")
                                 .Name("task_description")
                             )
                         )
                     )
         );
        }

        public IndexResponse createTask(Task task)
        {
            if (checkIfIndexExists())
            {
                return client.Index(task, i => i
                .Refresh(Elasticsearch.Net.Refresh.True));
            }
            else
            {
                var response = createIndex();
                if (response.IsValid)
                {
                    return client.Index(task, i => i
                        .Refresh(Elasticsearch.Net.Refresh.True));
                }
                return null;
            }
        }

        public DeleteResponse deleteTask(string id)
        {
            return client.Delete<Task>(id);
        }

        public GetResponse<Task> getTaskById(string id)
        {
            return client.Get<Task>(id);
        }

        public ISearchResponse<Task> getTasks()
        {
            client.Indices.Refresh();
            return client.Search<Task>(s => s
               .MatchAll()
            );
        }

        public UpdateResponse<Task> updateTask(string id, Task task)
        {
            return client.Update<Task>(id, u => u
                 .Doc(task)
                 .Refresh(Elasticsearch.Net.Refresh.True)
         );
        }
    }
}
