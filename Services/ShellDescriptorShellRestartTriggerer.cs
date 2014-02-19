using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lombiq.Hosting.DistributedEvents.Events;
using Lombiq.Hosting.DistributedEvents.Models;
using Orchard.Environment.Configuration;
using Orchard.Environment.Descriptor;
using Orchard.Environment.Descriptor.Models;
using Orchard.Environment.Extensions;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    [OrchardFeature("Lombiq.Hosting.DistributedEvents.Shell")]
    public class ShellDescriptorShellRestartTriggerer : IShellDescriptorManagerEventHandler
    {
        private readonly ShellSettings _shellSettings;
        private readonly IDistributedShellRestartTriggerer _restartTriggerer;


        public ShellDescriptorShellRestartTriggerer(ShellSettings shellSettings, IDistributedShellRestartTriggerer restartTriggerer)
        {
            _shellSettings = shellSettings;
            _restartTriggerer = restartTriggerer;
        }


        void IShellDescriptorManagerEventHandler.Changed(ShellDescriptor descriptor, string tenant)
        {
            _restartTriggerer.TriggerRestart(_shellSettings);
        }
    }
}