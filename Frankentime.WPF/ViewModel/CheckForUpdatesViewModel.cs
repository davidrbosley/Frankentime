using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Squirrel;

namespace Frankentime.WPF.ViewModel
{
    public class CheckForUpdatesViewModel : ViewModelBase
    {
#if DEBUG
        private string UpdateLocation = @"I:\Projects\FrankenTime\Releases";
#else
        private string UpdateLocation = "http://52.183.38.142/downloads";
#endif


        private string _updateStatus;
        private bool _canClose = false;


        public RelayCommand<Window> CloseCommand=> new RelayCommand<Window>(CloseExecute, CanClose);

        private bool CanClose(Window window)
        {
            return _canClose;
        }

        private void CloseExecute(Window window)
        {
            window.Close();
        }

        public CheckForUpdatesViewModel()
        {
            UpdateStatus = "Checking for Updates...";
        }

        public string UpdateStatus
        {
            get { return _updateStatus; }
            set
            {
                _updateStatus = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand WindowLoadedCommand => new RelayCommand(WindowLoaded);


        private void WindowLoaded()
        {
            _canClose = true;
            CheckForUpdates().Wait();
        }

        private async Task CheckForUpdates()
        {
            var restart = false;

            try
            {
                UpdateStatus = $"Loading Update Manager using location {UpdateLocation}...";
                using (var mgr = new UpdateManager(UpdateLocation))
                {
                    var updateInfo = await mgr.CheckForUpdate();

                    if (updateInfo.ReleasesToApply.Any())
                    {
                        restart = true;
                        var newVersion = updateInfo.FutureReleaseEntry.Version;

                        UpdateStatus = $"Beginning to download version {newVersion}...";

                        mgr.DownloadReleases(updateInfo.ReleasesToApply).Wait();

                        UpdateStatus = "Beginning to apply release...";
                        mgr.ApplyReleases(updateInfo).Wait();

                        UpdateStatus = "Creating uninstaller...";
                        mgr.CreateUninstallerRegistryEntry().Wait();
                    }
                    UpdateStatus = "Done!";
                }
            }
            catch (Exception ex)
            {
                UpdateStatus = $"Could not update application! {ex.Message}";
            }

            if (restart)
                UpdateManager.RestartApp();
        }

     

    }
}
