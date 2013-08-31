using System.Collections.Generic;
using System.Linq;

namespace BuildLight.Core.Configuration
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        public string TeamCityPassword
        {
            get
            {
                return Properties.Settings.Default.TeamCityPassword;
            }
        }

        public string TeamCityUser
        {
            get
            {
                return Properties.Settings.Default.TeamCityUser;
            }
        }

        public string TeamCityUrl
        {
            get
            {
                return Properties.Settings.Default.TeamCityUrl;
            }
        }

        public int PollingIntervalSeconds
        {
            get
            {
                return Properties.Settings.Default.PollingIntervalSeconds;
            }
        }

        public IList<string> OnlyCheckTheseConfigurationsForFailure
        {
            get
            {
                var configurations = Properties.Settings.Default.OnlyCheckTheseConfigurationsForFailure;
                if (configurations != null)
                {
                    return new List<string>(configurations.OfType<string>());
                }

                return null;
            }
        }
    }
}