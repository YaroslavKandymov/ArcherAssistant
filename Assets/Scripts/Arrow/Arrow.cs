using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ArrowMover))]
public class Arrow : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Vector3 _startScale;

    private ArrowMover _arrowMover;
    private Collider _collider;

    public ArrowStates ArrowState;
    public Transform Transform { get; private set; }

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

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnShooted()
    {
        ActivateCollider(true);
    }

    private void OnStopped()
    {
        ActivateCollider(true);
    }

    public void ActivateCollider(bool breaker)
    {
        _collider.enabled = breaker;
    }
}
