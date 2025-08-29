using System.Threading;
using UnityEngine;

namespace Test.Core
{
    public sealed class DogController : MonoBehaviour
    {
        [SerializeField] private DogView _view;
  
        private readonly RequestQueue _queue = new();
        private readonly DogService _service = new();

        private CancellationTokenSource _currentRequestCts;
        private bool _initialized;

        private void OnEnable()
        {
            if(_initialized)
                return;

            _view.BreedListRequested += OnTabEnter;
            _view.TabExited += OnTabExit;
            _view.BreedClicked += OnBreedClicked;
            _initialized = true;
        }

        private void OnTabEnter()
        {
            RequestBreeds();
        }

        private void OnTabExit()
        {
            CancelCurrent();
            _queue.ClearQueue();
            _view.HideLoader();
            _view.HidePopup();
        }

        private void RequestBreeds()
        {
            _view.ShowLoader();

            _queue.Enqueue(async ct =>
            {
                CancelCurrent();
                _currentRequestCts = CancellationTokenSource.CreateLinkedTokenSource(ct);

                var breeds = await _service.FetchBreedsAsync(_currentRequestCts.Token);

                _view.HideLoader();
                _view.ShowBreeds(breeds);

                CancelCurrent();
            });
        }

        private void OnBreedClicked(DogBreedWithUI breedWithUI)
        {
            breedWithUI.Loader.Show();

            _queue.Enqueue(async ct =>
            {
                CancelCurrent();
                _currentRequestCts = CancellationTokenSource.CreateLinkedTokenSource(ct);

                var details = await _service.FetchBreedDetailsAsync(breedWithUI.Breed.Id, _currentRequestCts.Token);

                breedWithUI.Loader.Hide();
                _view.ShowPopup(details);

                CancelCurrent();
            });
        }

        private void CancelCurrent()
        {
            _currentRequestCts?.Cancel();
            _currentRequestCts?.Dispose();
            _currentRequestCts = null;
        }

        private void OnDestroy()
        {
            _queue.Dispose();
            CancelCurrent();

            _view.BreedListRequested -= OnTabEnter;
            _view.TabExited -= OnTabExit;
            _view.BreedClicked -= OnBreedClicked;
        }
    }
}