using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerArrowsCountView : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _secondsBeforeDisappear;
    [SerializeField] private ArrowBooster[] _boosters;

    private TMP_Text _text;
    private WaitForSeconds _seconds;
    private int _currentArrowsCount;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _quiver.ArrowsCountChanged += OnArrowsCountChanged;
        _quiver.Taken += OnTaken;

        foreach (var booster in _boosters)
        {
            booster.ArrowCountIncreased += OnArrowCountIncreased;
        }
    }

    private void OnDisable()
    {
        _quiver.ArrowsCountChanged -= OnArrowsCountChanged;
        _quiver.Taken -= OnTaken;

        foreach (var booster in _boosters)
        {
            booster.ArrowCountIncreased -= OnArrowCountIncreased;
        }
    }

    private void Start()
    {
        _text = _canvasGroup.GetComponentInChildren<TMP_Text>();
        _canvasGroup.alpha = 0;
        _seconds = new WaitForSeconds(_secondsBeforeDisappear);
    }

    private void OnArrowsCountChanged(int count)
    {
        int difference = count - _currentArrowsCount;

        if (difference > 0)
        {
            _text.text = "+" + difference;

            if (_coroutine == null)
            {
                _currentArrowsCount = _quiver.ArrowsCount;
            }
            else
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(TurnOffCanvas());
        }
    }

    private void OnArrowCountIncreased(int count)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _text.text = "+" + count;
        _currentArrowsCount = _quiver.ArrowsCount;

        _coroutine = StartCoroutine(TurnOffCanvas());
    }

    private void OnTaken()
    {
        _text.text = "";
        _currentArrowsCount = 0;
    }

    private IEnumerator TurnOffCanvas()
    {
        _canvasGroup.alpha = 1;
        //_currentArrowsCount = _quiver.ArrowsCount;

        yield return _seconds;

        _canvasGroup.alpha = 0;
        _coroutine = null;
    }
}