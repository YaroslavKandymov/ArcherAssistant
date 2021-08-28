using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArcherAssistantHealth : MonoBehaviour
{
    private Animator _animator;

    public event Action Died;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Arrow arrow))
            if (arrow.ArrowState == ArrowStates.Killer)
                Die();
    }

    private void Die()
    {
        _animator.SetTrigger(ArcherAssistantAnimatorController.Params.TakeDamage);
        Died?.Invoke();
    }
}
