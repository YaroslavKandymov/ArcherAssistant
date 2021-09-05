using UnityEngine;

public abstract class ArcherAssistantHealth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Arrow arrow))
            if (arrow.ArrowState == ArrowStates.Killer)
                Die();
    }

    protected abstract void Die();
}
