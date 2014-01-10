using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombiq.Hosting.DistributedSignals.Models
{
    public interface IDistributedSignal
    {
        string Name { get; }
        string Context { get; }
    }
}
