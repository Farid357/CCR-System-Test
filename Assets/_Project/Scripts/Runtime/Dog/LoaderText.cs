using DG.Tweening;
using TMPro;
using UnityEngine;

public class LoaderText : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private float _scaleAmplitude = 1.2f;
    [SerializeField] private float _duration = 0.5f;      

    private Tween _tween;
    private Vector3? _startScale;
    
    public void Show()
    {
        _startScale ??= transform.localScale;
        _loadingText.text = "Loading...";
        StartAnimation();
    }

    public void Hide()
    {
        _tween?.Kill();
        _loadingText.transform.localScale = _startScale.Value;
    }

    private void StartAnimation()
    {
        _tween = _loadingText.transform
            .DOScale(Vector3.one * _scaleAmplitude, _duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}