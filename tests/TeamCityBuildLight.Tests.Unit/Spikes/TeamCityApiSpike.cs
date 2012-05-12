using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TeamCityBuildLight.Tests.Unit.Spikes
{
    [TestFixture]
    public class TeamCityApiSpike
    {
        private const string USER_NAME = "user";
        private const string PASSWORD = "password";
        private const string SERVER = "server";

        [Test]
        public void GetStatus()
        {
            using (var client = new HttpClient(new WebRequestHandler
                                                   {
                                                       Credentials = new NetworkCredential(USER_NAME, PASSWORD)
                                                   }))
            {
                client.BaseAddress = new Uri(SERVER);
                var response = client.GetStringAsync("/httpAuth/app/rest/cctray/projects.xml").Result;
                Trace.WriteLine(string.Format("Response from server is {0}", response));

                var doc = XDocument.Parse(response);
                doc.Should().NotBeNull();
            }

        }
    }
}
