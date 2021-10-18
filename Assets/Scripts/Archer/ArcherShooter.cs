using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArcherShooter : MonoBehaviour
{
    [SerializeField] private Transform _randomTargetPoint;
    [SerializeField] private Transform[] _targets;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _secondsBeforeShot;
    [SerializeField] private float _secondsBetweenShot;
    [SerializeField] private ArrowStates _arrowState;
    [SerializeField] private float _secondsBeforeTargetShot;
    [SerializeField] private LosePanel _panel;

    private Arrow _currentArrow;
    private float _lastShootTime;
    private Animator _animator;
    private float _realtime;
    private Queue<Transform> _newTargets = new Queue<Transform>();
    private Coroutine _coroutine;

    public event Action ArrowsEnded;

    private void OnEnable()
    {
        _panel.SceneRestarted += OnSceneRestarted;
    }

    private void OnDisable()
    {
        _panel.SceneRestarted -= OnSceneRestarted;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        foreach (var target in _targets)
        {
            _newTargets.Enqueue(target);
        }
    }

    private void Update()
    {
        _realtime += Time.deltaTime;

        if (_lastShootTime <= 0)
        {
            _animator.SetTrigger(ArcherAnimatorController.Params.GetArrow);
            _currentArrow = _quiver.TryGetArrow();

            if (_currentArrow == null)
            {
                ArrowsEnded?.Invoke();
            }
            else if(_realtime < _secondsBeforeTargetShot)
            {
                _coroutine = StartCoroutine(UntargetShot());
            }
            else if(_realtime >= _secondsBeforeShot)
            {
                StopCoroutine(_coroutine);
                StartCoroutine(TargetShot());
            }

            _lastShootTime = _secondsBetweenShot;
        }

        _lastShootTime -= Time.deltaTime;
    }

    private IEnumerator UntargetShot()
    {
        _animator.SetTrigger(ArcherAnimatorController.Params.Shot);

        yield return new WaitForSeconds(_secondsBeforeShot);

        if (_currentArrow == null)
            yield break;

        _currentArrow.ArrowState = _arrowState;
        _currentArrow.transform.position = _shootPoint.position;
        _currentArrow.gameObject.SetActive(true);
        _currentArrow.UntargetShot(_randomTargetPoint);
    }
    
    private IEnumerator TargetShot()
    {
        _animator.SetTrigger(ArcherAnimatorController.Params.Shot);

        yield return new WaitForSeconds(_secondsBeforeShot);

        if (_currentArrow == null)
            yield break;

        _currentArrow.ArrowState = _arrowState;
        _currentArrow.transform.position = _shootPoint.position;
        _currentArrow.gameObject.SetActive(true);
        _currentArrow.TargetShot(GetTarget());
    }

    private Transform GetTarget()
    {
        var newTarget = _newTargets.Peek();

        if (newTarget.gameObject.activeSelf == false)
        {
            _newTargets.Dequeue();
            newTarget = _newTargets.Peek();
        }

        return newTarget;
    }

    private void OnSceneRestarted()
    {
        _realtime = 0;
    }
}