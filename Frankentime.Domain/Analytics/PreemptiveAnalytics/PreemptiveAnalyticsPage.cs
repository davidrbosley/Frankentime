using PreEmptive.Analytics.NET;

namespace Frankentime.Domain.Analytics.PreemptiveAnalytics
{
    public class PreemptiveAnalyticsPage : IPageAnalytics
    {
        private readonly PAClient _paClient;

        private readonly string _pageName;

        public PreemptiveAnalyticsPage(PAClient paClient, string pageName)
        {
            _paClient = paClient;
            _pageName = pageName;
        }

        public void FeatureUsed(string featureName)
        {
            _paClient.FeatureTick(_pageName + "." + featureName);
        }

        public void End()
        {
        }
    }
}
