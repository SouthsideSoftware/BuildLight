using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using HIDIOWINCS;
using NUnit.Framework;
using FluentAssertions;

namespace TeamCityBuildLight.Tests.Unit.Spikes
{
    [TestFixture]
    public class UsbLightSpike
    {
        private DelcomHID Delcom = new DelcomHID();   // declare the Delcom class
        private DelcomHID.HidTxPacketStruct TxCmd;

        [Test]
        public void ShouldTurnOnLight()
        {

            Delcom.Open();
            try
            {
                AllOff();

                GreenOn();
                Thread.Sleep(3000);
                GreenFlash();
                Thread.Sleep(3000);
                GreenOff();
                Thread.Sleep(3000);
                RedOn();
                Thread.Sleep(3000);
                RedFlash();
                Thread.Sleep(3000);
                RedOff();
                Thread.Sleep(3000);
                BlueOn();
                Thread.Sleep(3000);
                BlueFlash();
                Thread.Sleep(3000);
                BlueOff();
                Thread.Sleep(3000);
            } 
            finally
            {
                AllOff();
                Delcom.Close();
            }
        }

        private void AllOff()
        {
            RedOff();
            GreenOff();
            BlueOff();
        }

        private void GreenOn()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 1;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd); // always disable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 1;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd);
        }

        private void GreenOff()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 1;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd);  // always disable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 0;
            TxCmd.MSBData = 1;
            Delcom.SendCommand(TxCmd);
        }

        private void GreenFlash()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 0;
            TxCmd.MSBData = 1;
            Delcom.SendCommand(TxCmd);  // enable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 1;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd); // and turn it on
        }

        private void RedOn()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 2;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd); // always disable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 2;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd);
        }

        private void RedOff()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 2;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd);  // always disable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 0;
            TxCmd.MSBData = 2;
            Delcom.SendCommand(TxCmd);
        }

        private void RedFlash()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 0;
            TxCmd.MSBData = 2;
            Delcom.SendCommand(TxCmd);  // enable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 2;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd); // and turn it on
        }

        private void BlueOn()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 2;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd); // always disable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 4;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd);
        }

        private void BlueOff()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 4;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd);  // always disable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 0;
            TxCmd.MSBData = 4;
            Delcom.SendCommand(TxCmd);
        }

        private void BlueFlash()
        {
            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 20;
            TxCmd.LSBData = 0;
            TxCmd.MSBData = 4;
            Delcom.SendCommand(TxCmd);  // enable the flash mode 

            TxCmd.MajorCmd = 101;
            TxCmd.MinorCmd = 12;
            TxCmd.LSBData = 4;
            TxCmd.MSBData = 0;
            Delcom.SendCommand(TxCmd); // and turn it on
        }

        private void Open()
        {
            if (Delcom.Open() == 0)
            {
                UInt32 SerialNumber, Version, Date, Month, Year;
                SerialNumber = Version = Date = Month = Year = 0;
                Delcom.GetDeviceInfo(ref SerialNumber, ref Version, ref Date, ref Month, ref Year);
                Year += 2000;
            }
        }

        private void Close()
        {
            Delcom.Close();
        }
    }
}
