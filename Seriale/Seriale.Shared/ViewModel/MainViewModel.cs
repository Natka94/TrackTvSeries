using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Seriale.Helpers;
using Seriale.Model;
using System;
using System.ComponentModel;

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
        public RelayCommand SearchTvSeriesCommand { get; set; }
        public RelayCommand GetTvSeriesCommand { get; set; }
        public RelayCommand<TvSeries> ShowDetailsPageCommand { get; set; }
       
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        
        public MainViewModel(INavigationService navigationService, IDataService dataService)

        {
            _dataService = dataService;
            _navigationService = navigationService;
            GetTvSeriesCommand = new RelayCommand( async () => AllTvSeries = await _dataService.GetPopularTvSeriesAsync());
            ShowDetailsPageCommand = new RelayCommand<TvSeries>(goToDetails);
            SearchTvSeriesCommand=new RelayCommand( async () => AllTvSeries=await _dataService.SearchTvSeriesAsync(TvSeriesQuery));
        }


        private async void goToDetails(TvSeries selectedTvSeries)
        {

            var instances=ServiceLocator.Current.GetInstance<DetailsViewModel>();
            await instances.Initialize(selectedTvSeries.Id);
            _navigationService.NavigateTo(typeof(DetailsPage));

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
