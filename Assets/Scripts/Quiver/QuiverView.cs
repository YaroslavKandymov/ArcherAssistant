using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiverView : MonoBehaviour
{
    [SerializeField] private List<QuiverPrefab> _quiverPrefabs;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private float _secondsBeforeChange;

    private void OnEnable()
    {
        _quiver.ArrowsCountChanged += OnArrowCountChanged;
    }

    private void OnDisable()
    {
        _quiver.ArrowsCountChanged -= OnArrowCountChanged;
    }

    private void OnArrowCountChanged()
    {
        StartCoroutine(TakeArrow());
    }

    private IEnumerator TakeArrow()
    {
        WaitForSeconds seconds = new WaitForSeconds(_secondsBeforeChange);

        yield return seconds;

        foreach (var quiverPrefab in _quiverPrefabs)
        {
            if (quiverPrefab.ArrowCount == _quiver.ArrowsCount)
                quiverPrefab.gameObject.SetActive(true);
            else
                quiverPrefab.gameObject.SetActive(false);
        }
    }
}
