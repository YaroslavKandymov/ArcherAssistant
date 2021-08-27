using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public event Action<Arrow> ArrowLanded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArrowMover arrow))
        {
            arrow.Stop();
            ArrowLanded?.Invoke(arrow.GetComponent<Arrow>());
        }
    }
}
