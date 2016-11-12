using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Frankentime.Domain.Analytics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Frankentime.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAnalytics _analytics;
        private readonly IFrameNavigationService _navigationService;

        public MainViewModel(IAnalytics analytics, IFrameNavigationService navigationService)
        {
            _analytics = analytics;
            _navigationService = navigationService;
            
            

        }

        public RelayCommand WindowLoadedCommand => new RelayCommand(WindowLoaded);


        private void WindowLoaded()
        {
            _navigationService.NavigateTo(ViewName.TimerPage);

            
        }
    }
}
