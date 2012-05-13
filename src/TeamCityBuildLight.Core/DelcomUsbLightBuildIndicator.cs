using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DelcomSupport;

namespace TeamCityBuildLight.Core
{
    public class DelcomUsbLightBuildIndicator : IBuildIndicator
    {
        readonly IDelcomLight delcomLight;

        public DelcomUsbLightBuildIndicator(IDelcomLight delcomLight)
        {
            this.delcomLight = delcomLight;
        }

        public void ShowIndicator(BuildStatusCollection buildStatusCollection){
            switch (buildStatusCollection.Status)
            {
                case IndicatorStatus.Unknown:
                    delcomLight.ChangeIndicator(DelcomIndicatorState.Off);
                    break;
                case IndicatorStatus.Success:
                    delcomLight.ChangeIndicator(DelcomIndicatorState.SolidGreen);
                    break;
                case IndicatorStatus.Building:
                    delcomLight.ChangeIndicator(DelcomIndicatorState.FlashingBlue);
                    break;
                case IndicatorStatus.Failure:
                    delcomLight.ChangeIndicator(DelcomIndicatorState.SolidRed);
                    break;
            }
        }
    }
}
