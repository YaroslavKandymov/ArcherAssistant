using UnityEngine;
using DG.Tweening;

public class BoosterTextView : MonoBehaviour
{
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _targetHeight;
    [SerializeField] private float _duration;
    [SerializeField] private ArrowBooster _booster;

    private Vector3 _startPosition;
    private Vector3 _startScale;

    private void OnEnable()
    {
        _booster.Taken += OnTaken;
    }

    private void OnDisable()
    {
        _booster.Taken -= OnTaken;
    }

    private void Start()
    {
        transform.position = _booster.transform.position;
        _startPosition = transform.position;
        _startScale = transform.localScale;
    }

    private void OnTaken(ArrowBooster booster)
    {
        transform.DOScale(_targetScale, _duration).OnComplete(() => transform.localScale = _startScale);
        transform.DOLocalMoveY(_targetHeight, _duration).OnComplete(() => transform.position = _startPosition);
    }
}
