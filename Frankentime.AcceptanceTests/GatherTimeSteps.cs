using System;
using Frankentime.Domain;
using Frankentime.Test;
using Frankentime.WPF.ViewModel;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Frankentime.AcceptanceTests
{
    [Binding]
    public class GatherTimeSteps
    {
        readonly DateTime _startTime = new DateTime(2015, 1, 27, 3, 33, 42);
        private readonly TimerViewModel _timerViewModel;

        public GatherTimeSteps()
        {
            TFSysTimeFake.InjectInSystem();
            _timerViewModel = new TimerViewModel();
        }

        [Given(@"I have triggered the start mechanism")]
        public void GivenIHaveTriggeredTheStartMechanism()
        {
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime);
            _timerViewModel.StartTimer.Execute(null);
        }

        [When(@"(.*) minutes have elapsed")]
        public void WhenMinutesHaveElapsed(int p0)
        {
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime.AddMinutes(p0));
        }

        [Then(@"the result should be (.*) hours and (.*) minutes on the screen")]
        public void ThenTheResultShouldBeHoursAndMinutesOnTheScreen(int p0, int p1)
        {
            
            Assert.AreEqual(string.Format("{0}:{1}:00", p0.ToString("00"), p1.ToString("00")), _timerViewModel.TimeGathered,
                Environment.NewLine + "TimeGatheredIncorrect");
        }
    }
}
