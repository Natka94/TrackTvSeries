using System;
using System.Threading.Tasks;
using Seriale.Helpers;
using Seriale.Model;
using System.Linq;

namespace Seriale.ViewModel
{
    public class NextEpisodeFinder
    {

        private readonly TvSeries _tvSeries;
        private readonly IDataService _dataService;
        public NextEpisodeFinder(TvSeries tvSeries, IDataService dataService)
        {
            _dataService = dataService;
            _tvSeries = tvSeries;
        }

        public async Task<DateTime> FindNextEpisode()
        {
            if (_tvSeries.Status == "Ended") return default(DateTime);

            var nextSeason = _tvSeries.Seasons.Where(x => x.AirDate > DateTime.Now).FirstOrDefault();
            DateTime nextSeasonDate=default(DateTime);
            if (nextSeason != null)  nextSeasonDate = nextSeason.AirDate;
            var seasonOnAir = _tvSeries.Seasons.Where(x => x.AirDate <= DateTime.Now)
                .OrderByDescending(d => d.AirDate)
                .FirstOrDefault();           
            seasonOnAir = await _dataService.GetSeasonInfoAsync(_tvSeries.Id, seasonOnAir.SeasonNumber);
            DateTime nextEpisodeInSeasonDate =
                seasonOnAir.Episodes.Where(x => x.AirDate >= DateTime.Now).FirstOrDefault().AirDate;

            if (nextSeason == null)
                return nextEpisodeInSeasonDate;
            else
            {
                return (nextSeasonDate < nextEpisodeInSeasonDate)
                    ? nextSeasonDate: nextEpisodeInSeasonDate;
            }


        }
    }
}