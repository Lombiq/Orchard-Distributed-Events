using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;

namespace Lombiq.Hosting.DistributedSignals.Services
{
    public interface IMachineNameAccessor : IDependency
    {
        string GetMachineName();
    }


    public class MachineNameAccessor : IMachineNameAccessor
    {
        public string GetMachineName()
        {
            return System.Environment.MachineName;
        }
    }
}