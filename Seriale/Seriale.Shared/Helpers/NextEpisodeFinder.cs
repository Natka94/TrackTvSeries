using System;
using System.Linq;
using System.Threading.Tasks;
using Seriale.Model;

namespace Seriale.Helpers
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
            //date of next season               
            DateTime nextSeasonDate = getNextSeasonDate();
            //next Episode in current season 
            DateTime nextEpisodeInSeasonDate = await getNextEpisodeInSeasonDate();

            //date of new season is unknown

            if (nextSeasonDate == default(DateTime))
                return nextEpisodeInSeasonDate;
            if (nextEpisodeInSeasonDate == default(DateTime))
                return nextSeasonDate;
            //choose the nearest date
            return (nextSeasonDate < nextEpisodeInSeasonDate)
                ? nextSeasonDate
                : nextEpisodeInSeasonDate;
        }

        private async Task<DateTime> getNextEpisodeInSeasonDate()
        {
            try
            {
                var seasonOnAir = _tvSeries.Seasons.Where(x => x.AirDate <= DateTime.Now)
                    .OrderByDescending(d => d.AirDate)
                    .FirstOrDefault();
                seasonOnAir = await _dataService.GetSeasonInfoAsync(_tvSeries.Id, seasonOnAir.SeasonNumber);
                var nextEpisodeInSeasonDate = (DateTime)
                    seasonOnAir.Episodes.Where(x => x.AirDate >= DateTime.Now).FirstOrDefault().AirDate;
                return nextEpisodeInSeasonDate;
            }
            catch (NullReferenceException)
            {
                return default(DateTime);
            }
        }

        private DateTime getNextSeasonDate()
        {
            var nextSeason = _tvSeries.Seasons.Where(x => x.AirDate > DateTime.Now).FirstOrDefault();

            DateTime nextSeasonDate = default(DateTime);
            if (nextSeason != null && nextSeason.AirDate != null) nextSeasonDate = (DateTime) nextSeason.AirDate;
            return nextSeasonDate;
        }
    }
}