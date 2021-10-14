using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ArcherAssistant))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private bl_Joystick _joystick;
    [SerializeField] private Archer _archer;
    [SerializeField] private float _transmissionRadius;

    private Animator _animator;
    private bool flipRot = true;
    private ArcherAssistant _archerAssistant;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _archerAssistant = GetComponent<ArcherAssistant>();
    }

    private void Update()
    {
        float vertical = _joystick.Vertical;
        float horizontal = _joystick.Horizontal;

        float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
        angle = flipRot ? -angle : angle;

        if (vertical != 0 && horizontal != 0)
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.Fall) == false)
                transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));

        PlayAnimations(vertical, horizontal);
       
        Vector3 offset = _archer.Transform.position - _transform.position;
        float sqrLength = offset.sqrMagnitude;

        if (sqrLength < _transmissionRadius * _transmissionRadius)
        {
            _archerAssistant.GiveAllArrows(_archer);
        }
    }

    public void Fall()
    {
        _animator.Play(ArcherAssistantAnimatorController.States.Fall);
    }

    private void PlayAnimations(float vertical, float horizontal)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.TakeArrow))
            return;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.GiveArrow))
            return;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.Fall))
            return;

        if (Mathf.Abs(vertical) >= 0.1f || Mathf.Abs(horizontal) >= 0.1f)
            _animator.Play(ArcherAssistantAnimatorController.States.RunForward);
        else
            _animator.Play(ArcherAssistantAnimatorController.States.Idle);
    }
}