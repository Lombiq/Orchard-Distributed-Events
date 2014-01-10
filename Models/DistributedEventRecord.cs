using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data.Conventions;

namespace Lombiq.Hosting.DistributedEvents.Models
{
    public class DistributedEventRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string MachineName { get; set; }
        [StringLengthMax]
        public virtual string Context { get; set; }
    }
}