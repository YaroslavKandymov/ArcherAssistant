using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuiverView : MonoBehaviour
{
    [SerializeField] private List<QuiverPrefab> _quiverPrefabs;
    [SerializeField] private Quiver _quiver;

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
        foreach (var quiverPrefab in _quiverPrefabs)
        {
            if(quiverPrefab.ArrowCount == _quiver.ArrowsCount)
                quiverPrefab.gameObject.SetActive(true);
            else
                quiverPrefab.gameObject.SetActive(false);
        }
    }

}
