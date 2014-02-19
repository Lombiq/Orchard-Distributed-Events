using Orchard;
using Orchard.Environment.Configuration;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    /// <summary>
    /// Service for triggering a shell restart on all server nodes.
    /// </summary>
    public interface IDistributedShellRestartTriggerer : IDependency
    {
        /// <summary>
        /// Invokes a shell restart for a shell on all server nodes.
        /// </summary>
        /// <param name="settings">Shell settings of the shell.</param>
        void TriggerRestart(ShellSettings settings);
    }
}
