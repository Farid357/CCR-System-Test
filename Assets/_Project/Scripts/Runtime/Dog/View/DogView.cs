using UnityEngine;
using System;

namespace Test.Core
{
    public sealed class DogView : MonoBehaviour
    {
        [SerializeField] private Loader _loader;
        [SerializeField] private Transform _breedsParent;
        [SerializeField] private BreedButton _breedButtonPrefab;
        [SerializeField] private BreedPopup _popup;

        public event Action BreedListRequested;
        public event Action<DogBreedWithUI> BreedClicked;
        public event Action TabExited;

        private void OnEnable()
        {
            BreedListRequested?.Invoke();
        }

        private void OnDisable()
        {
            TabExited?.Invoke();
        }

        public void ShowLoader() => _loader.Show();
        public void HideLoader() => _loader.Hide();

        public void ShowBreeds(DogBreed[] breeds)
        {
            foreach (Transform child in _breedsParent)
                Destroy(child.gameObject);

            for (int i = 0; i < breeds.Length; i++)
            {
                var breed = breeds[i];
                var breedButton = Instantiate(_breedButtonPrefab, _breedsParent);
                breedButton.ShowText($"{i + 1}.{breed.Name}");

                breedButton.Button.onClick.AddListener(() =>
                {
                    BreedClicked?.Invoke(new DogBreedWithUI(breed, breedButton.Loader));
                });
            }
        }

        public void ShowPopup(DogBreedDetails details) => _popup.Show(details);
        public void HidePopup() => _popup.Hide();
    }
}