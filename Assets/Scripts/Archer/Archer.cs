using System;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Vector3 _startPosition;

    public event Action ArrowsIncreased;

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    private void Start()
    {
        ResetTransform();
    }

    public void TakeArrows(IEnumerable<Arrow> arrows)
    {
        if(arrows == null)
            throw new NullReferenceException(arrows.ToString());

        _quiver.Add(arrows);
        ArrowsIncreased?.Invoke();
    }
    
    public void TakeArrow(Arrow arrow)
    {
        if(arrow == null)
            throw new NullReferenceException(arrow.ToString());

        _quiver.Add(arrow);
        ArrowsIncreased?.Invoke();
    }

    public void ResetTransform()
    {
        transform.localEulerAngles = _startRotation;
        transform.position = _startPosition;
    }
}