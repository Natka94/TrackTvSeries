using Seriale.Model;
using System.Threading.Tasks;

namespace Seriale.Helpers
{
    public interface IDataService
    {
        Task<TvSeries> GetTvSeriesInfoAsync(int idOfTvSeries);
        Task<Season> GetSeasonInfoAsync(int idOfTvSeries, int numberOfSeason);
        Task<Episode> GetEpisodeInfoAsync(int idOfTvSeries, int numberOfSeason, int numberOfEpisode);
        Task<PageOfTvSeries> GetPopularTvSeriesAsync(int amount);
        Task<PageOfTvSeries> SearchTvSeriesAsync(string tvSeriesQuery);
    }
}
