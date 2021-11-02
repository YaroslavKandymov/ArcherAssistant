using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private LosePanel _losePanel;
    private EnemyArcherHealth[] _healths;
    private int _allEnemiesHealths;
    private int _allEnemiesMaxHealths;

    public int AllEnemiesHealth => _allEnemiesHealths;

    public event Action ArcherHealthChanged;

    private void Awake()
    {
        _healths = GetComponentsInChildren<EnemyArcherHealth>();

        foreach (var health in _healths)
            _allEnemiesMaxHealths += health.MaxHitCount;

        _allEnemiesHealths = _allEnemiesMaxHealths;
    }

    private void OnEnable()
    {
        _losePanel.LevelRestarted += OnLevelRestarted;

        foreach (var health in _healths)
            health.Hit += OnHit;
    }

    private void OnDisable()
    {
        _losePanel.LevelRestarted -= OnLevelRestarted;

        foreach (var health in _healths)
            health.Hit -= OnHit;
    }

    private void OnHit()
    {
        _allEnemiesHealths--;
        ArcherHealthChanged?.Invoke();
    }

    private void OnLevelRestarted()
    {
        _allEnemiesHealths = _allEnemiesMaxHealths;
    }
}
