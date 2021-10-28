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
    private bool _isWorking;

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
        _currentArrowsCount = _quiver.ArrowsCount;
    }

    private void OnArrowsCountChanged(int count)
    {
        int difference = count - _currentArrowsCount;

        if (_isWorking)
        {
            difference ++;
            StopCoroutine(_coroutine);
        }

        if (difference > 0)
        {
            _text.text = "+" + difference;

            if (_isWorking == false)
            {
                _currentArrowsCount = _quiver.ArrowsCount;
            }

            _coroutine = StartCoroutine(TurnOffCanvas());
        }
    }

    private void OnArrowCountIncreased(int count)
    {
        if (_isWorking)
        {
            StopCoroutine(_coroutine);
        }

        _text.text = "+" + count;
        _currentArrowsCount = _quiver.ArrowsCount;

        _coroutine = StartCoroutine(TurnOffCanvas());
    }

    private void OnTaken()
    {
        _canvasGroup.alpha = 0;
        _text.text = "";
    }

    private IEnumerator TurnOffCanvas()
    {
        _canvasGroup.alpha = 1;
        _isWorking = true;

        yield return _seconds;

        _canvasGroup.alpha = 0;
        _isWorking = false;
    }
}