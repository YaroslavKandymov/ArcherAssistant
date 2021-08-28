using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private bl_Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private float _takeArrowRange;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float vertical = _joystick.Vertical;
        float horizontal = _joystick.Horizontal;

        Vector3 translate = (new Vector3(horizontal, 0, vertical) * Time.deltaTime) * _speed;
        transform.Translate(translate);

        _animator.SetFloat(ArcherAssistantAnimatorController.Params.Speed, _speed);
    }
}

