using Orchard.Environment.Configuration;
using Orchard.Environment.Extensions;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    // This would work, if it would... See: https://orchard.codeplex.com/workitem/20413

    /// <summary>
    /// Triggers a shell restart on all the server nodes if the shell settings change.
    /// </summary>
    [OrchardFeature("Lombiq.Hosting.DistributedEvents.ShellLifetime")]
    public class ShellSettingsShellRestartTriggerer : IShellSettingsManagerEventHandler
    {
        private readonly IDistributedShellRestartTriggerer _restartTriggerer;


        public ShellSettingsShellRestartTriggerer(IDistributedShellRestartTriggerer restartTriggerer)
        {
            _restartTriggerer = restartTriggerer;
        }


        public void Saved(ShellSettings settings)
        {
            _restartTriggerer.TriggerRestart(settings);
        }
    }
}