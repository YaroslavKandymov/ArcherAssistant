using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private bl_Joystick _joystick;

    private Animator _animator;
    private bool _flipRotation = true;
    private bool _freeze;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_freeze)
            return;

        float vertical = _joystick.Vertical;
        float horizontal = _joystick.Horizontal;

        float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
        angle = _flipRotation ? -angle : angle;

        if (vertical != 0 && horizontal != 0)
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.Fall) == false)
                transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));

        PlayAnimations(vertical, horizontal);
    }

    public void FreezeMoving(bool flag)
    {
        _freeze = flag;
    }

    private void PlayAnimations(float vertical, float horizontal)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.TakeArrow))
            return;

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.GiveArrow))
            return;

        if (Mathf.Abs(vertical) >= 0.1f || Mathf.Abs(horizontal) >= 0.1f)
            _animator.Play(ArcherAssistantAnimatorController.States.RunForward);
        else
            _animator.Play(ArcherAssistantAnimatorController.States.Idle);
    }
}