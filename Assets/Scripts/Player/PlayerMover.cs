using UnityEngine;
using UnityStandardAssets;

namespace UnityStandardAssets.CrossPlatformInput
{
    [RequireComponent(typeof(Animator))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        [SerializeField] private float _speed;
        [SerializeField] private float _border;

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector3 movement = Vector3.zero;

            /*float deltaX = _joystick.;
            float deltaZ = _joystick.Vertical;
            Debug.Log("1 " + deltaX + " " + deltaZ);
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
    
            transform.Translate(deltaX, 0, deltaZ);
    
            Debug.Log("2 " + deltaX + " " + deltaZ);*/
            //_animator.Play(ArcherAssistantAnimatorController.States.Run);
        }
    }
}
