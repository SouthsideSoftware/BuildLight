using System;
using System.Collections.Generic;
using BuildLight.Core;
using BuildLight.Core.CodeContracts;
using FluentAssertions;
using NUnit.Framework;

namespace BuildLight.Tests.Unit.Core
{
    [TestFixture]
    public class BuildStatusCollectionTests
    {
        [Test]
        public void ShouldBeAbleToLoadValidXmlFromTeamCity()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            Action act = () => new BuildStatusCollection(teamCityData);
            act.ShouldNotThrow();
        }

        [Test]
        public void ShouldThrowPreconditionExceptionIfPassedNullString()
        {
            Action act = () => new BuildStatusCollection(null);
            act.ShouldThrow<PreconditionException>();
        }

        [Test]
        public void ShouldThrowPreconditionExceptionIfPassedEmptyString()
        {
            Action act = () => new BuildStatusCollection("");
            act.ShouldThrow<PreconditionException>();
        }

        [Test]
        public void ShouldThrowPreconditionExceptionIfPassedStringOnlyWhitepspace()
        {
            Action act = () => new BuildStatusCollection("\t  \t\t");
            act.ShouldThrow<PreconditionException>();
        }

        [Test]
        public void ShouldHaveItemsInCollectionWhenPassedValidXmlFromTeamCity()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData).Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldHaveKnownStatusWhenPassedAtLeastOneProjectElementFromTeamCity()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData).Status.Should().NotBe(IndicatorStatus.Unknown);
        }

        [Test]
        public void ShouldHaveUnknownStatusWhenPassedNoProjectElementsFromTeamCity()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData).Status.Should().Be(IndicatorStatus.Unknown);
        }

        [Test]
        public void ShouldHaveUnknownStatusWhenPassedInvalidXmlFromTeamCity()
        {
            string teamCityData = "8dhd";

            new BuildStatusCollection(teamCityData).Status.Should().Be(IndicatorStatus.Unknown);
        }

        [Test]
        public void ShouldHaveStatusBuildingIfAnyProjectIsBuilding()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "<Project activity=\"Building\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData).Status.Should().Be(IndicatorStatus.Building);
        }

        [Test]
        public void ShouldHaveStatusFailureIfNoProjectBuildingAndAtLeastOneProjectFailure()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Failure\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "<Project activity=\"Sleeping\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData).Status.Should().Be(IndicatorStatus.Failure);
        }

        [Test]
        public void ShouldHaveStatusFailureIfNoProjectBuildingAndAtLeastOneProjectFailureAndConfigurationSpecifiesTheFailingConfigurationShouldBeChecked()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Failure\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "<Project activity=\"Sleeping\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Release\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData, new List<string>{"BuildLight :: Debug"}).Status.Should().Be(IndicatorStatus.Failure);
        }

        [Test]
        public void ShouldHaveStatusSuccessIfNoProjectBuildingAndAtLeastOneProjectFailureAndConfigurationDoesNotSpecifyTheFailingConfigurationShouldBeChecked()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Failure\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "<Project activity=\"Sleeping\" lastBuildLabel=\"6\" lastBuildStatus=\"Success\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Release\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData, new List<string> { "BuildLight :: Release" }).Status.Should().Be(IndicatorStatus.Success);
        }


        [Test]
        public void ShouldHaveStatusSuccessIfNoProjectBuildingAndAllProjectsShowSuccess()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Status\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "<Project activity=\"Sleeping\" lastBuildLabel=\"6\" lastBuildStatus=\"Status\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData).Status.Should().Be(IndicatorStatus.Success);
        }

        [Test]
        public void ShouldHaveStatusBuildingIfAtLeastOneProjectBuildingEventIfAndAtLeastOneProjectFailure()
        {
            string teamCityData =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><Projects>" +
                "<Project activity=\"Has pending changes\" lastBuildLabel=\"6\" lastBuildStatus=\"Failure\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "<Project activity=\"Building\" lastBuildLabel=\"6\" lastBuildStatus=\"Failure\" " +
                "lastBuildTime=\"2012-05-12T10:51:45.098-05:00\" name=\"BuildLight :: Debug\" webUrl=\"http://localhost:81/viewType.html?buildTypeId=bt2\"/>" +
                "</Projects>";

            new BuildStatusCollection(teamCityData).Status.Should().Be(IndicatorStatus.Building);
        }
    }
}
