using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Transform _transform;

    public Transform Transform => _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }
}
