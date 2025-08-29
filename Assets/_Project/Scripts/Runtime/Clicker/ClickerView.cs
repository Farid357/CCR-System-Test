using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Core
{
    public class ClickerView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private ClickerConfig _config;
        
        private Vector3 _startButtonScale;
        private Tween _scaleTween;

        [field: SerializeField] public Button Button { get; private set; }
        
        private void Awake()
        {
            _startButtonScale = Button.transform.localScale;
        }

        public void PlayEffect()
        {
            _particle.gameObject.SetActive(true);
            _particle.Play();
        }

        public async UniTaskVoid PlayVFX()
        {
            _scaleTween?.Kill();

            _scaleTween = Button.transform
                .DOScale(_startButtonScale + Vector3.one * _config.ScaleCoefficient, _config.ScaleDuration)
                .SetUpdate(true)
                .SetEase(_config.Ease);

            await _scaleTween.AsyncWaitForCompletion().AsUniTask();

            _scaleTween = Button.transform
                .DOScale(_startButtonScale, _config.ScaleDuration)
                .SetUpdate(true)
                .SetEase(_config.Ease);
        }
    }
}