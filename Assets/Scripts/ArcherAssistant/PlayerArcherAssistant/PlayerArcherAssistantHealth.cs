using System;
using UnityEngine;

public class PlayerArcherAssistantHealth : ArcherAssistantHealth
{
    private Animator _animator;

    public event Action PlayerDied;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Die()
    {
        PlayerDied?.Invoke();
    }
}
