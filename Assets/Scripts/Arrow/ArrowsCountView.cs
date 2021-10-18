using System.Collections;
using TMPro;
using UnityEngine;

public class ArrowsCountView : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _secondsBeforeDisappear;

    private WaitForSeconds _seconds;
    private int _currentArrowsCount;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _quiver.ArrowsCountChanged += OnArrowsCountChanged;
    }

    private void OnDisable()
    {
        _quiver.ArrowsCountChanged -= OnArrowsCountChanged;
    }

    private void Start()
    {
        _canvas.gameObject.SetActive(false);
        _seconds = new WaitForSeconds(_secondsBeforeDisappear);
        _currentArrowsCount = _quiver.ArrowsCount;
    }

    private void OnArrowsCountChanged(int count)
    {
        int difference = count - _currentArrowsCount;

        if(_coroutine != null)
            StopCoroutine(_coroutine);

        if (difference > 0)
        {
            _canvas.gameObject.SetActive(true);
            _text.text = "+" + difference;
            _currentArrowsCount = _quiver.ArrowsCount;

             _coroutine = StartCoroutine(TurnOffCanvas());
        }
    }

    private IEnumerator TurnOffCanvas()
    {
        yield return _seconds;

        _canvas.gameObject.SetActive(false);
    }
}