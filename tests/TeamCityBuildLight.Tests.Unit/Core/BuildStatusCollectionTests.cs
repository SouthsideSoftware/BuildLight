using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using TeamCityBuildLight.Core;
using TeamCityBuildLight.Core.CodeContracts;

namespace TeamCityBuildLight.Tests.Unit.Core
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
    }
}
