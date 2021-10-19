using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ArcherAssistant))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Quiver))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _takeArrowRange;
    [SerializeField] private List<Archer> _archers;
    [SerializeField] private float _transmissionDistance;
    [SerializeField] private float _speed;

    private Quiver _quiver;
    private Arrow _currentArrow;
    private Animator _animator;
    private ArcherAssistant _assistant;
    private Transform _transform;
    private bool _quiverFulled = false;
    private Archer _closestArcher;
    private Vector3 _offset = new Vector3();

    public Arrow CurrentArrow => _currentArrow;

    private void Awake()
    {
        _quiver = GetComponent<Quiver>();
    }

    private void OnEnable()
    {
        _quiver.Fulled += OnFulled;
    }

    private void OnDisable()
    {
        _quiver.Fulled -= OnFulled;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _assistant = GetComponent<ArcherAssistant>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.TakeDamage))
            return;

        if(_quiverFulled == true)
            return;

        if (_currentArrow == null)
        {
            _animator.Play(ArcherAssistantAnimatorController.States.Idle);
            return;
        }

        var target = _currentArrow.Transform.position;
        target.y = 1.4f;

        MoveTo(target);

        if (_offset.SqrDistance(transform, _currentArrow.transform, _takeArrowRange))
        {
            _assistant.TakeArrow(_currentArrow);
            _currentArrow = null;
        }
    }

    public void Init(Arrow arrow)
    {
        _currentArrow = arrow;
    }

    private void OnFulled()
    {
        _quiverFulled = true;

        _closestArcher = _archers.OrderBy(a => a.GetComponent<Quiver>().ArrowsCount).FirstOrDefault();

        StartCoroutine(MoveToArcher(_closestArcher));
    }

    private IEnumerator MoveToArcher(Archer closestArcher)
    {
        while (true)
        {
            MoveTo(closestArcher.transform.position);

            if (_offset.SqrDistance(transform, _closestArcher.transform, _transmissionDistance))
            {
                _assistant.GiveAllArrows(_closestArcher);
                _quiverFulled = false;
            }

            yield return null;
        }
    }

    private void MoveTo(Vector3 target)
    {
        _animator.Play(ArcherAssistantAnimatorController.States.Run);

        _transform.position = Vector3.MoveTowards(_transform.position, target, _speed * Time.deltaTime);

        transform.LookAt(target);
    }
}
