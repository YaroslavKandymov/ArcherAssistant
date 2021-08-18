using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _border;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _rotationSpeed = 15.0f;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

        float deltaX = GetDirection(Horizontal);
        float deltaZ = GetDirection(Vertical);

        /*if (deltaX != 0 || deltaZ != 0)
        {
            movement.x = deltaX;
            movement.z = deltaZ;

            Quaternion tmp = _camera.rotation;
            _camera.eulerAngles = new Vector3(0, _camera.eulerAngles.y, 0);
            movement = _camera.TransformDirection(movement);
            _camera.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation,
                direction, _rotationSpeed * Time.deltaTime);
        }*/

        LimitMovement(transform.position.x);
        LimitMovement(transform.position.z);

        transform.Translate(deltaX, 0, deltaZ);
    }

    private float GetDirection(string axis)
    {
        return Input.GetAxis(axis) * _speed * Time.deltaTime;
    }

    private void LimitMovement(float direction)
    {
        if (Mathf.Abs(direction) > _border)
            transform.position = new Vector3(_border, transform.position.y, _border);
    }
}
