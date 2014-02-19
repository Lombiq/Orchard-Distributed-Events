using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    public class DefaultEnvironmentIdentifierProvider : IEnvironmentIdentifierProvider
    {
        public string GetIdentifier()
        {
            return System.Environment.MachineName + "/" + HttpRuntime.AppDomainId;
        }
    }
}