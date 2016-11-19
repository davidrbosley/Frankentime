using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        //private string UpdateLocation = "http://52.183.38.142/downloads";
        private string UpdateLocation = @"C:\Users\bosleybo\Dropbox\Workshare\Frankentime";
#endif

        private string _updateStatus;
        private bool _canClose;


        public RelayCommand<Window> CloseCommand=> new RelayCommand<Window>(CloseExecute, CanClose);
        public ICommand CheckForUpdatesCommand => new RelayCommand(ChecForUpdatesExecute, ()=> true);

        private async void ChecForUpdatesExecute()
        {
            await CheckForUpdates();
            //await ImDumb();
        }


        //private async Task ImDumb()
        //{

        //    UpdateStatus = $"Contacting {UpdateLocation}...";

        //    await Wait();
        //    UpdateStatus = $"Downloading version ...";

        //    await Wait();
        //    UpdateStatus = "Applying release...";
        //    await Wait();
        //    UpdateStatus = "Creating uninstaller...";
        //    await Wait();
        //    UpdateStatus = "Done!";
        //}

        //private async Task Wait()
        //{
        //    await Task.Delay(1000);
        //}


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

#pragma warning disable 4014
            //CheckForUpdates();
#pragma warning restore 4014
            _canClose = true;
        }

        private async Task CheckForUpdates()
        {
            var restart = false;

            try
            {
                UpdateStatus = $"Contacting {UpdateLocation}...";
                using (var mgr = new UpdateManager(UpdateLocation))
                {
                    var t = await mgr.UpdateApp(UpdatePercentage);
                    //var updateInfo = await mgr.CheckForUpdate();

                    //if (updateInfo.ReleasesToApply.Any())
                    //{
                    //    restart = true;
                    //    var newVersion = updateInfo.FutureReleaseEntry.Version;

                    //    UpdateStatus = $"Downloading version {newVersion}...";

                    //    await mgr.DownloadReleases(updateInfo.ReleasesToApply);

                    //    UpdateStatus = "Applying release...";

                    //    await mgr.ApplyReleases(updateInfo);

                    //    UpdateStatus = "Creating uninstaller...";

                    //    await mgr.CreateUninstallerRegistryEntry();

                    //    mgr.CreateShortcutsForExecutable("Frankentime.WPF.exe", ShortcutLocation.Desktop, false);
                    //    mgr.CreateShortcutsForExecutable("Frankentime.WPF.exe", ShortcutLocation.StartMenu, false);
                    //}
                    UpdateStatus = "Done!";
                }
            }
            catch (Exception ex)
            {
                UpdateStatus = $"!! {ex.Message}";
            }

            //if (restart)
            //    UpdateManager.RestartApp();
        }


        private void UpdatePercentage(int percentage)
        {
            UpdateStatus = percentage.ToString();
        }




    }
}
