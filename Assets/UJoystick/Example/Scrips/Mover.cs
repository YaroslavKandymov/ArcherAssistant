using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ArcherAssistant))]
public class Mover : MonoBehaviour
{
    [SerializeField] private bl_Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private Archer _archer;
    [SerializeField] private float _transmissionRadius;

    private Animator _animator;
    private bool flipRot = true;
    private ArcherAssistant _archerAssistant;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _archerAssistant = GetComponent<ArcherAssistant>();
    }

    private void Update()
    {
        float vertical = _joystick.Vertical;
        float horizontal = _joystick.Horizontal;

        Vector3 translate = (new Vector3(horizontal, 0, vertical) * Time.deltaTime) * _speed;

        float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
        angle = flipRot ? -angle : angle;
        if (vertical != 0 && horizontal != 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
        
        PlayAnimations(vertical, horizontal);
       
        Vector3 offset = _archer.transform.position - transform.position;
        float sqrLength = offset.sqrMagnitude;

        if (sqrLength < _transmissionRadius * _transmissionRadius)
        {
            _archerAssistant.GiveAllArrows(_archer);
        }
    }

    private void PlayAnimations(float vertical, float horizontal)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.TakeArrow))
            return;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.GiveArrow))
            return;

        if (vertical != 0 || horizontal != 0)
            _animator.Play(ArcherAssistantAnimatorController.States.RunForward);
        else
            _animator.Play(ArcherAssistantAnimatorController.States.Idle);

    }
}