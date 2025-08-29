using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Test.Core
{
    public sealed class WeatherController : MonoBehaviour
    {
        [SerializeField] private WeatherView _view;
       
        private readonly RequestQueue _queue = new();
        private readonly WeatherService _service = new();

        private CancellationTokenSource _currentRequestCts;
        private bool _isActive;
        private bool _isFirstEnable = true;

        private void OnEnable()
        {
            _isActive = true;
            
            if (_isFirstEnable)
                _view.ShowLoading();
            
            _isFirstEnable = false;
            LoopRequests().Forget();
        }

        public void OnDisable()
        {
            _isActive = false;
            _currentRequestCts?.Cancel(); 
            _queue.ClearQueue();          
        }

        private async UniTaskVoid LoopRequests()
        {
            while (_isActive)
            {
                _queue.Enqueue(async ct =>
                {
                    _currentRequestCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                    
                    var data = await _service.FetchWeatherAsync(_currentRequestCts.Token);
                    _view.HideLoading();
                    _view.ShowWeather(data);
                 
                    _currentRequestCts.Dispose();
                    _currentRequestCts = null;
                });

                await UniTask.Delay(TimeSpan.FromSeconds(5));
            }
        }

        private void OnDestroy()
        {
            _queue.Dispose();
            _currentRequestCts?.Dispose();
        }
    }
}