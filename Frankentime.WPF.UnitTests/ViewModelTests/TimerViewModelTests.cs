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
            var timerViewModel = new TimerViewModel(AnalyticsFake);

            AnalyticsFake.VerifyCallCount("ApplicationStart", 1);
        }



    }
}
