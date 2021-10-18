using System.Collections;
using TMPro;
using UnityEngine;

public class ArrowsBoosterCountView : MonoBehaviour
{
    [SerializeField] private ArrowBooster[] _boosters;
    [SerializeField] private float _secondsBeforeDisappear;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _text;

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
        _seconds = new WaitForSeconds(_secondsBeforeDisappear);
    }

    private void OnArrowCountIncreased(int count)
    {
        _canvas.gameObject.SetActive(true);
        _text.text = "+" + count;

        StartCoroutine(TurnOffCanvas());
    }

    private IEnumerator TurnOffCanvas()
    {
        yield return _seconds;

        _canvas.gameObject.SetActive(false);
    }
}
