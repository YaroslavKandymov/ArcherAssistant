using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArcherAssistant))]
[RequireComponent(typeof(Animator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _takeArrowRange;

    private Arrow _currentArrow;
    private Animator _animator;
    private ArcherAssistant _assistant;
    private Queue<Arrow> _arrows = new Queue<Arrow>();
    private Transform _transform;

    public Arrow CurrentArrow => _currentArrow;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _assistant = GetComponent<ArcherAssistant>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_arrows.Count <= 0)
        {
            _animator.Play(ArcherAssistantAnimatorController.States.Idle);
            return;
        }

        _currentArrow = _arrows.Peek();

        _animator.Play(ArcherAssistantAnimatorController.States.RunForward);

        Vector3 offset = transform.position - _currentArrow.transform.position;
        float sqrLength = offset.sqrMagnitude;

        var direction = _currentArrow.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(direction);
        _transform.rotation = rotation;

        if (sqrLength < _takeArrowRange * _takeArrowRange)
        {
            _assistant.TakeArrow(_currentArrow);

            _arrows.Dequeue();
            _currentArrow = null;
        }
    }

    public void Init(Arrow arrow)
    {
        _arrows.Enqueue(arrow);
    }
}
