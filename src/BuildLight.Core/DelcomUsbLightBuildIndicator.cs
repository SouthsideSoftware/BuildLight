using DelcomSupport;

namespace BuildLight.Core
{
    public class DelcomUsbLightBuildIndicator : IBuildIndicator
    {
        readonly IDelcomLight delcomLight;
        
        public DelcomUsbLightBuildIndicator(IDelcomLight delcomLight)
        {
            this.delcomLight = delcomLight;
        }

        public void ShowIndicator(IBuildStatusSource buildStatusSource){
            switch (buildStatusSource.Status)
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
                    delcomLight.ChangeIndicator(DelcomIndicatorState.FlashingRed);
                    break;
            }
        }

        public void Clear()
        {
            delcomLight.ChangeIndicator(DelcomIndicatorState.Off);
        }
    }
}
