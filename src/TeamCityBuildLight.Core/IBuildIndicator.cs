namespace TeamCityBuildLight.Core
{
    public interface IBuildIndicator
    {
        void ShowIndicator(IBuildStatusSource buildStatusSource);
    }
}