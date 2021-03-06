using System;
using UnityEngine;

[RequireComponent(typeof(ArrowMover))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private Vector3 _startScale;

    private ArrowMover _arrowMover;
    private Collider _collider;

    public Transform Transform { get; private set; }
    public ArrowStates ArrowState;

    public event Action Stopped;

    private void Awake()
    {
        _arrowMover = GetComponent<ArrowMover>();
        Transform = GetComponent<Transform>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _arrowMover.Shooted += OnShooted;
        _arrowMover.Stopped += OnStopped;
    }

    private void OnDisable()
    {
        _arrowMover.Shooted -= OnShooted;
        _arrowMover.Stopped -= OnStopped;
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

    public void TargetShot(Transform target, bool inTarget)
    {
        if (target == null)
            throw new NullReferenceException(target.ToString());

        if (inTarget)
        {
            _arrowMover.TargetShot(target);
        }
        else
        {
            _arrowMover.UntargetShot(target);
        }
    }

    public void ActivateCollider(bool breaker)
    {
        _collider.enabled = breaker;
    }

    private void OnShooted()
    {
        ActivateCollider(true);
    }

    private void OnStopped()
    {
        ActivateCollider(true);
        Stopped?.Invoke();
    }
}
