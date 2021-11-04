using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Quiver))]
public abstract class ArcherAssistant : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;

    private Quiver _quiver;

    public event Action<Arrow> ArrowTaken;
    public event Action ArrowGiven;

    private void Awake()
    {
        _quiver = GetComponent<Quiver>();

        transform.position = _startPosition;
        transform.localEulerAngles = _startRotation;
    }

    public void GiveAllArrows(Archer target)
    {
        if(target == null)
            throw new NullReferenceException(target.name);

        if(_quiver.ArrowsCount <= 0)
            return;

        List<Arrow> arrows = new List<Arrow>();

        while(_quiver.ArrowsCount > 0)
        { 
            var arrow = _quiver.TryGetArrow();
            arrows.Add(arrow);
        }

        target.TakeArrows(arrows);
        arrows.Clear();

        ArrowGiven?.Invoke();
    }

    public void TakeArrow(Arrow arrow)
    {
        if (arrow == null)
            return;

        ArrowTaken?.Invoke(arrow);
        _quiver.Add(arrow);
    }

    public void Restart()
    {
        transform.position = _startPosition;
        transform.localEulerAngles = _startRotation;
    }
}
