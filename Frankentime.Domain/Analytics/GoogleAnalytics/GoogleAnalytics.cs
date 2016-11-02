using Garlic;

namespace Frankentime.Domain.Analytics.GoogleAnalytics
{
    public class GoogleAnalytics : IAnalytics
    {
        //private const string Domain = "Frankentime";
        //private const string GaCode = "UA-59502936-1";


        private const string Domain = "www.FrankentimeBoz42.org";
        private const string GaCode = "UA-60850553-1";

        private AnalyticsSession _analyticsSession;

        private IAnalyticsPageViewRequest _page;

        private ITiming _apptimer;

        public void ApplicationStart()
        {
            if (_analyticsSession == null)
                _analyticsSession = new AnalyticsSession(Domain, GaCode);

            _page = _analyticsSession.CreatePageViewRequest(
                "/",          // path
                "Main"); // page title

            _apptimer = _page.StartTiming("Application", "WPF", "ExecutionTime");

            _page.SendEvent("Application", "AppStart", string.Empty, "1");

            _page.Send();

        }

        public void ApplicationEnd()
        {
            _apptimer.Finish();
        }

        public void FeatureUsed(string featureName)
        {
            _page.SendEvent("Main", featureName, string.Empty, "1");
        }

        public IPageAnalytics StartPage(string pageName)
        {
            return new GoogleAnalyticsPage(_analyticsSession, pageName);
        }
    }
}
