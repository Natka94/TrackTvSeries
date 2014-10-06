using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Seriale.Helpers;
using System.ComponentModel;
using System.Threading.Tasks;
using Seriale.Model;
using GalaSoft.MvvmLight.Command;

namespace Seriale.ViewModel
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
       
        public TvSeries CurrentTvSeries { get; set; }
        private readonly IDataService _dataService;
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
                _selectedSeason = value;
                NotifyPropertyChanged("SelectedSeason");
            }
        }

        public async Task Initialize(int id)
        {
            CurrentTvSeries.Id = id;
            CurrentTvSeries= await _dataService.GetTvSeriesInfoAsync(CurrentTvSeries.Id);
        }

        public DetailsViewModel(INavigationService navigationService, IDataService dataService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
            CurrentTvSeries = new TvSeries();
            GoBackCommand = new RelayCommand(GoBack);
            ShowEpisodesCommand = new RelayCommand(async () => await loadEpisodes());
            ShowEpisodeDetailsCommand=new RelayCommand(async () => await goToEpisodeDetails());
        }

        private async Task loadEpisodes()
        {
            if (SelectedSeason == null || SelectedSeason.EpisodesVisible) return;
            var season = await _dataService.GetSeasonInfoAsync(CurrentTvSeries.Id,SelectedSeason.SeasonNumber);
         
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