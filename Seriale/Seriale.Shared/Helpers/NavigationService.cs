using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Seriale.Helpers
{
    public class NavigationService : INavigationService
    {
        private Frame frame { get { return (Frame)Window.Current.Content; } }

     

        
        public void GoBack()
        {
            if (frame.CanGoBack) frame.GoBack();
        }

        public void NavigateTo(Type page)
        {
             frame.Navigate(page);
            
        }
        public void NavigateTo(Type page, object param)
        {
            frame.Navigate(page, param);

        }

       
        
    }
}
