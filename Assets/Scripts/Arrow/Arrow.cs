using System;
using UnityEngine;

[RequireComponent(typeof(ArrowMover))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private float _collectRange;

    private ArrowMover _arrowMover;

    public ArrowStates ArrowState;

    private void Awake()
    {
        _arrowMover = GetComponent<ArrowMover>();
    }

    public void Shoot(Transform targets)
    {
        if(targets == null)
            throw new NullReferenceException(targets.ToString());

        _arrowMover.Shoot(targets);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerArcherAssistant archerAssistant))
        {
            if (ArrowState == ArrowStates.NotKiller)
            {
                archerAssistant.TakeArrow(this);
            }
        }
    }
}
