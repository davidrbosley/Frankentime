using System;
using System.Collections.Generic;
using Frankentime.Domain;
using Frankentime.WPF.ViewModel;

namespace Frankentime.Test
{
    public class TFFrameNavigationFake : IFrameNavigationService
    {
        public void GoBack()
        {
            
        }

        public void NavigateTo(string pageKey)
        {
            
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            
        }

        public string CurrentPageKey { get; }
        public object Parameter { get; }
    }
}