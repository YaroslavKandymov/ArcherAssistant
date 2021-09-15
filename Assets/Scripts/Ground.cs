using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private ArrowStates _arrowState;

    public event Action<Arrow> ArrowLanded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArrowMover arrow))
        {
            arrow.Stop();
            arrow.GetComponent<Arrow>().ArrowState = _arrowState;
            ArrowLanded?.Invoke(arrow.GetComponent<Arrow>());
        }
    }
}
