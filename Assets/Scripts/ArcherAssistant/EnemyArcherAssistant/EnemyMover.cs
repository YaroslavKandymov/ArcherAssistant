using UnityEngine;

[RequireComponent(typeof(ArcherAssistant))]
[RequireComponent(typeof(Animator))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _takeArrowRange;

    private Arrow _currentArrow;
    private Animator _animator;
    private ArcherAssistant _assistant;

    public Arrow CurrentArrow => _currentArrow;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _assistant = GetComponent<ArcherAssistant>();
    }

    private void Update()
    {
        if (_currentArrow == null)
        {
            _animator.Play(ArcherAssistantAnimatorController.States.Idle);
            return;
        }

        _animator.Play(ArcherAssistantAnimatorController.States.RunForward);
        transform.LookAt(_currentArrow.transform);

        Vector3 offset = transform.position - _currentArrow.transform.position;
        float sqrLength = offset.sqrMagnitude;

        if (sqrLength < _takeArrowRange * _takeArrowRange)
        {
            _assistant.TakeArrow(_currentArrow);

            _currentArrow = null;
        }
    }

    public void Init(Arrow arrow)
    {
        _currentArrow = arrow;
    }
}
