using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class ArrowMover : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private int _spread;
    [SerializeField] private ParticleSystem _particleSystem;

    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private Arrow _arrow;

    public event Action Shooted;
    public event Action Stopped;

    private void Awake()
    {
        _arrow = GetComponent<Arrow>();
        _rigidbody = GetComponent<Rigidbody>();
        _particleSystem = GetComponentInChildren<ParticleSystem>(true);
        _particleSystem.gameObject.SetActive(false);
    }

    public void UntargetShot(Transform target)
    {
        int[] targetPoints = {-_spread, _spread};

        var number = targetPoints[Random.Range(0, targetPoints.Length)];

        Shot(target, new Vector3(0, number, 0));
    }

    public void TargetShot(Transform target)
    {
        Shot(target, Vector3.zero);
    }

    public void Stop()
    {
        SetArrowValues(RigidbodyConstraints.FreezeAll, Vector3.zero, false, false);

        Stopped?.Invoke();
    }

    public void SetState(ArrowStates state)
    {
        _arrow.ArrowState = state;
    }

    private void Shot(Transform target, Vector3 spread)
    {
        transform.LookAt(target);

        Vector3 delta = (target.position - transform.position).normalized;
        _velocity = delta * _force + spread;

        SetArrowValues(RigidbodyConstraints.None, _velocity, false, true);

        Shooted?.Invoke();
    }

    private void SetArrowValues(RigidbodyConstraints constraints, Vector3 velocity, bool kinematic, bool particleSystemActivity)
    {
        _rigidbody.constraints = constraints;
        _rigidbody.isKinematic = kinematic;
        _rigidbody.velocity = velocity;
        _particleSystem.gameObject.SetActive(particleSystemActivity);
    }
}