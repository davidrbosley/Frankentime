using Frankentime.WPF.ViewModel;
using NUnit.Framework;

namespace Frankentime.WPF.UnitTests.ViewModelTests
{
    [TestFixture]
    public class TimerViewModelTests : TimerViewBase
    {
        [Test]
        public void Construction_SendsApplicationStartAnalytic()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new TimerViewModel(AnalyticsFake);

            AnalyticsFake.VerifyCallCount("ApplicationStart", 1);
        }

        [Test]
        public void StartTimer_SendsAnalyticFeatureUsed()
        {
            var timerViewModel = new TimerViewModel(AnalyticsFake);

            timerViewModel.StartTimer.Execute(null);

            AnalyticsFake.VerifyFeatureUsedCallCount("StartTimer", 1);
        }

        [Test]
        public void StopTimer_SendsAnalyticFeatureUsed()
        {
            var timerViewModel = new TimerViewModel(AnalyticsFake);

            timerViewModel.StopTimer.Execute(null);

            AnalyticsFake.VerifyFeatureUsedCallCount("StopTimer", 1);
        }

        [Test]
        public void ClearTimer_SendsAnalyticFeatureUsed()
        {
            var timerViewModel = new TimerViewModel(AnalyticsFake);

            timerViewModel.ClearTimer.Execute(null);

            AnalyticsFake.VerifyFeatureUsedCallCount("ClearTimer", 1);
        }

    }
}
