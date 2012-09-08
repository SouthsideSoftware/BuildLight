namespace BuildLight.Core
{
    public interface IBuildStatusSource
    {
        IndicatorStatus Status { get; }
    }
}