using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Frankentime.Domain;

namespace Frankentime.WPF.ViewModel
{
    public class TimerViewModel : ViewModelBase
    {


        private readonly FrankenTimer _frankenTimer;
        private readonly Timer _timer;

        public TimerViewModel()
        {
            _frankenTimer = new FrankenTimer();
            _timer = new Timer();
            _timer.Elapsed += SecondTick;
        }


        public string TimeGathered
        {
            get
            {
                return _frankenTimer.TotalTime.ToString(@"hh\:mm\:ss");
            }
        }

        public ICommand StartTimer { get { return new RelayCommand(StartTimerExecute, CanStartTimerExecute); } }
        public ICommand StopTimer { get { return new RelayCommand(StopTimerExecute, CanStopTimerExecute); } }
        public ICommand ClearTimer { get { return new RelayCommand(ClearTimerExecute, CanClearTimerExecute); } }

        private bool CanStopTimerExecute()
        {
            return _frankenTimer.IsRunning;
        }

        private void StopTimerExecute()
        {
            _frankenTimer.Stop();
            _timer.Stop();
        }

        private bool CanClearTimerExecute()
        {
            return true;
        }

        private void ClearTimerExecute()
        {
            _frankenTimer.Reset();
        }


        private bool CanStartTimerExecute()
        {
            return !_frankenTimer.IsRunning;
        }

        private void StartTimerExecute()
        {
            if (!_frankenTimer.IsRunning)
            {
                _frankenTimer.Start();

                RaiseEventAtNextSecond();
            }
        }

        private void SecondTick(object sender, EventArgs e)
        {
            _timer.Stop();
            OnPropertyChanged("TimeGathered");

            RaiseEventAtNextSecond();
        }

        private void RaiseEventAtNextSecond()
        {
            _timer.Interval = 1001 - _frankenTimer.TotalTime.Milliseconds;
            if (_timer.Interval < 50)
                _timer.Interval = 1050;
            _timer.Start();

        }
    }
}
