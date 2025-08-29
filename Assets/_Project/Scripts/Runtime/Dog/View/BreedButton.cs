using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Core
{
    public sealed class BreedButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        [field: SerializeField] public Button Button { get; private set; }
        
        [field: SerializeField] public Loader Loader { get; private set; }
        
        public void ShowText(string text)
        {
            _text.text = text;
        }
    }
}