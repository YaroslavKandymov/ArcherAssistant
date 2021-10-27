using UnityEngine;
using DG.Tweening;

public class HeightWithDistance : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _duration;

    private Transform _transform;
    private float _distance;

    private void Start()
    {
        _transform = transform;
        _distance = _transform.position.z - _point.position.z;
    }

    private void Update()
    {
        _transform.DOMoveY(_distance / 2.5f, _duration);
    }
}
