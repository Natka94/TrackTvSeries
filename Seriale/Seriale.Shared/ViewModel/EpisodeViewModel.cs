using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Seriale.Helpers;
using Seriale.Model;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Seriale.ViewModel
{
    public class EpisodeViewModel : ObservableObject
    {
        public Episode CurrentEpisode { get; set; }// może fasada?
        public TvSeries CurrentTvSeries { get; set; }
        public int NumberOfSeason { get; set; }
        public int NumberOfEpisode { get; set; }

        public RelayCommand GoBackCommand { get; set; }
        private INavigationService _navigationService;
        public EpisodeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBackCommand=new RelayCommand(_navigationService.GoBack);
        }
        public async Task Initialize(TvSeries currentTvSeries, int numberOfSeason, int numberOfEpisode)
        {
            CurrentTvSeries = currentTvSeries;
            NumberOfSeason = numberOfSeason;
            NumberOfEpisode = numberOfEpisode;
           
           await getInfoAsync();

        }

        private async Task getInfoAsync()
        {
            var urlBase = String.Format("https://api.themoviedb.org/3/tv/{0}/season/{1}/episode/{2}?api_key=6ddd8a671123ed37164c64d1c8b33a0c", 
                CurrentTvSeries.Id,NumberOfSeason,NumberOfEpisode);
            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
          
            CurrentEpisode = JsonConvert.DeserializeObject<Episode>(json);
        }


       
    }
}
