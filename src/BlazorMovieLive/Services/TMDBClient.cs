using BlazorMovieLive.Models;
using System.Net.Http.Json;

namespace BlazorMovieLive.Services
{
    public class TMDBClient
    {
        private readonly HttpClient httpClient;

        public TMDBClient(HttpClient httpClient, IConfiguration config)
        {
            this.httpClient = httpClient;

            this.httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            this.httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

            string apiKey = config["TMDBKey"] ?? throw new Exception("TMDBKey not found!");
            this.httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        }

        public Task<PopularMoviePagedResponse?> GetPopularMoviesAsync(int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }
            if (page > 500)
            {
                page = 500;
            }

            return httpClient.GetFromJsonAsync<PopularMoviePagedResponse>($"movie/popular?page={page}");
        }

        public Task<MovieDetails?> GetMovieDetailsAsync(int id)
        {
            return httpClient.GetFromJsonAsync<MovieDetails>($"movie/{id}");
        }
    }
}
