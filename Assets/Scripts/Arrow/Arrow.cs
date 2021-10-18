using System;
using UnityEngine;

[RequireComponent(typeof(ArrowMover))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private float _collectRange;

    private ArrowMover _arrowMover;

    public ArrowStates ArrowState;
    public Transform Transform { get; private set; }

    private void Awake()
    {
        _arrowMover = GetComponent<ArrowMover>();
        Transform = GetComponent<Transform>();
    }

    public void TargetShot(Transform target)
    {
        if (target == null)
            throw new NullReferenceException(target.ToString());

        _arrowMover.TargetShot(target);
    }

    public void UntargetShot(Transform target)
    {
        if(target == null)
            throw new NullReferenceException(target.ToString());

        _arrowMover.UntargetShot(target);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerArcherAssistant archerAssistant))
        {
            if (ArrowState == ArrowStates.EnemyKiller)
            {
                archerAssistant.TakeArrow(this);
            }
        }
    }
}
