using System;

public class PlayerArcherAssistantHealth : ArcherAssistantHealth
{
    public event Action PlayerDied;

    protected override void Die()
    {
        PlayerDied?.Invoke();
    }
}
