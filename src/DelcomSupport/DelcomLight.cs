using System.Collections.Generic;
using System.Linq;
using System.Text;
using DelcomSupport.LowLevel;
using NLog;

namespace DelcomSupport
{
    public class DelcomLight : IDelcomLight
    {
        private readonly DelcomHID delcom = new DelcomHID();
        DelcomHID.HidTxPacketStruct txCmd;
        DelcomIndicatorState? state;
        Logger logger = LogManager.GetCurrentClassLogger();

        public DelcomLight()
        {
            delcom.Open();
            ChangeIndicator(DelcomIndicatorState.Off);
        }

        public void ChangeIndicator(DelcomIndicatorState newState)
        {
            if (state != newState)
            {
                logger.Debug("Change Delcom build light to {0}", newState);
                AllOff();
                switch (newState)
                {
                    case DelcomIndicatorState.SolidGreen:
                        GreenOn();
                        break;
                    case DelcomIndicatorState.SolidRed:
                        RedOn();
                        break;
                    case DelcomIndicatorState.SolidBlue:
                        BlueOn();
                        break;
                    case DelcomIndicatorState.FlashingGreen:
                        GreenFlash();
                        break;
                    case DelcomIndicatorState.FlashingRed:
                        RedFlash();
                        break;
                    case DelcomIndicatorState.FlashingBlue:
                        BlueFlash();
                        break;
                }
                state = newState;
            }
        }

        public void Close()
        {
            AllOff();
            delcom.Close();
        }

        protected virtual void Dispose(bool isDiposing)
        {
            if (isDiposing)
            {
                Close();
            }
        }

        public void Dispose()
        {
           Dispose(true);
        }

        private void AllOff()
        {
            RedOff();
            GreenOff();
            BlueOff();
        }

        private void GreenOn()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 1;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd); // always disable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 1;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd);
        }

        private void GreenOff()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 1;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd);  // always disable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 0;
            txCmd.MSBData = 1;
            delcom.SendCommand(txCmd);
        }

        private void GreenFlash()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 0;
            txCmd.MSBData = 1;
            delcom.SendCommand(txCmd);  // enable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 1;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd); // and turn it on
        }

        private void RedOn()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 2;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd); // always disable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 2;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd);
        }

        private void RedOff()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 2;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd);  // always disable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 0;
            txCmd.MSBData = 2;
            delcom.SendCommand(txCmd);
        }

        private void RedFlash()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 0;
            txCmd.MSBData = 2;
            delcom.SendCommand(txCmd);  // enable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 2;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd); // and turn it on
        }

        private void BlueOn()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 2;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd); // always disable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 4;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd);
        }

        private void BlueOff()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 4;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd);  // always disable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 0;
            txCmd.MSBData = 4;
            delcom.SendCommand(txCmd);
        }

        private void BlueFlash()
        {
            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 20;
            txCmd.LSBData = 0;
            txCmd.MSBData = 4;
            delcom.SendCommand(txCmd);  // enable the flash mode 

            txCmd.MajorCmd = 101;
            txCmd.MinorCmd = 12;
            txCmd.LSBData = 4;
            txCmd.MSBData = 0;
            delcom.SendCommand(txCmd); // and turn it on
        }
    }
}
