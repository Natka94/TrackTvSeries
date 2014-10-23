using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Seriale.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Seriale.Helpers
{
    public class DataService : IDataService
    {
        public async Task<TvSeries> GetTvSeriesInfoAsync(int idOfTvSeries)
        {
            var urlBase = String.Format("https://api.themoviedb.org/3/tv/{0}?api_key=6ddd8a671123ed37164c64d1c8b33a0c",
                idOfTvSeries);
            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            return JsonConvert.DeserializeObject<TvSeries>(json);
        }

        public async Task<Season> GetSeasonInfoAsync(int idOfTvSeries, int numberOfSeason)
        {
            var urlBase =
                String.Format(
                    "https://api.themoviedb.org/3/tv/{0}/season/{1}?api_key=6ddd8a671123ed37164c64d1c8b33a0c",
                    idOfTvSeries, numberOfSeason
                    );


            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            return JsonConvert.DeserializeObject<Season>(json);
        }

        public async Task<Episode> GetEpisodeInfoAsync(int idOfTvSeries, int numberOfSeason, int numberOfEpisode)
        {
            try
            {
                var urlBase =
                    String.Format(
                        "https://api.themoviedb.org/3/tv/{0}/season/{1}/episode/{2}?api_key=6ddd8a671123ed37164c64d1c8b33a0c",
                        idOfTvSeries, numberOfSeason, numberOfEpisode);
                var client = new HttpClient();
                var json = await client.GetStringAsync(urlBase);

                return JsonConvert.DeserializeObject<Episode>(json);
            }
            catch (HttpRequestException)
            {

                return new Episode()
                {
                    Name = "Error",
                    Overview = "Sorry, We don't have any information about this episode. "

                };
            }

        }

        public async Task<PageOfTvSeries> GetAiringTodayTvSeriesAsync(int amount)
        {
            var pageOfTvSeries=new PageOfTvSeries {List = new ObservableCollection<TvSeries>()};
            var client = new HttpClient();
            for (var i = 1; i <= amount; i++)
            {
                string urlBase = String.Format("https://api.themoviedb.org/3/tv/airing_today?api_key=6ddd8a671123ed37164c64d1c8b33a0c&page={0}",i);
                var json = await client.GetStringAsync(urlBase);
                var newPage = JsonConvert.DeserializeObject<PageOfTvSeries>(json);
                foreach (var p in newPage.List)
                {
                    pageOfTvSeries.List.Add(p);
                }
            }



            return pageOfTvSeries;
        }
        public async Task<PageOfTvSeries> GetTopRatedTvSeriesAsync(int amount)
        {
            var pageOfTvSeries = new PageOfTvSeries { List = new ObservableCollection<TvSeries>() };
            var client = new HttpClient();
            for (var i = 1; i <= amount; i++)
            {
                string urlBase = String.Format("https://api.themoviedb.org/3/tv/top_rated?api_key=6ddd8a671123ed37164c64d1c8b33a0c&page={0}", i);
                var json = await client.GetStringAsync(urlBase);
                var newPage = JsonConvert.DeserializeObject<PageOfTvSeries>(json);
                foreach (var p in newPage.List)
                {
                    pageOfTvSeries.List.Add(p);
                }
            }



            return pageOfTvSeries;
        }
        public async Task<PageOfTvSeries> GetPopularTvSeriesAsync(int amount)
        {
            var pageOfTvSeries = new PageOfTvSeries { List = new ObservableCollection<TvSeries>() };
            var client = new HttpClient();
            for (var i = 1; i <= amount; i++)
            {
                string urlBase = String.Format("https://api.themoviedb.org/3/tv/popular?api_key=6ddd8a671123ed37164c64d1c8b33a0c&page={0}", i);
                var json = await client.GetStringAsync(urlBase);
                var newPage = JsonConvert.DeserializeObject<PageOfTvSeries>(json);
                foreach (var p in newPage.List)
                {
                    pageOfTvSeries.List.Add(p);
                }
            }



            return pageOfTvSeries;
        }
        public async Task<PageOfTvSeries> SearchTvSeriesAsync(string tvSeriesQuery)
        {
            var urlBase = "https://api.themoviedb.org/3/search/tv?api_key=6ddd8a671123ed37164c64d1c8b33a0c&query=" +
                          tvSeriesQuery;
            var client = new HttpClient();
            var json = await client.GetStringAsync(urlBase);
            return JsonConvert.DeserializeObject<PageOfTvSeries>(json);
        }
    }
}