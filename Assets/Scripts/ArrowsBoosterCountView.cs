using System.Collections;
using TMPro;
using UnityEngine;

public class ArrowsBoosterCountView : MonoBehaviour
{
    [SerializeField] private ArrowBooster[] _boosters;
    [SerializeField] private float _secondsBeforeDisappear;
    [SerializeField] private Canvas _canvas;
    
    private TMP_Text _text;
    private WaitForSeconds _seconds;

    private void OnEnable()
    {
        foreach (var booster in _boosters)
        {
            booster.ArrowCountIncreased += OnArrowCountIncreased;
        }
    }

    private void OnDisable()
    {
        foreach (var booster in _boosters)
        {
            booster.ArrowCountIncreased -= OnArrowCountIncreased;
        }
    }

    private void Start()
    {
        _text = _canvas.GetComponentInChildren<TMP_Text>();
        _seconds = new WaitForSeconds(_secondsBeforeDisappear);
    }

    private void OnArrowCountIncreased(int count)
    {
        if (count > 0)
        {
            _canvas.gameObject.SetActive(true);
            _text.text = "+" + count;

            StartCoroutine(TurnOffCanvas());
        }
    }

    private IEnumerator TurnOffCanvas()
    {
        yield return _seconds;

        _canvas.gameObject.SetActive(false);
    }
}
