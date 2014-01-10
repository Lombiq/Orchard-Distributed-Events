using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data.Conventions;

namespace Lombiq.Hosting.DistributedSignals.Models
{
    public class DistributedSignalRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string MachineName { get; set; }
        [StringLengthMax]
        public virtual string Context { get; set; }
    }
}