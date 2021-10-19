using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class ArrowMover : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _randomCoefficient;

    private Rigidbody _rigidbody;
    private ParticleSystem _particleSystem;
    private Vector3 _velocity;

    public Vector3 Velocity => _velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _particleSystem = GetComponentInChildren<ParticleSystem>(true);
        _particleSystem.gameObject.SetActive(false);
    }

    public void UntargetShot(Transform target)
    {
        Shot(target, Random.insideUnitSphere * _randomCoefficient);
    }

    public void TargetShot(Transform target)
    {
        Shot(target, Vector3.zero);
    }

    public void Stop()
    {
        SetArrowValues(RigidbodyConstraints.FreezeAll, Vector3.zero, false, false);
    }

    private void Shot(Transform target, Vector3 spread)
    {
        transform.LookAt(target);

        Vector3 delta = (target.position - transform.position).normalized;
        _velocity = (delta + spread) * _force;

        SetArrowValues(RigidbodyConstraints.None, _velocity, false, true);
    }

    private void SetArrowValues(RigidbodyConstraints constraints, Vector3 velocity, bool kinematic, bool particleSystemActivity)
    {
        _rigidbody.constraints = constraints;
        _rigidbody.isKinematic = kinematic;
        _rigidbody.velocity = velocity;
        _particleSystem.gameObject.SetActive(particleSystemActivity);
    }
}