using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private bl_Joystick _joystick;
    [SerializeField] private float _speed;

    private Animator _animator;
    private bool flipRot = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
        
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(ArcherAssistantAnimatorController.States.TakeArrow))
            return;
        
        if (vertical != 0 || horizontal != 0)
            _animator.Play(ArcherAssistantAnimatorController.States.Run2);
        else
            _animator.Play(ArcherAssistantAnimatorController.States.Idle);
    }
}