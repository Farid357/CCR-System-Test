using System;
using System.Net.Http;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Test.Core
{
    public sealed class WeatherService
    {
        private const string ApiUrl = "https://api.weather.gov/gridpoints/TOP/32,81/forecast";
        private static readonly HttpClient _httpClient;

        static WeatherService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("UnityWeatherApp/1.0 (contact: you@example.com)");
            _httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        public async UniTask<WeatherData> FetchWeatherAsync(CancellationToken token)
        {
            using var response = await _httpClient.GetAsync(ApiUrl, token);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            JObject root = JObject.Parse(json);
            var today = root["properties"]?["periods"]?[0];
            
            if (today == null)
                throw new Exception("Weather data not found");

            string temp = today["temperature"]?.ToString() ?? "N/A";
            string forecast = today["shortForecast"]?.ToString() ?? "";
            string iconUrl = today["icon"]?.ToString() ?? "";

            Sprite icon = await LoadIcon(iconUrl, token);

            return new WeatherData
            {
                Temperature = temp,
                ShortForecast = forecast,
                Icon = icon
            };
        }

        private async UniTask<Sprite> LoadIcon(string url, CancellationToken token)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            using var www = UnityWebRequestTexture.GetTexture(url);
            var op = await www.SendWebRequest().WithCancellation(token);

            if (www.result == UnityWebRequest.Result.Success)
            {
                var tex = DownloadHandlerTexture.GetContent(www);
                return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }

            Debug.LogWarning($"Failed to load icon: {www.error}");
            return null;
        }
    }
}
