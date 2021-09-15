using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class ArrowMover : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _randomCoefficient;

    private Rigidbody _rigidbody;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _particleSystem = GetComponentInChildren<ParticleSystem>(true);
        _particleSystem.gameObject.SetActive(false);
    }

    public void Shoot(Transform target)
    {
        transform.LookAt(target);

        Vector3 delta = (target.position - transform.position).normalized;
        Vector3 velocity = (delta + Random.insideUnitSphere * _randomCoefficient) * _force;

        SetArrowValues(RigidbodyConstraints.None, velocity, false, true);
    }

    public void Stop()
    {
        SetArrowValues(RigidbodyConstraints.FreezeAll, Vector3.zero, false, false);
    }

    private void SetArrowValues(RigidbodyConstraints constraints, Vector3 velocity, bool kinematic, bool particleSystemActivity)
    {
        _rigidbody.constraints = constraints;
        _rigidbody.isKinematic = kinematic;
        _rigidbody.velocity = velocity;
        _particleSystem.gameObject.SetActive(particleSystemActivity);
    }
}