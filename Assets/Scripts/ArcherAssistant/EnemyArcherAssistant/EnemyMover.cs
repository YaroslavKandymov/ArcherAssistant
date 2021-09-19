using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ArcherAssistant))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Quiver))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _takeArrowRange;
    [SerializeField] private Archer _archer;
    [SerializeField] private float _transmissionDistance;
    [SerializeField] private float _speed;

    private Quiver _quiver;
    private Arrow _currentArrow;
    private Animator _animator;
    private ArcherAssistant _assistant;
    private Transform _transform;
    private bool _quiverFulled = false;

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
        if (_quiverFulled == true)
        {
            transform.LookAt(_archer.transform);

            _animator.Play(ArcherAssistantAnimatorController.States.Run);
            _transform.position = Vector3.MoveTowards(_transform.position, _archer.transform.position, _speed * Time.deltaTime);

            if (EnoughDistance(transform, _archer.transform, _transmissionDistance))
            {
                _assistant.GiveAllArrows(_archer);
                _quiverFulled = false;
            }
        }
        else
        {
            if (_currentArrow == null)
            {
                _animator.Play(ArcherAssistantAnimatorController.States.Idle);
                return;
            }

            _animator.Play(ArcherAssistantAnimatorController.States.Run);

            var target = _currentArrow.Transform.position;
            target.y = -0.1f;
            _transform.position = Vector3.MoveTowards(_transform.position, target, _speed * Time.deltaTime);

            transform.LookAt(target);

            if (EnoughDistance(transform, _currentArrow.transform, _takeArrowRange))
            {
                _assistant.TakeArrow(_currentArrow);
                _currentArrow = null;
            }
        }
    }

    public void Init(Arrow arrow)
    {
        _currentArrow = arrow;
    }

    private void OnFulled()
    {
        _quiverFulled = true;
    }

    private bool EnoughDistance(Transform self, Transform target, float distance)
    {
        Vector3 offset = self.position - target.transform.position;
        float sqrLength = offset.sqrMagnitude;

        return sqrLength < distance * distance;
    }
}
