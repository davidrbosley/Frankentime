using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Frankentime.Domain;
using Frankentime.Domain.Analytics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Squirrel;
// ReSharper disable ExplicitCallerInfoArgument

namespace Frankentime.WPF.ViewModel
{
    public class TimerViewModel : ViewModelBase
    {
        private readonly FrankenTimer _frankenTimer;
        private readonly Timer _timer;

        private bool _showTime = true;
        private readonly float _hourlyRate = 91.60f;

        private readonly IAnalytics _analytics;


        public TimerViewModel(IAnalytics analytics)
        {
            _frankenTimer = new FrankenTimer();
            _timer = new Timer();
            _timer.Elapsed += SecondTick;

            _analytics = analytics;

            _analytics?.ApplicationStart();
            
        }


        public string TimeGathered => _showTime ? _frankenTimer.TotalTime.ToString(@"hh\:mm\:ss\.ff") : (_frankenTimer.TotalTime.TotalHours*_hourlyRate).ToString("C");

        public ICommand StartTimer => new RelayCommand(StartTimerExecute, CanStartTimerExecute);
        public ICommand StopTimer => new RelayCommand(StopTimerExecute, CanStopTimerExecute);
        public ICommand ClearTimer => new RelayCommand(ClearTimerExecute, CanClearTimerExecute);
        public ICommand PushTimer => new RelayCommand(PushTimerExecute, CanPushTimerExecute);
        public RelayCommand<Window> CloseWindowCommand => new RelayCommand<Window>(CloseWindow);

        public ICommand Subtract30 => new RelayCommand(Subtract30Execute, CanSubtract30Execute);
        public ICommand Subtract5 => new RelayCommand(Subtract5Execute, CanSubtract5Execute);
        public ICommand Subtract15 => new RelayCommand(Subtract15Execute, CanSubtract15Execute);

        public ICommand Add30 => new RelayCommand(Add30Execute, CanAlways);
        public ICommand Add5 => new RelayCommand(Add5Execute, CanAlways);
        public ICommand Add15 => new RelayCommand(Add15Execute, CanAlways);

        public ICommand DisplayTime => new RelayCommand(SwitchDisplayTime, CanAlways);
        public ICommand DisplayMoney => new RelayCommand(SwitchDisplayMoney, CanAlways);
        public ICommand CheckUpdates => new RelayCommand(CheckForFrankenUpdates, CanAlways);

        private void SwitchDisplayMoney()
        {
            _showTime = false;
            RaisePropertyChanged("TimeGathered");
        }

        private void SwitchDisplayTime()
        {
            _showTime = true;
            RaisePropertyChanged("TimeGathered");
        }


        private void CheckForFrankenUpdates()
        {
            //_navigationService.NavigateTo(ViewName.CheckForUpdates);
        }


        private void CloseWindow(Window window)
        {

            window?.Close();
        }

        private bool CanAlways()
        {
            return true;
        }

        private void Add30Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(30));
            RaisePropertyChanged("TimeGathered");
        }

        private void Add5Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(5));
            RaisePropertyChanged("TimeGathered");
        }

        private void Add15Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(15));
            RaisePropertyChanged("TimeGathered");
        }

        private bool CanSubtract30Execute()
        {
            return _frankenTimer.TotalTime >= TimeSpan.FromMinutes(30);
        }

        private void Subtract30Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(-30));
            RaisePropertyChanged("TimeGathered");

        }

        private bool CanSubtract5Execute()
        {
            return _frankenTimer.TotalTime >= TimeSpan.FromMinutes(5);
        }

        private void Subtract5Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(-5));
            RaisePropertyChanged("TimeGathered");

        }
        private bool CanSubtract15Execute()
        {
            return _frankenTimer.TotalTime >= TimeSpan.FromMinutes(15);
        }

        private void Subtract15Execute()
        {
            _frankenTimer.AdjustTime(TimeSpan.FromMinutes(-15));
            RaisePropertyChanged("TimeGathered");
        }

        private bool CanPushTimerExecute()
        {            
            return true;
        }

        private void PushTimerExecute()
        {
            _analytics?.FeatureUsed("PushTimer");
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

            _analytics?.FeatureUsed("StopTimer");
        }

        private bool CanClearTimerExecute()
        {
            return true;
        }

        private void ClearTimerExecute()
        {
            _frankenTimer.Reset();
            RaisePropertyChanged("TimeGathered");
            _analytics?.FeatureUsed("ClearTimer");
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

            _analytics?.FeatureUsed("StartTimer");
        }

        private void SecondTick(object sender, EventArgs e)
        {
            _timer.Stop();
            RaisePropertyChanged("TimeGathered");

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
