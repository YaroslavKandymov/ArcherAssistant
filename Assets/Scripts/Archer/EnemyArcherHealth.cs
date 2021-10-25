using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyArcherHealth : ArcherAssistantHealth
{
    [SerializeField] private float _secondsBeforeDeath;
    [SerializeField] private LosePanel _panel;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private int _maxHitCount;

    private Animator _animator;
    private int _hitCount;

    public bool IsDied { get; private set; }
    public int MaxHitCount => _maxHitCount;

    public event Action Died;
    public event Action Hit;

    private void OnEnable()
    {
        _panel.SceneRestarted += OnSceneRestarted;
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
                arrow.gameObject.SetActive(false);
                _hitCount++;
                Hit?.Invoke();

                if(_hitCount >= _maxHitCount) 
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

        var newDeathEffect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
        _deathEffect.Play();

        yield return new WaitForSeconds(_secondsBeforeDeath);

        gameObject.SetActive(false);
    }

    private void OnSceneRestarted()
    {
        IsDied = false;
        gameObject.SetActive(true);

        _panel.SceneRestarted -= OnSceneRestarted;
        
    }
}
