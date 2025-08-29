using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Test.Core
{
    public sealed class DogService
    {
        private const string ApiUrl = "https://dogapi.dog/api/v2";
        private static readonly HttpClient _httpClient;

        static DogService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("UnityDogApp/1.0 (contact: you@example.com)");
            _httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        public async UniTask<DogBreed[]> FetchBreedsAsync(CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"{ApiUrl}/breeds", token);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            JObject root = JObject.Parse(json);

            var arr = root["data"];
          
            if (arr == null) 
                throw new Exception("No breeds found");

            int count = Mathf.Min(10, arr.Count());
            var breeds = new DogBreed[count];

            for (int i = 0; i < count; i++)
            {
                breeds[i] = new DogBreed
                {
                    Id = arr[i]["id"]?.ToString(),
                    Name = arr[i]["attributes"]?["name"]?.ToString() ?? "Unknown"
                };
            }

            return breeds;
        }

        public async UniTask<DogBreedDetails> FetchBreedDetailsAsync(string id, CancellationToken token)
        {
            using var response = await _httpClient.GetAsync($"{ApiUrl}/breeds/{id}", token);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            JObject root = JObject.Parse(json);

            var breed = root["data"];
            if (breed == null) throw new Exception("Breed not found");

            return new DogBreedDetails
            {
                Name = breed["attributes"]?["name"]?.ToString() ?? "Unknown",
                Description = breed["attributes"]?["description"]?.ToString() ?? "No description"
            };
        }
    }
}
