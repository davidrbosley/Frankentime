using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get
            {
                if (_viewModels == null)
                {
                    _viewModels= new ObservableCollection<ViewModelBase>();
                }
                return _viewModels;
            }
        }

    }
}
