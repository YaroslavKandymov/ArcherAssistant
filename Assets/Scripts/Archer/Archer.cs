using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Archer : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;

    public event Action ArrowsIncreased;

    public void TakeArrows(IEnumerable<Arrow> arrows)
    {
        if(arrows == null)
            throw new NullReferenceException(arrows.ToString());

        _quiver.Add(arrows);
        ArrowsIncreased?.Invoke();
    }
}
