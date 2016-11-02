using System.Collections.Generic;

namespace Frankentime.Domain.Analytics
{
    public class AnalyticsMultiplexor : IAnalytics
    {
        private readonly List<IAnalytics> _analytics;

        public AnalyticsMultiplexor()
        {
            _analytics = new List<IAnalytics>(10)
            {
                new GoogleAnalytics.GoogleAnalytics(), 
                new PreemptiveAnalytics.PreemptiveAnalytics()
            };
        }


        public void ApplicationStart()
        {
            foreach (var analytic in _analytics)
            {
                analytic.ApplicationStart();
            }
        }

        public void ApplicationEnd()
        {
            foreach (var analytic in _analytics)
            {
                analytic.ApplicationEnd();
            }

        }

        public void FeatureUsed(string featureName)
        {
            foreach (var analytic in _analytics)
            {
                analytic.FeatureUsed(featureName);
            }
        }

        public IPageAnalytics StartPage(string pageName)
        {
            return new AnalyticsPageMultiplexor(_analytics, pageName);
        }
    }
}
