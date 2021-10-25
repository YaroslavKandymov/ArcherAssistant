using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyArcherHealth[] _healths;
    private int _allEnemiesMaxHealths;

    public int AllEnemiesMaxHealth => _allEnemiesMaxHealths;

    public event Action ArcherHit;

    private void Awake()
    {
        _healths = GetComponentsInChildren<EnemyArcherHealth>();

        foreach (var health in _healths)
            _allEnemiesMaxHealths += health.MaxHitCount;
    }

    private void OnEnable()
    {
        foreach (var health in _healths)
            health.Hit += OnHit;
    }

    private void OnDisable()
    {
        foreach (var health in _healths)
            health.Hit -= OnHit;
    }

    private void OnHit()
    {
        _allEnemiesMaxHealths--;
        ArcherHit?.Invoke();
    }
}
