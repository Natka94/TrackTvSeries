/*
  In App.xaml:
  <Application.Resources>
      <vm:MvvmViewModelLocator1 xmlns:vm="using:Seriale"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Seriale.Helpers;


namespace Seriale.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
                SimpleIoc.Default.Register<INavigationService, NavigationService>();
            }
            else
            {
                 SimpleIoc.Default.Register<INavigationService, NavigationService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DetailsViewModel>();
            SimpleIoc.Default.Register<SeasonViewModel>();
            

        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public DetailsViewModel Details
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DetailsViewModel>();
            }
        }
        public SeasonViewModel Season
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SeasonViewModel>();
            }
        }
    }
}