using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArrowMover arrow))
        {
            arrow.Stop();
            arrow.gameObject.SetActive(false);
            arrow.transform.parent = null;
        }
    }
}
