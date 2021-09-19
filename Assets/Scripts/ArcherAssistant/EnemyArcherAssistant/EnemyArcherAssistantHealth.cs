using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyArcherAssistantHealth : ArcherAssistantHealth
{
    [SerializeField] private float _secondsBeforeDeath;
    [SerializeField] private GameOverPanel[] _panels;

    private Animator _animator;

    public bool IsDied { get; private set; }

    public event Action Died;

    private void OnEnable()
    {
        foreach (var panel in _panels)
        {
            panel.SceneRestarted += OnSceneRestarted;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDied == true)
            return;

        if (other.TryGetComponent(out Arrow arrow))
        {
            if (arrow.ArrowState == ArrowStates.EnemyKiller)
            {
                IsDied = true;

                Die();
            }
        }
    }

    protected override void Die()
    {
        StartCoroutine(PlayDeath());

        Died?.Invoke();
    }

    private IEnumerator PlayDeath()
    {
        _animator.Play(ArcherAssistantAnimatorController.States.TakeDamage);

        yield return new WaitForSeconds(_secondsBeforeDeath);

        gameObject.SetActive(false);
    }

    private void OnSceneRestarted()
    {
        IsDied = false;
        gameObject.SetActive(true);

        foreach (var panel in _panels)
        {
            panel.SceneRestarted -= OnSceneRestarted;
        }
    }
}
