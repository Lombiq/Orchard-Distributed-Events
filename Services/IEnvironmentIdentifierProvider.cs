using Orchard;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    /// <summary>
    /// Defines a service that can provide a unique identifier for the application domain that currently runs the site. I.e. the identifier
    /// should uniquely identify (among the servers of a web farm or worker processes of a web garden) the environment that runs the site.
    /// </summary>
    public interface IEnvironmentIdentifierProvider : IDependency
    {
        string GetIdentifier();
    }
}