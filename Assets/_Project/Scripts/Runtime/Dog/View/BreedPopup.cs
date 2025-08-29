using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Core
{
    public sealed class BreedPopup : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;

        public void Show(DogBreedDetails details)
        {
            _panel.gameObject.SetActive(true);
            _title.text = details.Name;
            _description.text = details.Description;
           
            LayoutRebuilder.ForceRebuildLayoutImmediate(_panel);
        }

        public void Hide()
        {
            _panel.gameObject.SetActive(false);
        }
    }
}