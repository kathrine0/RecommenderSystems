using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using Recommender.DataAccess.MovieLense;
using Recommender.DataAccess.MovieLense.Entities;

namespace Recommender.DataAccess.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class MovieDataDownloader
    {
        private MovieLenseContext _context;
        private string _apiAddresss = "http://www.omdbapi.com/";

        public MovieDataDownloader()
        {
            _context = new MovieLenseContext();
        }

        public async void GetData()
        {
            foreach (var link in _context.Links.OrderBy(c => c.MovieId).QueryInChunksOf(100))
            {
                var movie = _context.Movies.FirstOrDefault(x => x.Id == link.MovieId);

                if (movie == null) continue;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiAddresss);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var imdbId = link.ImdbId;
                    HttpResponseMessage response = client.GetAsync("?i=tt" + imdbId).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

                        SaveData(movie, result);
                    }
                }
            }
        }

        private void SaveData(Movie movie, Dictionary<string, string> result)
        {

            if (result.ContainsKey("Title"))
            {
                movie.Title = result["Title"];
            }

            int year;
            if (result.ContainsKey("Year") && Int32.TryParse(result["Year"], out year))
                movie.Year = year;

            if (result.ContainsKey("Director"))
            {
                movie.Director = result["Director"];
            }

            if (result.ContainsKey("Actors"))
            {
                movie.Actors = result["Actors"];
            }

            if (result.ContainsKey("Language"))
            {
                movie.Language = result["Language"];
            }

            if (result.ContainsKey("Country"))
            {
                movie.Country = result["Country"];
            }

            double imdbRating;
            if (result.ContainsKey("imdbRating") && Double.TryParse(result["imdbRating"].Replace(".", ","), out imdbRating))
            {
                movie.ImdbRating = imdbRating;
            }

            _context.SaveChanges();
        }

    }
}
