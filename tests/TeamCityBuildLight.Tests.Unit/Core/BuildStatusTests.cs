using System;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;
using TeamCityBuildLight.Core;
using TeamCityBuildLight.Core.CodeContracts;

namespace TeamCityBuildLight.Tests.Unit.Core
{
    [TestFixture]
    public class BuildStatusTests
    {
        private const string TEAM_CITY_DATA =
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>";
        [Test]
        public void ShouldBeAbleToLoadValidXmlFromTeamCity()
        {
            var teamCityElement = XElement.Parse(TEAM_CITY_DATA);
            Action act = () => new BuildStatus(teamCityElement);
            act.ShouldNotThrow();
        }

        [Test]
        public void ShouldBeAbleToGetBuildStatusFromTeamCity()
        {
            var teamCityElement = XElement.Parse(TEAM_CITY_DATA);
            var status = new BuildStatus(teamCityElement);
            status.LastBuildStatus.Should().Be("Success");
        }

        [Test]
        public void ShouldBeAbleToGetProjectFromTeamCity()
        {
            var teamCityElement = XElement.Parse(TEAM_CITY_DATA);
            var status = new BuildStatus(teamCityElement);
            status.Project.Should().Be("BuildLight");
        }

        [Test]
        public void ShouldBeAbleToGetConfigurationFromTeamCity()
        {
            var teamCityElement = XElement.Parse(TEAM_CITY_DATA);
            var status = new BuildStatus(teamCityElement);
            status.Configuration.Should().Be("Debug");
        }

        [Test]
        public void ShouldBeAbleToGetActivityFromTeamCity()
        {
            var teamCityElement = XElement.Parse(TEAM_CITY_DATA);
            var status = new BuildStatus(teamCityElement);
            status.Activity.Should().Be("Has pending changes");
        }

        [Test]
        public void ShouldThrowPreconditionExceptionIfPassedNullElement()
        {
            Action act = () => new BuildStatus(null);
            act.ShouldThrow<PreconditionException>();
        }
    }
}