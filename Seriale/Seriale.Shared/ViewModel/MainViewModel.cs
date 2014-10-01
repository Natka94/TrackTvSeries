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
        private string _tvSeriesQuery;
        public string TvSeriesQuery {
            get { return _tvSeriesQuery; }
            set
            {
                if (_tvSeriesQuery == value) return;
                
                _tvSeriesQuery = value;
                if(String.IsNullOrWhiteSpace(_tvSeriesQuery)) AllTvSeries.List.Clear();
                else  SearchTvSeriesCommand.Execute(value);
            }
        }
        private TvSeries _selectedTvSeries;
        public TvSeries SelectedTvSeries
        {
            get { return _selectedTvSeries; }
            set
            {
                _selectedTvSeries = value;
                NotifyPropertyChanged("SelectedTvSeries");
            }
        }
        private PageOfTvSeries _allTvSeries = new PageOfTvSeries();
        public PageOfTvSeries AllTvSeries
        {
            get { return _allTvSeries; }
            set
            {
                _allTvSeries = value;
                NotifyPropertyChanged("AllTvSeries");
            }
        }
        public RelayCommand<object> SearchTvSeriesCommand { get; set; }
        public RelayCommand<object> GetTvSeriesCommand { get; set; }
        public RelayCommand<TvSeries> ShowDetailsPageCommand { get; set; }
       
        private readonly INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService)
        
        {
          
            _navigationService = navigationService;
            GetTvSeriesCommand = new RelayCommand<object>( async (ob) => await getTvSeriesAsync());
            ShowDetailsPageCommand = new RelayCommand<TvSeries>(goToDetails);
            SearchTvSeriesCommand=new RelayCommand<object>( async (ob) => await searchTVSeriesAsync());
        }


        private async void goToDetails(TvSeries selectedTvSeries)
        {
            

            var instances=ServiceLocator.Current.GetInstance<DetailsViewModel>();
            await instances.Initialize(selectedTvSeries.Id);
            _navigationService.NavigateTo(typeof(DetailsPage));

        }
        private async Task getTvSeriesAsync()
        {
           

           const string urlBase = "https://api.themoviedb.org/3/tv/popular?api_key=6ddd8a671123ed37164c64d1c8b33a0c";
           var client = new HttpClient();
           var json = await client.GetStringAsync(urlBase);
           AllTvSeries = JsonConvert.DeserializeObject<PageOfTvSeries>(json);
           
           
           
          

        }
        private async Task searchTVSeriesAsync()
        {


            var urlBase = "https://api.themoviedb.org/3/search/tv?api_key=6ddd8a671123ed37164c64d1c8b33a0c&query="+TvSeriesQuery;
            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            AllTvSeries = JsonConvert.DeserializeObject<PageOfTvSeries>(json);





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
