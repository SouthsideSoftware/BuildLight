using System;
using System.Net;
using System.Net.Http;
using BuildLight.Core;
using FluentAssertions;
using NLog;
using NUnit.Framework;

namespace BuildLight.Tests.Unit.Spikes
{
    [TestFixture]
    public class TeamCityApiSpike
    {
        private const string USER_NAME = "";
        private const string PASSWORD = "";
        private const string SERVER = "";
        private Logger logger = LogManager.GetCurrentClassLogger(); 

        [Test]
        [Ignore("Spike")]
        public void GetStatus()
        {
            using (var client = new HttpClient(new WebRequestHandler
                                                   {
                                                       Credentials = new NetworkCredential(USER_NAME, PASSWORD)
                                                   }))
            {
                client.BaseAddress = new Uri(SERVER);
                var response = client.GetStringAsync("/httpAuth/app/rest/cctray/projects.xml").Result;
                logger.Debug("Response from server is {0}", response);

                var status = new BuildStatusCollection(response);
                status.Should().NotBeNull();
            }

        }
    }
}
