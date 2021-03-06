﻿using System.Threading;
using DelcomSupport;
using NUnit.Framework;

namespace BuildLight.Tests.Unit.Spikes
{
    [TestFixture]
    public class UsbLightSpike
    {
        DelcomLight light = new DelcomLight();

        [Test]
        [Ignore("Spike")]
        public void ShouldTurnOnLight()
        {
            try
            {
                light.ChangeIndicator(DelcomIndicatorState.SolidGreen);
                Thread.Sleep(2000);
                light.ChangeIndicator(DelcomIndicatorState.FlashingGreen);
                Thread.Sleep(2000);
                light.ChangeIndicator(DelcomIndicatorState.Off);
                Thread.Sleep(1000);
                light.ChangeIndicator(DelcomIndicatorState.SolidRed);
                Thread.Sleep(2000);
                light.ChangeIndicator(DelcomIndicatorState.FlashingRed);
                Thread.Sleep(2000);
                light.ChangeIndicator(DelcomIndicatorState.Off);
                Thread.Sleep(1000);
                light.ChangeIndicator(DelcomIndicatorState.SolidBlue);
                Thread.Sleep(2000);
                light.ChangeIndicator(DelcomIndicatorState.FlashingBlue);
                Thread.Sleep(2000);
            } 
            finally
            {
                light.Dispose();
            }
        }

    }
}
