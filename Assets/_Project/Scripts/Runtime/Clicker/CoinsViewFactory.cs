using UnityEngine;
using UnityEngine.Pool;

namespace Test.Core
{
    public class CoinsViewFactory : MonoBehaviour
    {
        [SerializeField] private CoinView _coinViewPrefab;
        [SerializeField] private Transform _coinSpawnPoint;
        [SerializeField] private Transform _coinParent;

        public ObjectPool<CoinView> ObjectsPool { get; private set; }

        private void Awake()
        {
            ObjectsPool = new ObjectPool<CoinView>(CreateCoin, OnGetCoin);
        }
        
        private void OnGetCoin(CoinView coin)
        {
            coin.transform.position = _coinSpawnPoint.position;
            coin.gameObject.SetActive(true);
        }

        private CoinView CreateCoin()
        {
            return Instantiate(_coinViewPrefab, _coinSpawnPoint.position, _coinSpawnPoint.rotation,  _coinParent);
        }

        public CoinView GetCoin()
        {
            return ObjectsPool.Get();
        }
    }
}