using UnityEngine;

public class RotationFollower : MonoBehaviour
{
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
