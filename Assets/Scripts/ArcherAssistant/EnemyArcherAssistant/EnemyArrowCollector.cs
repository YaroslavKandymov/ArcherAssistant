using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyArcherAssistant))]
public class EnemyArrowCollector : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _takeArrowRange;

    private EnemyArcherAssistant _archerAssistant;
    private Arrow _currentArrow;
    private readonly Queue<Arrow> _arrows = new Queue<Arrow>();
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _archerAssistant = GetComponent<EnemyArcherAssistant>();
    }

    private void Update()
    {
        if (_arrows.Count <= 0)
        {
            _animator.Play(ArcherAssistantAnimatorController.States.Idle);
            return;
        }

        if (_arrows.Count > 0 && _currentArrow == null)
        {
            _currentArrow = _arrows.Peek();

            _animator.Play(ArcherAssistantAnimatorController.States.RunForward);
            transform.LookAt(_currentArrow.transform);
        }

        Vector3 offset = transform.position - _currentArrow.transform.position;
        float sqrLength = offset.sqrMagnitude;

        if (sqrLength < _takeArrowRange * _takeArrowRange)
        {
            _archerAssistant.TakeArrow(_currentArrow);

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.TakeArrow))
                return;

            _currentArrow = null;
            _arrows.Dequeue();
        }
    }

    public void Collect(Arrow arrow)
    {
        if (arrow == null)
            throw new NullReferenceException(arrow.name);

        _arrows.Enqueue(arrow);
    }

    public void Restart()
    {
        _arrows.Clear();
        _currentArrow = null;
    }
}