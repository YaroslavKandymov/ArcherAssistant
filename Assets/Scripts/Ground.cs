using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArrowMover arrow))
        {
            arrow.Stop();
        }
    }
}
