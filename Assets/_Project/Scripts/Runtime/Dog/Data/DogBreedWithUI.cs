using UnityEngine.UI;

namespace Test.Core
{
    public class DogBreedWithUI
    {
        public DogBreed Breed;
        public Loader Loader;

        public DogBreedWithUI(DogBreed breed, Loader loader)
        {
            Breed = breed;
            Loader = loader;
        }
    }
}