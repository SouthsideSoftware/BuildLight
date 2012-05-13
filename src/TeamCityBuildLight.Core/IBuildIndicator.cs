namespace TeamCityBuildLight.Core
{
    public interface IBuildIndicator
    {
        void ShowIndicator(BuildStatusCollection buildStatusCollection);
    }
}