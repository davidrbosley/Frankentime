using Frankentime.WPF.ViewModel;

namespace Frankentime.WPF
{
    public class ViewModelLocator
    {
        public TimerViewModel TimerViewModel
        {
            get { return IocKernel.Get<TimerViewModel>(); } 
        }
    }
}
