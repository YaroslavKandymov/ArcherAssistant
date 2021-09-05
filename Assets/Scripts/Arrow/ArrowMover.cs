using System;
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
    private ParticleSystem _particleSystem;

    public event Action<Arrow> ArrowMissed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _arrow = GetComponent<Arrow>();
        _particleSystem = GetComponentInChildren<ParticleSystem>(true);
    }

    public void Shoot(Transform target)
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _arrow.ArrowState = ArrowStates.Killer;
        transform.LookAt(target);
        Vector3 delta = (target.position - transform.position).normalized;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = (delta + UnityEngine.Random.insideUnitSphere * _randomCoefficient) * _force;
        _particleSystem.gameObject.SetActive(true);
    }
    
    /*public void Shoot(Transform target)
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _arrow.ArrowState = ArrowStates.Killer;
        transform.LookAt(target);
        Vector3 delta = (target.position - transform.position).normalized;
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = (delta + UnityEngine.Random.insideUnitSphere * _randomCoefficient) * _force;
        _particleSystem.gameObject.SetActive(true);
    }*/

    public void Shoot(Transform[] targets)
    {
        StartCoroutine(ManyShots(targets));
    }

    public void Stop()
    {
        _arrow.ArrowState = ArrowStates.NotKiller;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        ArrowMissed?.Invoke(_arrow);
        _particleSystem.gameObject.SetActive(false);
    }

    private IEnumerator ManyShots(Transform[] targets)
    {
        foreach (var target in targets)
            Shoot(target);

        yield return new WaitForSeconds(_seconds);
    }
}