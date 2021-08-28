using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyArcherAssistant))]
public class EnemyArrowCollector : MonoBehaviour, ICollector
{ 
    [SerializeField] private float _speed;
    [SerializeField] private float _takeArrowRange;
    [SerializeField] private float seconds;

    private EnemyArcherAssistant _archerAssistant;
    private Arrow _currentArrow;
    private readonly Queue<Arrow> _arrows = new Queue<Arrow>();
    private Coroutine _coroutine;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _archerAssistant = GetComponent<EnemyArcherAssistant>();
    }

    public void Collect(Arrow arrow)
    {
        if (arrow == null)
            throw new NullReferenceException(arrow.name);

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _arrows.Enqueue(arrow);

        _coroutine = StartCoroutine(TakeArrow());
    }

    private IEnumerator TakeArrow()
    {
        while (true)
        {
            if (_arrows.Count > 0)
            {
                _currentArrow = _arrows.Dequeue();

                _animator.Play(ArcherAssistantAnimatorController.States.Run);
                transform.LookAt(_currentArrow.transform);
            }

            transform.position = Vector3.MoveTowards(transform.position, _currentArrow.transform.position,
                _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _currentArrow.transform.position) < _takeArrowRange)
            {
                _archerAssistant.TakeArrow(_currentArrow);

                yield return new WaitForSeconds(seconds);

                if (_arrows.Count <= 0)
                {
                    _animator.Play(ArcherAssistantAnimatorController.States.Idle);
                }

                yield break;
            }

            yield return null;
        }
    }
}
