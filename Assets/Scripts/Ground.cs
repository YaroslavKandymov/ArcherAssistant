using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private ArrowStates _arrowState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArrowMover arrow))
        {
            arrow.Stop();
            arrow.SetState(_arrowState);
        }
    }
}
