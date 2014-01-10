using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombiq.Hosting.DistributedEvents.Models
{
    public interface IDistributedEvent
    {
        string Name { get; }
        string Context { get; }
    }
}
