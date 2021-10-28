using TMPro;
using UnityEngine;
using DG.Tweening;

public class ArcherArrowsCountViewPanel : Panel
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private float _scale;
    [SerializeField] private float _duration;
    [SerializeField] private Transform _scaleObject;
    [SerializeField] private Ease _ease = Ease.Linear;

    private Tween _tween;

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
            _scaleObject.transform.localScale = Vector3.one;
            _tween.Kill();
        }
    }

    private void ChangeSizeZero()
    {
        _tween = _scaleObject.DOScale(_scale, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(_ease);
    }
}
