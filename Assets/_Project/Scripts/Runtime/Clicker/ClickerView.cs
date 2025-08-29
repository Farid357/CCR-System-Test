using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Test.Core
{
    public class ClickerView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private ClickerConfig _config;
        [SerializeField] private CoinView _coinViewPrefab;
        [SerializeField] private Transform _coinSpawnPoint;
        [SerializeField] private Image _coinIcon;
        
        private Vector3 _startButtonScale;
        private Tween _scaleTween;
        private ObjectPool<CoinView> _objectPool;

        [field: SerializeField] public Button Button { get; private set; }

        private void Awake()
        {
            _startButtonScale = Button.transform.localScale;
            _objectPool = new ObjectPool<CoinView>(CreateCoin, OnGetCoin);
        }

        public void PlayEffect()
        {
            _particle.gameObject.SetActive(true);
            _particle.Play();

            CoinView coinView = _objectPool.Get();
            coinView.MoveTo(_coinIcon, _objectPool);
        }

        private void OnGetCoin(CoinView coin)
        {
            coin.transform.position = _coinSpawnPoint.position;
            coin.gameObject.SetActive(true);
        }

        private CoinView CreateCoin()
        {
            return Instantiate(_coinViewPrefab, _coinSpawnPoint.position, _coinSpawnPoint.rotation);
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