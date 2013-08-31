using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BuildLight.Core.Configuration
{
    public interface IApplicationConfiguration
    {
        string TeamCityPassword { get; }
        string TeamCityUser { get; }
        string TeamCityUrl { get; }
        int PollingIntervalSeconds { get; }
        IList<string> OnlyCheckTheseConfigurationsForFailure { get; }
    }
}
