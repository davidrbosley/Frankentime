using System;
using Frankentime.Test;
using Frankentime.WPF.ViewModel;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Frankentime.AcceptanceTests
{
    [Binding]
    public class GatherTimeSteps : FeaturesTestBase
    {
        readonly DateTime _startTime = new DateTime(2015, 1, 27, 3, 33, 42);
        private readonly TimerViewModel _timerViewModel;

        public GatherTimeSteps()
        {
            TFSysTimeFake.InjectInSystem();
            _timerViewModel = new TimerViewModel(AnalyticsFake);
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
            VerifyTimeDisplay(p0, p1);
        }

        private void VerifyTimeDisplay(int hours, int minutes, int seconds=0, int milliseconds=0)
        {
            Assert.AreEqual(string.Format("{0}:{1}:{2}.{3}", hours.ToString("00"), minutes.ToString("00"), seconds.ToString("00"), milliseconds.ToString("00")), _timerViewModel.TimeGathered,
                Environment.NewLine + "TimeGatheredIncorrect");
        }

        [Given(@"(.*) minutes have elapsed")]
        public void GivenMinutesHaveElapsed(int p0)
        {
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime.AddMinutes(p0));
        }

        [When(@"I trigger the stop mechanism")]
        public void WhenITriggerTheStopMechanism()
        {
            _timerViewModel.StopTimer.Execute(null);
        }

        [Given(@"I have (.*) minutes gathered")]
        public void GivenIHaveMinutesGathered(int p0)
        {
            GatherTime(TimeSpan.FromMinutes(p0));
        }

        private void GatherTime(TimeSpan  timeSpan)
        {
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime);
            _timerViewModel.StartTimer.Execute(null);
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime.Add(timeSpan));
            _timerViewModel.StopTimer.Execute(null);
        }

        [When(@"I trigger the clear mechanism")]
        public void WhenITriggerTheClearMechanism()
        {
            _timerViewModel.ClearTimer.Execute(null);
        }

        [Given(@"I have (.*) minutes and (.*) seconds gathered")]
        public void GivenIHaveMinutesAndSecondsGathered(int p0, int p1)
        {
            GatherTime(TimeSpan.FromSeconds((p0*60) + p1));
        }

        [When(@"I look at the screen")]
        public void WhenILookAtTheScreen()
        {
        }

        [Then(@"the result should be (.*) hours and (.*) minutes and (.*) seconds on the screen")]
        public void ThenTheResultShouldBeHoursAndMinutesAndSecondsOnTheScreen(int p0, int p1, int p2)
        {
            VerifyTimeDisplay(p0, p1, p2);
        }


    }
}
