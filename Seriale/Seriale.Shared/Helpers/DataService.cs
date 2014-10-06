using Newtonsoft.Json;
using Seriale.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
          public async Task <Season> GetSeasonInfoAsync(int idOfTvSeries, int numberOfSeason )
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
          public async Task<PageOfTvSeries> GetPopularTvSeriesAsync()
          {


              const string urlBase = "https://api.themoviedb.org/3/tv/popular?api_key=6ddd8a671123ed37164c64d1c8b33a0c";
              var client = new HttpClient();
              var json = await client.GetStringAsync(urlBase);
              return JsonConvert.DeserializeObject<PageOfTvSeries>(json);

          }
          public async Task<PageOfTvSeries> SearchTvSeriesAsync(string tvSeriesQuery)
          {


              var urlBase = "https://api.themoviedb.org/3/search/tv?api_key=6ddd8a671123ed37164c64d1c8b33a0c&query=" + tvSeriesQuery;
              var client = new HttpClient();
              var json = await client.GetStringAsync(urlBase);
              return JsonConvert.DeserializeObject<PageOfTvSeries>(json);

          }
    }
}
