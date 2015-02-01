using System.Collections.ObjectModel;

namespace Frankentime.WPF.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ObservableCollection<ViewModelBase> _viewModels;

        public MainWindowViewModel()
        {
            
        }

        public ObservableCollection<ViewModelBase> ViewModels
        {
            get { return _viewModels ?? (_viewModels = new ObservableCollection<ViewModelBase>()); }
        }

    }
}
