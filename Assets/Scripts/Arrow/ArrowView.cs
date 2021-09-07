using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowView : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Transform _arrowsPlace;

    private void OnEnable()
    {
        _quiver.ArrowsCountChanged += OnArrowsCountChanged;
    }

    private void OnDisable()
    {
        _quiver.ArrowsCountChanged -= OnArrowsCountChanged;
    }

    private void OnArrowsCountChanged(int count)
    {

    }
}
