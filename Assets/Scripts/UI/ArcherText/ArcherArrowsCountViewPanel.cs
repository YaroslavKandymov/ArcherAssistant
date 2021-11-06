using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ArcherArrowsCountViewPanel : Panel
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private float _zeroScale;
    [SerializeField] private float _duration;
    [SerializeField] private Transform _scaleObject;
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private float _nextScaleCoefficient;
    [SerializeField] private float _idleTime;
    [SerializeField] private float _increaseTime;

    private Tween _tween;
    private float _time;
    private int _oldArrowsCount;
    private Coroutine _coroutine;

    private void Awake()
    {
        InitBehaviors();
        PanelOpener.Open(this);
    }

    private void OnEnable()
    {
        _quiver.ArrowsCountChanged += OnArrowsCountChanged;
    }

    private void OnDisable()
    {
        _quiver.ArrowsCountChanged -= OnArrowsCountChanged;
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
    }

    private void OnArrowsCountChanged(int count)
    {
        _text.text = count.ToString();

        if (count == 0)
        {
            ChangeSizeZero();
        }
        else
        {
            if(count < _oldArrowsCount)
                return;

            if (_tween.active == true)
            {
                _scaleObject.transform.localScale = _startScale;
                _tween.Kill();
            }

            _oldArrowsCount = count;

            if (_coroutine == null)
            {
                _scaleObject.DOScale(_scaleObject.transform.localScale * _nextScaleCoefficient, _increaseTime);
                _coroutine = StartCoroutine(IncreaseNumberScale());
            }
        }
    }

    private void ChangeSizeZero()
    {
        _tween = _scaleObject.DOScale(_scaleObject.transform.localScale * _zeroScale, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private IEnumerator IncreaseNumberScale()
    {
        while (true)
        {
            _time += Time.deltaTime;

            if (_time > _idleTime)
            {
                _tween.Kill();

                _scaleObject.DOScale(_startScale, _duration);
                _oldArrowsCount = 0;
                _coroutine = null;

                yield break;
            }

            yield return null;
        }
    }
}
