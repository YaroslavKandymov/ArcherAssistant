using System.Collections;
using UnityEngine;

public class TargetPointFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _point;
    [SerializeField] private float _startHeight;
    [SerializeField] private EnemyRay[] _rays;
    [SerializeField] private float _seconds;

    private float _offsetX;
    private float _offsetZ;
    private float _distance;
    private Transform _transform;
    private bool _isStopped;
    private WaitForSeconds _time;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        foreach (var ray in _rays)
            ray.Stopped += OnStopped;
    }

    private void OnDisable()
    {
        foreach (var ray in _rays)
            ray.Stopped -= OnStopped;
    }

    private void Start()
    {
        _transform = transform;
        _offsetX = transform.position.x - _target.transform.position.x;
        _offsetZ = transform.position.z - _target.transform.position.z;
        _time = new WaitForSeconds(_seconds);
    }

    private void Update()
    {
        if(_isStopped)
            return;

        _distance = transform.position.z - _point.position.z;
        _transform.position = new Vector3(_target.transform.position.x + _offsetX,  _startHeight - _distance * 0.3f, _target.transform.position.z + _offsetZ);
    }

    private void OnStopped()
    {
        _isStopped = true;

        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return _time;

        _isStopped = false;
    }
}

