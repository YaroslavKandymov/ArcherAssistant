using UnityEngine;

public class BoosterLifetime : MonoBehaviour
{
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _existTime;

    public float SpawnInterval => _spawnInterval;
    public float ExistTime => _existTime;
}
