/*
  In App.xaml:
  <Application.Resources>
      <vm:FrankenLocator xmlns:vm="clr-namespace:Frankentime.WPF"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System;
using Frankentime.Domain.Analytics;
using Frankentime.WPF.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Frankentime.WPF
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class FrankenLocator
    {
        /// <summary>
        /// Initializes a new instance of the FrankenLocator class.
        /// </summary>
        public FrankenLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //SetupNavigation();

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            //SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IAnalytics, AnalyticsMultiplexor>();
            SimpleIoc.Default.Register<TimerViewModel>();
            SimpleIoc.Default.Register<CheckForUpdatesViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();


        }

     
        public TimerViewModel TimerView => ServiceLocator.Current.GetInstance<TimerViewModel>();
        public CheckForUpdatesViewModel CheckForUpdates => ServiceLocator.Current.GetInstance<CheckForUpdatesViewModel>();
        public MainViewModel MainView => ServiceLocator.Current.GetInstance<MainViewModel>();

        private void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();

            navigationService.Configure(ViewName.TimerPage, new Uri("../Views/TimerPage.xaml", UriKind.Relative));
            navigationService.Configure(ViewName.CheckForUpdates, new Uri("../Views/CheckForUpdatesView.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}