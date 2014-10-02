using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Seriale.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Seriale.Model;
using GalaSoft.MvvmLight.Command;

namespace Seriale.ViewModel
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public TvSeries CurrentTvSeries { get; set; }

        private readonly INavigationService _navigationService;
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand ShowSeasonPageCommand { get; set; }
        private Season _selectedSeason;

        public Season SelectedSeason
        {
            get { return _selectedSeason; }
            set
            {
               if(_selectedSeason!=null) _selectedSeason.EpisodesVisible = false;
                _selectedSeason = value;
                NotifyPropertyChanged("SelectedSeason");
            }
        }

        public async Task Initialize(int id)
        {
            CurrentTvSeries.Id = id;
            await getInfoAsync();


            //  await loadEpisodes();
        }

        public DetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CurrentTvSeries = new TvSeries();
            GoBackCommand = new RelayCommand(GoBack);
            ShowSeasonPageCommand = new RelayCommand(goToSeason);
        }

        private async Task loadEpisodes(int numberOfItem)
        {
            var urlBase =
                String.Format(
                    "https://api.themoviedb.org/3/tv/{0}/season/{1}?api_key=6ddd8a671123ed37164c64d1c8b33a0c",
                    CurrentTvSeries.Id, CurrentTvSeries.Seasons[numberOfItem].SeasonNumber);


            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            CurrentTvSeries.Seasons[numberOfItem] = JsonConvert.DeserializeObject<Season>(json);
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        private async Task getInfoAsync()
        {
            var urlBase = String.Format("https://api.themoviedb.org/3/tv/{0}?api_key=6ddd8a671123ed37164c64d1c8b33a0c",
                CurrentTvSeries.Id);


            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            CurrentTvSeries = JsonConvert.DeserializeObject<TvSeries>(json);
            for (int i = 0; i < CurrentTvSeries.Seasons.Count; i++)
            {
                await loadEpisodes(i);
            }
        }

        private async void goToSeason()
        {
            if (!SelectedSeason.EpisodesVisible) SelectedSeason.EpisodesVisible = true;
            else
            {
                var instances = ServiceLocator.Current.GetInstance<SeasonViewModel>();
                await instances.Initialize(SelectedSeason.SeasonNumber, CurrentTvSeries);
                _navigationService.NavigateTo(typeof(SeasonPage));
            }
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