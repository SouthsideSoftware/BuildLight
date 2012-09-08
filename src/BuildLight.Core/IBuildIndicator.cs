namespace BuildLight.Core
{
    public interface IBuildIndicator
    {
        void ShowIndicator(IBuildStatusSource buildStatusSource);
        void Clear();
    }
}