using System;

namespace Seriale.Helpers
{
    public interface INavigationService
    {
        void GoBack();
        void NavigateTo(Type page);
        void NavigateTo(Type page, object param);
    }
}
