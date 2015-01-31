using System;
using NUnit.Framework;
using Frankentime.Test;

namespace Frankentime.Domain.UnitTests.FrankenTimerTests
{
    [TestFixture]
    public class BasicTests
    {
        private FrankenTimer _timer;
        private DateTime _startTime1 = new DateTime(2015,1,25,3,27,8);

        [SetUp]
        public void SetUp()
        {
            _timer = new FrankenTimer();
            TFSysTimeFake.InjectInSystem();
        }

        [Test]
        public void NeverStarted_ReturnsZero()
        {
            VerifyTotalTime(TimeSpan.Zero, _timer.TotalTime);
        }

        [Test]
        public void Start_Stop_TotalTimeCorrect()
        {
            StartAndStopTimerForMinutes(42);

            VerifyTotalTime(TimeSpan.FromMinutes(42), _timer.TotalTime);
        }

        [Test]
        public void StopCalledFirst_ReturnsZero()
        {
            _timer.Stop();
            VerifyTotalTime(TimeSpan.Zero, _timer.TotalTime);
        }

        [Test]
        public void StartStopMultipleTimes_AggregatesAll()
        {
            StartAndStopTimerForMinutes(42);
            StartAndStopTimerForMinutes(10);
            StartAndStopTimerForMinutes(1);

            VerifyTotalTime(TimeSpan.FromMinutes(42+10+1), _timer.TotalTime);
        }

        [Test]
        public void StartStopStop_TotalTimeCorrect()
        {
            StartAndStopTimerForMinutes(42);
            _timer.Stop();

            VerifyTotalTime(TimeSpan.FromMinutes(42), _timer.TotalTime);
        }

        [Test]
        public void StartStartStop_SecondStartIgnored()
        {
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime1, _startTime1.AddMinutes(42));
            _timer.Start();
            _timer.Start();
            _timer.Stop();

            VerifyTotalTime(TimeSpan.FromMinutes(42), _timer.TotalTime);
        }

        [Test]
        public void StartedButNotStopped_TotalTimeIncludesRunningTimer()
        {
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime1, _startTime1.AddMinutes(42));
            _timer.Start();

            VerifyTotalTime(TimeSpan.FromMinutes(42), _timer.TotalTime);
        }

        [Test]
        public void StartStopStartedAndRuning_CalculatedCorrectly()
        {
            StartAndStopTimerForMinutes(42);
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime1, _startTime1.AddMinutes(2));
            _timer.Start();

            VerifyTotalTime(TimeSpan.FromMinutes(42+2), _timer.TotalTime);
        }

        [Test]
        public void ToString_ShowsTotalTime()
        {
            StartAndStopTimerForMinutes(123);

            Assert.AreEqual("02:03:00", _timer.ToString(), Environment.NewLine + "Incorrect ToString()");
        }

        [Test]
        public void ToString_AtZero_Correct()
        {
            Assert.AreEqual("00:00:00", _timer.ToString(), Environment.NewLine + "Incorrect ToString");
        }

        [Test]
        public void Reset_TimerStopped_ClearsTime()
        {
            PutMinutesOnTimer(42);
            _timer.Reset();
            VerifyTotalTime(TimeSpan.Zero, _timer.TotalTime);
        }
        [Test]
        public void Reset_TimerRunning_RestartsFromZero()
        {
            StartAndStopTimerForMinutes(42);

            _startTime1 = _startTime1.AddMinutes(44);
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime1, _startTime1.AddMinutes(30), _startTime1.AddMinutes(40));
            _timer.Start();

            _timer.Reset();

            VerifyTotalTime(TimeSpan.FromMinutes(10), _timer.TotalTime);
        }

        private void PutMinutesOnTimer(int minutes)
        {
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime1, _startTime1.AddMinutes(minutes));
            _timer.Start();
            _timer.Stop();
        }

        private void VerifyTotalTime(TimeSpan expectedTime, TimeSpan actualTime)
        {
            Assert.AreEqual(expectedTime, actualTime, Environment.NewLine + "Incorrect Totaltime");
        }


        private void StartAndStopTimerForMinutes(int numberOfMinutes)
        {
            TFSysTimeFake.Instance.ReturnTimeOf(_startTime1, _startTime1.AddMinutes(numberOfMinutes));
            _timer.Start();
            _timer.Stop();

            _startTime1 = _startTime1.AddMinutes(numberOfMinutes + 1);
        }


    }
}
