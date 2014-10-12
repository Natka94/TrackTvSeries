using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Seriale.Helpers;
using Seriale.Model;
using System.Threading.Tasks;

namespace Seriale.ViewModel
{
    public class EpisodeViewModel : ObservableObject
    {
        public Episode CurrentEpisode { get; set; } // może fasada?
        public TvSeries CurrentTvSeries { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }

        public RelayCommand GoBackCommand { get; set; }
        private INavigationService _navigationService;
        private readonly IDataService _dataService;

        public EpisodeViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            GoBackCommand = new RelayCommand(_navigationService.GoBack);
        }

        public async Task Initialize(TvSeries currentTvSeries, int numberOfSeason, int numberOfEpisode)
        {
            CurrentTvSeries = currentTvSeries;
            SeasonNumber = numberOfSeason;
            EpisodeNumber = numberOfEpisode;
            CurrentEpisode = await _dataService.GetEpisodeInfoAsync(CurrentTvSeries.Id, numberOfSeason, numberOfEpisode);
        }
    }
}