using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArcherShooter : MonoBehaviour
{
    [SerializeField] private EnemyArcherHealth[] _targets;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _secondsBeforeShot;
    [SerializeField] private float _secondsBetweenShot;
    [SerializeField] private ArrowStates _arrowState;
    [SerializeField] private LosePanel _panel;

    private Transform _currentEnemy;
    private Arrow _currentArrow;
    private float _lastShootTime;
    private Animator _animator;
    private Queue<EnemyArcherHealth> _enemies = new Queue<EnemyArcherHealth>();
    private List<EnemyArcherHealth> _killedEnemies = new List<EnemyArcherHealth>();

    public event Action ArrowsEnded;

    private void OnEnable()
    {
        _panel.SceneRestarted += OnSceneRestarted;

        foreach (var target in _targets)
            target.Died += OnDied;
    }

    private void OnDisable()
    {
        _panel.SceneRestarted -= OnSceneRestarted;

        foreach (var target in _targets)
            target.Died -= OnDied;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        foreach (var target in _targets)
            _enemies.Enqueue(target);

        _currentEnemy = GetTargetPoint();
    }

    private void Update()
    {
        if (_lastShootTime <= 0)
        {
            _animator.SetTrigger(ArcherAnimatorController.Params.GetArrow);
            _currentArrow = _quiver.TryGetArrow();

            if (_currentArrow == null)
            {
                ArrowsEnded?.Invoke();
            }
            else
            {
                StartCoroutine(TargetShot());
            }

            _lastShootTime = _secondsBetweenShot;
        }

        _lastShootTime -= Time.deltaTime;
    }

    private IEnumerator TargetShot()
    {
        _animator.SetTrigger(ArcherAnimatorController.States.Shot);

        yield return new WaitForSeconds(_secondsBeforeShot);

        if (_currentArrow == null)
            yield break;

        _currentArrow.ArrowState = _arrowState;
        _currentArrow.transform.position = _shootPoint.position;
        _currentArrow.gameObject.SetActive(true);
        _currentArrow.TargetShot(_currentEnemy);
    }

    private void OnDied(EnemyArcherHealth enemy)
    {
        _killedEnemies.Add(enemy);

        if(_enemies.Count > 0) 
            _currentEnemy = GetTargetPoint();
    }

    private Transform GetTargetPoint()
    {
        var enemy = _enemies.Dequeue();

        return enemy.GetComponentInChildren<TargetPoint>().transform;
    }

    private void OnSceneRestarted()
    {
        if (_killedEnemies.Count > 0)
        {
            for (int i = 0; i < _killedEnemies.Count; i++)
            {
                _enemies.Enqueue(_killedEnemies[i]);
            }

            _killedEnemies.Clear();
        }
    }
}