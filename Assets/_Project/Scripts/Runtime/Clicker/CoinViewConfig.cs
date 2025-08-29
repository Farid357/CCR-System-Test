using DG.Tweening;
using UnityEngine;

namespace Test.Core
{
    [CreateAssetMenu(fileName = "CoinConfig", menuName = "Configs/CoinViewConfig")]
    public class CoinViewConfig : ScriptableObject
    {
        [field: SerializeField] public float TimeToReachCoinIcon { get; private set; } = 0.5f;

        [field: SerializeField] public Ease Ease { get; private set; } = Ease.OutQuad;
    }
}