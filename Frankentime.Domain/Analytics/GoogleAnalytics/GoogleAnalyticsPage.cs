using Garlic;

namespace Frankentime.Domain.Analytics.GoogleAnalytics
{
    public class GoogleAnalyticsPage : IPageAnalytics
    {
        private readonly string _pageName;

        private readonly IAnalyticsPageViewRequest _page;

        public GoogleAnalyticsPage(AnalyticsSession analyticsSession, string pageName)
        {
            _pageName = pageName;

            _page = analyticsSession.CreatePageViewRequest(pageName, pageName);
        }

        public void FeatureUsed(string featureName)
        {
            _page.SendEvent(_pageName, featureName, featureName, "1");
        }

        public void End()
        {
            _page.Send();
        }
    }
}
