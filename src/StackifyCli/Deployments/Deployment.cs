using System;

namespace StackifyCli.Deployments
{
    public class Deployment
    {
        public int Id { get; set; }
        public Guid? AppEnvironmentId { get; set; }
        public string App { get; set; }
        public string Environment { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Branch { get; set; }
        public string Commit { get; set; }
        public DateTimeOffset StartedAt { get; set; }
        public DateTimeOffset? EndedAt { get; set; }
    }
}