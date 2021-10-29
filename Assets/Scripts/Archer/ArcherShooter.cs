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
    [SerializeField] private int _perfectShotNumbers;

    private Transform _currentEnemy;
    private Arrow _currentArrow;
    private float _lastShootTime;
    private Animator _animator;
    private Queue<EnemyArcherHealth> _enemies = new Queue<EnemyArcherHealth>();
    private List<EnemyArcherHealth> _killedEnemies = new List<EnemyArcherHealth>();
    private float _shotCounter;

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
            _currentArrow = _quiver.TryGetArrow();

            if (_currentArrow == null)
            {
                _animator.Play(ArcherAnimatorController.States.Idle);
                return;
            }
            else
            {
                _animator.SetTrigger(ArcherAnimatorController.Params.GetArrow);
                _shotCounter++;

                if ((_shotCounter %= _perfectShotNumbers) == 0)
                {
                    StartCoroutine(TargetShot(true));
                }
                else
                {
                    StartCoroutine(TargetShot(false));
                }

                _lastShootTime = _secondsBetweenShot;
            }
        }

        _lastShootTime -= Time.deltaTime;
    }

    private IEnumerator TargetShot(bool correct)
    {
        _animator.SetTrigger(ArcherAnimatorController.States.Shot);

        yield return new WaitForSeconds(_secondsBeforeShot);

        _currentArrow.ArrowState = _arrowState;
        _currentArrow.transform.position = _shootPoint.position;
        _currentArrow.gameObject.SetActive(true);

        if (correct)
        {
            _currentArrow.TargetShot(_currentEnemy);
        }
        else
        {
            _currentArrow.UntargetShot(_currentEnemy);
        }
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