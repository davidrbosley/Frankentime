using System.Collections.Generic;

namespace Frankentime.Domain.Analytics
{
    public class AnalyticsPageMultiplexor : IPageAnalytics
    {
        private readonly List<IPageAnalytics> _pageAnalytics;


        public AnalyticsPageMultiplexor(List<IAnalytics> analytics, string pageName)
        {
            _pageAnalytics = new List<IPageAnalytics>();

            foreach (var analytic in analytics)
            {
                _pageAnalytics.Add(analytic.StartPage(pageName));
            }
        }


        public void FeatureUsed(string featureName)
        {
            foreach (var pageAnalytic in _pageAnalytics)
            {
                pageAnalytic.FeatureUsed(featureName);
            }
        }

        public void End()
        {
            foreach (var pageAnalytic in _pageAnalytics)
            {
                pageAnalytic.End();
            }

        }
    }
}
