using UnityEngine;

public class TargetPointFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _point;
    [SerializeField] private float _startHeight;

    private float _offsetX;
    private float _offsetZ;
    private float _distance;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        _offsetX = transform.position.x - _target.transform.position.x;
        _offsetZ = transform.position.z - _target.transform.position.z;
    }

    private void Update()
    {
        _distance = transform.position.z - _point.position.z;
        _transform.position = new Vector3(_target.transform.position.x + _offsetX,  _startHeight - _distance * 0.3f, _target.transform.position.z + _offsetZ);
    }
}

