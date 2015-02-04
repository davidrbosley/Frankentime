using System;
using System.ComponentModel;
using Frankentime.Domain.Analytics;

namespace Frankentime.WPF.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected readonly IAnalytics Analytics;

        protected ViewModelBase(
            IAnalytics analytics)
        {
            Analytics = analytics;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }

        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            
        }
    }
}
