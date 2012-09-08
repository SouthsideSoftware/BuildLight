using System;
using System.Net;
using System.Net.Http;
using BuildLight.Core.Properties;
using NLog;

namespace BuildLight.Core
{
    public class BuildStatusChecker : IBuildStatusChecker
    {
        const string REQUEST_PATH = "/httpAuth/app/rest/cctray/projects.xml";
        readonly Logger logger = LogManager.GetCurrentClassLogger();

        public BuildStatusCollection Check()
        {
            var status = new BuildStatusCollection();
            string response = "NO RESPONSE";

            try
            {
                var webRequestHandler = new WebRequestHandler();
                if (!string.IsNullOrWhiteSpace(Settings.Default.TeamCityUser) && !string.IsNullOrWhiteSpace(Settings.Default.TeamCityPassword))
                {
                    webRequestHandler.Credentials = new NetworkCredential(Settings.Default.TeamCityUser, Settings.Default.TeamCityPassword);
                }
                using (var client = new HttpClient(webRequestHandler))
                {
                    client.BaseAddress = new Uri(Settings.Default.TeamCityUrl);
                    logger.Debug("Getting status from TeamCity at URL [{0}] with user [{1}] and password [{2}]", new Uri(client.BaseAddress, REQUEST_PATH), Settings.Default.TeamCityUser ?? "Anonymous",
                                 Settings.Default.TeamCityPassword ?? "N/A");
                    response = client.GetStringAsync(REQUEST_PATH).Result;
                    logger.Debug("Response from server is {0}", response);
                    return new BuildStatusCollection(response);
                }
            }
            catch (Exception err)
            {
                logger.ErrorException(
                    string.Format(
                        "Unexpected exception occured when checking status at TeamCity.  URL [{0}], User [{1}], Password [{2}], RawResponse [{3}]",
                        Settings.Default.TeamCityUrl ?? " NULL", Settings.Default.TeamCityUser ?? "Anonymous",
                        Settings.Default.TeamCityPassword ?? "N/A", response), err);
            }

            return status;
        }
    }
}