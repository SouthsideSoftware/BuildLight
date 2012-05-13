namespace TeamCityBuildLight.Core
{
    public interface IBuildStatusSource
    {
        IndicatorStatus Status { get; }
    }
}