using System;
using System.Net;
using System.Net.Http;
using BuildLight.Core.CodeContracts;
using BuildLight.Core.Configuration;
using NLog;

namespace BuildLight.Core
{
    public class BuildStatusChecker : IBuildStatusChecker
    {
        private readonly IApplicationConfiguration applicationConfiguration;
        const string REQUEST_PATH = "/httpAuth/app/rest/cctray/projects.xml";
        readonly Logger logger = LogManager.GetCurrentClassLogger();

        public BuildStatusChecker(IApplicationConfiguration applicationConfiguration)
        {
            ParameterCheck.ParameterRequired(applicationConfiguration, "applicationConfiguration");

            this.applicationConfiguration = applicationConfiguration;
        }

        public BuildStatusCollection Check()
        {
            var status = new BuildStatusCollection();
            string response = "NO RESPONSE";

            try
            {
                var webRequestHandler = new WebRequestHandler();
                if (!string.IsNullOrWhiteSpace(applicationConfiguration.TeamCityUser) && !string.IsNullOrWhiteSpace(applicationConfiguration.TeamCityPassword))
                {
                    webRequestHandler.Credentials = new NetworkCredential(applicationConfiguration.TeamCityUser, applicationConfiguration.TeamCityPassword);
                }
                using (var client = new HttpClient(webRequestHandler))
                {
                    client.BaseAddress = new Uri(applicationConfiguration.TeamCityUrl);
                    logger.Debug("Getting status from TeamCity at URL [{0}] with user [{1}] and password [{2}]", new Uri(client.BaseAddress, REQUEST_PATH), applicationConfiguration.TeamCityUser ?? "Anonymous",
                                 applicationConfiguration.TeamCityPassword ?? "N/A");
                    response = client.GetStringAsync(REQUEST_PATH).Result;
                    logger.Debug("Response from server is {0}", response);
                    return new BuildStatusCollection(response, applicationConfiguration.OnlyCheckTheseConfigurationsForFailure);
                }
            }
            catch (Exception err)
            {
                logger.ErrorException(
                    string.Format(
                        "Unexpected exception occured when checking status at TeamCity.  URL [{0}], User [{1}], Password [{2}], RawResponse [{3}]",
                        applicationConfiguration.TeamCityUrl ?? " NULL", applicationConfiguration.TeamCityUser ?? "Anonymous",
                        applicationConfiguration.TeamCityPassword ?? "N/A", response), err);
            }

            return status;
        }
    }
}