using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Seriale.Helpers;
using Seriale.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Seriale.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string TvSeriesQuery { get; set; }
        private TVSeries _selectedTVSeries;
        public TVSeries SelectedTVSeries
        {
            get { return _selectedTVSeries; }
            set
            {
                _selectedTVSeries = value;
                NotifyPropertyChanged("SelectedTVSeries");
            }
        }
        private PageOfTvSeries _allTVSeries = new PageOfTvSeries();
        public PageOfTvSeries AllTVSeries
        {
            get { return _allTVSeries; }
            set
            {
                _allTVSeries = value;
                NotifyPropertyChanged("AllTVSeries");
            }
        }
        public RelayCommand<object> SearchTVSeriesCommand { get; set; }
        public RelayCommand<object> GetTVSeriesCommand { get; set; }
        public RelayCommand<TVSeries> ShowDetailsPageCommand { get; set; }
       
        private INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService)
        
        {
          
            _navigationService = navigationService;
            GetTVSeriesCommand = new RelayCommand<object>( async (ob) => await getTVSeriesAsync());
            ShowDetailsPageCommand = new RelayCommand<TVSeries>( (arg) => goToDetails(arg) );
            SearchTVSeriesCommand=new RelayCommand<object>( async (ob) => await searchTVSeriesAsync());
        }


        private async void goToDetails(TVSeries selectedTVSeries)
        {
            

            var instances=ServiceLocator.Current.GetInstance<DetailsViewModel>();
            await instances.Initialize(selectedTVSeries.Id);
            _navigationService.NavigateTo(typeof(DetailsPage), SelectedTVSeries);

        }
        private async Task getTVSeriesAsync()
        {
           

           string urlBase = "https://api.themoviedb.org/3/tv/popular?api_key=6ddd8a671123ed37164c64d1c8b33a0c";
           var client = new HttpClient();
           var json = await client.GetStringAsync(urlBase);
           AllTVSeries = JsonConvert.DeserializeObject<PageOfTvSeries>(json);
           
           
           
          

        }
        private async Task searchTVSeriesAsync()
        {


            string urlBase = "https://api.themoviedb.org/3/search/tv?api_key=6ddd8a671123ed37164c64d1c8b33a0c&query="+TvSeriesQuery;
            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            AllTVSeries = JsonConvert.DeserializeObject<PageOfTvSeries>(json);





        }
        public event PropertyChangedEventHandler PropertyChanged;
       
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
