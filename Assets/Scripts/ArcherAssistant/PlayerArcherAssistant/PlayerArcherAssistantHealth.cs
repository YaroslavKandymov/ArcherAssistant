using System;
using UnityEngine;

public class PlayerArcherAssistantHealth : ArcherAssistantHealth
{
    public event Action Died;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Arrow arrow))
            if (arrow.ArrowState == ArrowStates.PlayerKiller)
                Die();
    }

    protected override void Die() => Died?.Invoke();
}
