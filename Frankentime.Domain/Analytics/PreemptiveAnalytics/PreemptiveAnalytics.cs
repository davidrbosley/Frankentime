using PreEmptive.Analytics.Common;
using PreEmptive.Analytics.NET;

namespace Frankentime.Domain.Analytics.PreemptiveAnalytics
{
    public class PreemptiveAnalytics : IAnalytics
    {
        private class PreemptiveLogger : ILogger
        {
            public void LogError(string message)
            {
            }

            public void LogWarning(string message)
            {
            }

            public void LogInfo(string message)
            {
            }
        }

        private const string CompanyId = "92D63F36-7F8D-41B9-B7AB-5318AD1641C8";
        private const string ApplicationId = "84FDF30C-D9B9-43FF-AAFE-880CF849C5EE";

        private PAClient _paClient;

        public void ApplicationStart()
        {
            var config = new Configuration(CompanyId, ApplicationId);

            config.CompanyName = "FrankenTime";
            config.ApplicationName = "FrankenTime WPF";
            config.ApplicationType = "WPF";
            config.ApplicationVersion = "1.0";
            config.Endpoint = "message.runtimeintelligence.com/PreEmptive.Web.Services.Messaging/MessagingServiceV2.asmx";
            config.UseSSL = false;
            config.FullData = false;
            config.OmitPersonalInfo = true;
            config.SupportOfflineStorage = true;

            _paClient = new PAClient(config, new PreemptiveLogger());

            _paClient.ApplicationStart();

            _paClient.SystemProfile();
        }

        public void ApplicationEnd()
        {
            if (_paClient != null)
                _paClient.ApplicationStop();
        }

        public void FeatureUsed(string featureName)
        {
            _paClient.FeatureTick(featureName);
        }

        public IPageAnalytics StartPage(string pageName)
        {
            return new PreemptiveAnalyticsPage(_paClient, pageName);
        }
    }
}
