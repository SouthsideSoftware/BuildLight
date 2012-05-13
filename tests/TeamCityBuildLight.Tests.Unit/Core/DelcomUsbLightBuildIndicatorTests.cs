using DelcomSupport;
using NUnit.Framework;
using Rhino.Mocks;
using TeamCityBuildLight.Core;

namespace TeamCityBuildLight.Tests.Unit.Core
{
    [TestFixture]
    public class DelcomUsbLightBuildIndicatorTests
    {
        [Test]
        public void ShouldFlashRedWhenStatusIsFailure()
        {
            PerformIndicatorTest(IndicatorStatus.Failure, DelcomIndicatorState.FlashingRed);
        }

        [Test]
        public void ShouldFlashBlueWhenStatusIsBuilding()
        {
            PerformIndicatorTest(IndicatorStatus.Building, DelcomIndicatorState.FlashingBlue);
        }

        [Test]
        public void ShouldBeGreenWhenStatusIsSuccess()
        {
            PerformIndicatorTest(IndicatorStatus.Success, DelcomIndicatorState.SolidGreen);
        }

        [Test]
        public void ShouldTurnLightOffWhenStatusUnknown()
        {
            PerformIndicatorTest(IndicatorStatus.Unknown, DelcomIndicatorState.Off);
        }

        static void PerformIndicatorTest(IndicatorStatus indicatorStatus, DelcomIndicatorState expectedIndicatorState)
        {
            //arrange
            var mockLight = MockRepository.GenerateMock<IDelcomLight>();
            mockLight.Expect(m => m.ChangeIndicator(expectedIndicatorState)).Repeat.Once();

            var stubStatusSource = MockRepository.GenerateStub<IBuildStatusSource>();
            stubStatusSource.Stub(s => s.Status).Return(indicatorStatus);

            var indicator = new DelcomUsbLightBuildIndicator(mockLight);

            //act
            indicator.ShowIndicator(stubStatusSource);

            //assert
            mockLight.VerifyAllExpectations();
        }
    }
}