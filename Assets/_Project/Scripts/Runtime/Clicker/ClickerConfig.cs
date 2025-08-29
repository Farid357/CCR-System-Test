using DG.Tweening;
using UnityEngine;

namespace Test.Core
{
    [CreateAssetMenu(fileName = "ClickerConfig", menuName = "Configs/ClickerConfig")]
    public class ClickerConfig : ScriptableObject
    {
        [field: SerializeField] public float TimeToAutoClick { get; private set; } = 3f;

        [field: SerializeField] public float ScaleCoefficient { get; private set; } = 0.1f;
       
        [field: SerializeField] public float ScaleDuration { get; private set; } = 0.2f;

        
        [field: SerializeField] public Ease Ease { get; private set; } = Ease.OutQuad;
    }
}