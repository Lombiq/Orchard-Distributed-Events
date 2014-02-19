using Orchard.Data.Conventions;

namespace Lombiq.Hosting.DistributedEvents.Models
{
    public class DistributedEventRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string EnvironmentIdentifier { get; set; }
        [StringLengthMax]
        public virtual string Context { get; set; }
    }
}