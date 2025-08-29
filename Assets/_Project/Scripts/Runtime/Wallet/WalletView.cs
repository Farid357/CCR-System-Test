using TMPro;
using UnityEngine;

namespace Test.Core
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsCountText;
        
        public void SetCoinsText(string text)
        {
            _coinsCountText.text = text;
        }
    }
}