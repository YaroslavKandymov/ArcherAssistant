using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private float _border;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _rotationSpeed;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        float deltaX = _joystick.Horizontal;
        float deltaZ = _joystick.Vertical;

        if (_joystick.Horizontal >= 0.2f)
        {
            deltaX = _speed;
        }
        else if(_joystick.Horizontal <= 0.2f)
        {
            deltaX = -_speed;
        }

        if (_joystick.Vertical >= 0.2f)
        {
            deltaZ = _speed;
        }
        else if (_joystick.Vertical <= 0.2f)
        {
            deltaZ = -_speed;
        }

        //_animator.SetFloat(AnimatorArcherAssistantController.Params.Speed, _speed);
    }

    private void LimitMovement(float direction)
    {
        if (Mathf.Abs(direction) > _border)
            transform.position = new Vector3(_border, transform.position.y, _border);
    }
}
