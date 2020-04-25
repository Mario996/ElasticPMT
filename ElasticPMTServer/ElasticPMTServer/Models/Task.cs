namespace ElasticPMTServer.Models
{
    public class Task
    {
        public string Type { get; set; }
        public string Summary { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Environment { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
    }
}
