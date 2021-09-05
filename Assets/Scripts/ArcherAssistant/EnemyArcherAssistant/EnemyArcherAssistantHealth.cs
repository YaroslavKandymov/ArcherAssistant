using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyArcherAssistantHealth : ArcherAssistantHealth
{
    private Animator _animator;

    public event Action EnemyDied;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Die()
    {
        _animator.Play(ArcherAssistantAnimatorController.States.TakeDamage);
        EnemyDied?.Invoke();
    }
}
