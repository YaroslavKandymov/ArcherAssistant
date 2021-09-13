using UnityEngine;

public abstract class ArcherAssistantHealth : MonoBehaviour
{
    public bool IsDied { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(IsDied == true)
            return;

        if (other.TryGetComponent(out Arrow arrow))
        {
            if (arrow.ArrowState == ArrowStates.Killer)
            {
                IsDied = true;

                Die();
            }
        }
    }

    protected abstract void Die();
}
