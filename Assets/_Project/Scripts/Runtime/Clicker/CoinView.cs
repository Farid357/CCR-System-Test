using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Test.Core
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField] private CoinViewConfig _config;
        
        public async UniTaskVoid MoveTo(Image coinImage, ObjectPool<CoinView> pool)
        {
            await transform.DOMove(coinImage.transform.position, _config.TimeToReachCoinIcon).SetEase(_config.Ease).SetUpdate(true).AsyncWaitForCompletion().AsUniTask();
            gameObject.SetActive(false);
          
            pool.Release(this);
        }
    }
}