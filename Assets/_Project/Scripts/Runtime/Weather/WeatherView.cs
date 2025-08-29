using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Core
{
    public class WeatherView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private LoaderText _loaderText;
        
        public void ShowWeather(WeatherData data)
        {
            _icon.gameObject.SetActive(true);
            _text.text = $"Today - {data.Temperature}F";
            _icon.sprite = data.Icon;
        }

        public void ShowLoading()
        {
            _loaderText.Show();
        }

        public void HideLoading()
        {
            _loaderText.Hide();
;        }
    }
}