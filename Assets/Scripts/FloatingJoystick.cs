using UnityEngine;

public class FloatingJoystick : MonoBehaviour
{
    [SerializeField] private float _inactivityTime;

    private Vector3 _position;
    private Vector3 _startPosition;
    private float _time;

    private void Start()
    {
        _position = GetComponent<Transform>().position;
        _startPosition = _position;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                _position = myTouch.position;
            }

            float posX = ((_position.x - Screen.width / 2) * 14 / Screen.width);
            float posY = ((_position.y - Screen.height / 2) * 26 / Screen.height);

            if (posY < 10)
            {
                transform.position = new Vector2(posX, posY);
            }
        }
        else
        {
            _position = _startPosition;
        }
    }
}
