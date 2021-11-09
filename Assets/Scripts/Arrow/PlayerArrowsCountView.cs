using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerArrowsCountView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _secondsBeforeDisappear;
    [SerializeField] private ArrowBooster[] _boosters;

    private TMP_Text _text;
    private WaitForSeconds _seconds;
    private Coroutine _coroutine;
    private Coroutine _coroutineMax;
    private ArcherAssistant _archerAssistant;

    private void Awake()
    {
        _archerAssistant = GetComponent<ArcherAssistant>();
    }

    private void OnEnable()
    {
        _archerAssistant.MaxArrowsCountReached += OnMaxArrowsCountReached;

        foreach (var booster in _boosters)
        {
            booster.MaxArrowsCountReached += OnMaxArrowsCountReached;
        }
    }

    private void OnDisable()
    {
        _archerAssistant.MaxArrowsCountReached -= OnMaxArrowsCountReached;

        foreach (var booster in _boosters)
        {
            booster.MaxArrowsCountReached -= OnMaxArrowsCountReached;
        }
    }

    private void Start()
    {
        _text = _canvasGroup.GetComponentInChildren<TMP_Text>();
        _canvasGroup.alpha = 0;
        _seconds = new WaitForSeconds(_secondsBeforeDisappear);
    }

    private IEnumerator TurnOffCanvas()
    {
        _canvasGroup.alpha = 1;

        yield return _seconds;

        _canvasGroup.alpha = 0;
    }

    private void OnMaxArrowsCountReached()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _text.text = "Max arrows";
        _coroutine = StartCoroutine(TurnOffCanvas());
    }

    private void OnMaxArrowsCountReached(Arrow arrow)
    {
        if (_coroutineMax != null)
        {
            StopCoroutine(_coroutineMax);
        }

        _text.text = "Max arrows";
        _coroutineMax = StartCoroutine(TurnOffCanvas());
    }
}