using System;
using System.Collections.Generic;
using System.Text;

namespace Seriale.Helpers
{
    public interface INavigationService
    {
        void GoBack();
        void NavigateTo(Type page);
        void NavigateTo(Type page, object param);
    }
}
