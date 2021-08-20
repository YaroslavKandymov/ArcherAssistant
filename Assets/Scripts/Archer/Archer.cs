using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Archer : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private GameObject _text;

    public event Action ArrowsIncreased;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
        }
    }

    public void TakeArrows(IEnumerable<Arrow> arrows)
    {
        if(arrows == null)
            throw new NullReferenceException(arrows.ToString());

        _quiver.Add(arrows);
        ArrowsIncreased?.Invoke();
    }
}
