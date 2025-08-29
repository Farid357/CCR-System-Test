using TMPro;
using UnityEngine;

namespace Test.Core
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _energyText;

        public void SetEnergyText(string text, Color color)
        {
            _energyText.text = text;
            _energyText.color = color;
        }
    }
}