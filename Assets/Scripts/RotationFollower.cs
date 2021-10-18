using UnityEngine;

public class RotationFollower : MonoBehaviour
{
    private void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
