namespace TeamCityBuildLight.Core
{
    public interface IBuildStatusChecker
    {
        BuildStatusCollection Check();
    }
}