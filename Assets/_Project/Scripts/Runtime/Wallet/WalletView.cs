using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Core
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsCountText;
        
        [field: SerializeField] public Image CoinIcon { get; private set; }

        public void SetCoinsText(string text)
        {
            _coinsCountText.text = text;
        }
    }
}