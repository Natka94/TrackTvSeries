using System.Collections.ObjectModel;
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
        public RelayCommand ShowEpisodesCommand { get; set; }
        public RelayCommand ShowEpisodeDetailsCommand { get; set; }
        public Episode SelectedEpisode  { get; set; }
        private Season _selectedSeason;

        public Season SelectedSeason
        {
            get { return _selectedSeason; }
            set
            {
                // if(_selectedSeason!=null) _selectedSeason.EpisodesVisible = false;
                _selectedSeason = value;
                NotifyPropertyChanged("SelectedSeason");
            }
        }

        public async Task Initialize(int id)
        {
            CurrentTvSeries.Id = id;
            await getInfoAsync();
        }

        public DetailsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CurrentTvSeries = new TvSeries();
            GoBackCommand = new RelayCommand(GoBack);
            ShowEpisodesCommand = new RelayCommand(async () => await loadEpisodes());
            ShowEpisodeDetailsCommand=new RelayCommand(async () => await goToEpisodeDetails());
        }

        private async Task loadEpisodes()
        {
            if (SelectedSeason==null || SelectedSeason.EpisodesVisible) return;
            var urlBase =
                String.Format(
                    "https://api.themoviedb.org/3/tv/{0}/season/{1}?api_key=6ddd8a671123ed37164c64d1c8b33a0c",
                    CurrentTvSeries.Id, SelectedSeason.SeasonNumber);


            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            var season = JsonConvert.DeserializeObject<Season>(json);

            var indexOfSeason = CurrentTvSeries.Seasons.ToList().FindIndex(x => SelectedSeason.Id == x.Id);
            CurrentTvSeries.Seasons[indexOfSeason] = season;
            CurrentTvSeries.Seasons[indexOfSeason].EpisodesVisible = true;
        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        private async Task goToEpisodeDetails()
        {
            var instances = ServiceLocator.Current.GetInstance<EpisodeViewModel>();
            await instances.Initialize(CurrentTvSeries, SelectedEpisode.SeasonNumber,SelectedEpisode.EpisodeNumber);
            _navigationService.NavigateTo(typeof(EpisodePage));

        }
        private async Task getInfoAsync()
        {
            var urlBase = String.Format("https://api.themoviedb.org/3/tv/{0}?api_key=6ddd8a671123ed37164c64d1c8b33a0c",
                CurrentTvSeries.Id);

            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            CurrentTvSeries = JsonConvert.DeserializeObject<TvSeries>(json);
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