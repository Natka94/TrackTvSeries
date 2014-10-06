using Seriale.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Seriale.Helpers
{
    public interface IDataService
    {
        Task<TvSeries> GetTvSeriesInfoAsync(int idOfTvSeries);
        Task<Season> GetSeasonInfoAsync(int idOfTvSeries, int numberOfSeason);
        Task<PageOfTvSeries> GetPopularTvSeriesAsync();
        Task<PageOfTvSeries> SearchTvSeriesAsync(string tvSeriesQuery);
    }
}
