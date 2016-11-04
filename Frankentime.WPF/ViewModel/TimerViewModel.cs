using System;
using System.Timers;
using System.Windows.Input;
using Frankentime.Domain;
using Frankentime.Domain.Analytics;

namespace Frankentime.WPF.ViewModel
{
    public class TimerViewModel : ViewModelBase
    {
        private readonly FrankenTimer _frankenTimer;
        private readonly Timer _timer;

        private bool _showTime = true;
        private readonly float _hourlyRate = 91.60f;

        // Dependency Injection via Constructor
        public TimerViewModel(IAnalytics analytics)
            : base(analytics)
        {
            _frankenTimer = new FrankenTimer();
            _timer = new Timer();
            _timer.Elapsed += SecondTick;

            Analytics?.ApplicationStart();
        }
        

        public string TimeGathered
        {
            get
            {
                return _showTime ? _frankenTimer.TotalTime.ToString(@"hh\:mm\:ss\.ff") : (_frankenTimer.TotalTime.TotalHours*_hourlyRate).ToString("C");
            }
        }

        public ICommand StartTimer => new RelayCommand(StartTimerExecute, CanStartTimerExecute);
        public ICommand StopTimer => new RelayCommand(StopTimerExecute, CanStopTimerExecute);
        public ICommand ClearTimer => new RelayCommand(ClearTimerExecute, CanClearTimerExecute);
        public ICommand PushTimer => new RelayCommand(PushTimerExecute, CanPushTimerExecute);
        public ICommand ExitApplication => new RelayCommand(ExitApplicationExecute, CanExitApplicationExecute);

        public ICommand Subtract30 => new RelayCommand(Subtract30Execute, CanSubtract30Execute);
        public ICommand Subtract5 => new RelayCommand(Subtract5Execute, CanSubtract5Execute);
        public ICommand Subtract15 => new RelayCommand(Subtract15Execute, CanSubtract15Execute);

        public ICommand Add30 => new RelayCommand(Add30Execute, CanAlways);
        public ICommand Add5 => new RelayCommand(Add5Execute, CanAlways);
        public ICommand Add15 => new RelayCommand(Add15Execute, CanAlways);

        public ICommand SwitchDisplay => new RelayCommand(SwitchDisplayExecute, CanAlways);

        private void SwitchDisplayExecute()
        {
            _showTime = !_showTime;
            OnPropertyChanged("TimeGathered");
        }

        private bool CanAlways()
        {
            return true;
        }

        private void Add30Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(30));
            OnPropertyChanged("TimeGathered");
        }

        private void Add5Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(5));
            OnPropertyChanged("TimeGathered");
        }

        private void Add15Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(15));
            OnPropertyChanged("TimeGathered");
        }

        private bool CanSubtract30Execute()
        {
            return _frankenTimer.TotalTime >= TimeSpan.FromMinutes(30);
        }

        private void Subtract30Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(-30));
            OnPropertyChanged("TimeGathered");

        }

        private bool CanSubtract5Execute()
        {
            return _frankenTimer.TotalTime >= TimeSpan.FromMinutes(5);
        }

        private void Subtract5Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(-5));
            OnPropertyChanged("TimeGathered");

        }
        private bool CanSubtract15Execute()
        {
            return _frankenTimer.TotalTime >= TimeSpan.FromMinutes(15);
        }

        private void Subtract15Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(-15));
            OnPropertyChanged("TimeGathered");
        }

        private bool CanExitApplicationExecute()
        {
            return true;
        }

        private void ExitApplicationExecute()
        {
            
        }

        private bool CanPushTimerExecute()
        {            
            return true;
        }

        private void PushTimerExecute()
        {
            Analytics?.FeatureUsed("PushTimer");
            if (_frankenTimer.IsRunning)
            {
                _frankenTimer.Stop();
                _timer.Stop();
            }
            else
            {
                _frankenTimer.Start();
                _timer.Start();
            }
        }

        private bool CanStopTimerExecute()
        {
            return _frankenTimer.IsRunning;
        }

        private void StopTimerExecute()
        {
            _frankenTimer.Stop();
            _timer.Stop();

            Analytics?.FeatureUsed("StopTimer");
        }

        private bool CanClearTimerExecute()
        {
            return true;
        }

        private void ClearTimerExecute()
        {
            _frankenTimer.Reset();
            OnPropertyChanged("TimeGathered");
            Analytics?.FeatureUsed("ClearTimer");
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

            Analytics?.FeatureUsed("StartTimer");
        }

        private void SecondTick(object sender, EventArgs e)
        {
            _timer.Stop();
            OnPropertyChanged("TimeGathered");

            RaiseEventAtNextSecond();
        }

        private void RaiseEventAtNextSecond()
        {
            //_timer.Interval = 1001 - _frankenTimer.TotalTime.Milliseconds;
            //if (_timer.Interval < 50)
            //    _timer.Interval = 1050;
            _timer.Interval = 10;
            _timer.Start();

        }
    }
}
