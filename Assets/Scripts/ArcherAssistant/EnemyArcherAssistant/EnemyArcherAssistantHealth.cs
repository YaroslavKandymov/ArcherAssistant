using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyArcherAssistantHealth : ArcherAssistantHealth
{
    [SerializeField] private float _secondsBeforeDeath;

    private Animator _animator;

    public event Action EnemyDied;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Die()
    {
        StartCoroutine(PlayDeath());

        EnemyDied?.Invoke();
    }

    private IEnumerator PlayDeath()
    {
        _animator.Play(ArcherAssistantAnimatorController.States.TakeDamage);

        yield return new WaitForSeconds(_secondsBeforeDeath);

        gameObject.SetActive(false);
    }
}
