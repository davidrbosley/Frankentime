namespace Frankentime.Domain.Analytics
{
    public interface IAnalytics
    {
        void ApplicationStart();
        void ApplicationEnd();

        void FeatureUsed(string featureName);
        IPageAnalytics StartPage(string pageName);
    }
}
