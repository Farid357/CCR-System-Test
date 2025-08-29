using UnityEngine;
using DG.Tweening;

public class Loader : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;

    private Tween _rotationTween;

    public void Show()
    {
        gameObject.SetActive(true);
        _rotationTween?.Kill();

        _rotationTween = transform
            .DORotate(new Vector3(0, 0, -360), _duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    public void Hide()
    {
        _rotationTween?.Kill();
        _rotationTween = null;

        gameObject.SetActive(false);
    }
}