using System.Collections;
using UnityEngine;

public class bl_ControllerExample : MonoBehaviour
{
    [SerializeField]private bl_Joystick _joystick;
    [SerializeField]private float _speed;

    private Animator _animator;
    private Vector3 _previousPosition;
    private bool _flag = true;
    private Coroutine _coroutine;

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
        //transform.localEulerAngles = new Vector3(0, (horizontal + vertical) * 180,0);

        if(vertical != 0 || horizontal != 0)
            _animator.SetFloat(ArcherAssistantAnimatorController.Params.Speed, _speed);
        else
            _animator.SetTrigger(ArcherAssistantAnimatorController.States.Idle);

        Debug.Log($"{vertical} + {horizontal}");
    }
}