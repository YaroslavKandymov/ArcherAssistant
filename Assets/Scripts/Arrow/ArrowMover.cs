using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Arrow))]
public class ArrowMover : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _seconds;
    [SerializeField] private float _randomCoefficient;

    private Arrow _arrow;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _arrow = GetComponent<Arrow>();
    }

    public void Shoot(Transform target)
    {
        _arrow.ArrowState = ArrowStates.Killer;
        transform.LookAt(target);
        Vector3 delta = target.position - transform.position;
        delta.Normalize();
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = (delta + Random.insideUnitSphere * _randomCoefficient) * _force;
    }

    public void Shoot(Transform[] targets)
    {
        foreach (var target in targets)
            StartCoroutine(ShotPause(target));
    }

    public void Stop()
    {
        _arrow.ArrowState = ArrowStates.NotKiller;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private IEnumerator ShotPause(Transform target)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_seconds);

        Shoot(target);

        yield return waitForSeconds;
    }
}