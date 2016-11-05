namespace Frankentime.Domain.Analytics
{
    public interface IPageAnalytics
    {
        void FeatureUsed(string featureName);
        void End();
    }
}
