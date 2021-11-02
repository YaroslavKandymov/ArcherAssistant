using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class EnemyArcherHealth : MonoBehaviour
{
    [SerializeField] private float _secondsBeforeDeath;
    [SerializeField] private float _secondsBeforeTakeDamageFalse;
    [SerializeField] private LosePanel _panel;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private int _maxHitCount;

    private Animator _animator;
    private int _hitCount;
    private WaitForSeconds _deathSeconds;
    private WaitForSeconds _takeDamageSeconds;

    public bool IsDied { get; private set; }
    public int MaxHitCount => _maxHitCount;

    public event Action<EnemyArcherHealth> Died;
    public event Action Hit;

    private void OnEnable()
    {
        _panel.LevelRestarted += OnLevelRestarted;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _deathEffect.gameObject.SetActive(false);
        _hitEffect.gameObject.SetActive(false);
        _deathSeconds = new WaitForSeconds(_secondsBeforeDeath);
        _takeDamageSeconds = new WaitForSeconds(_secondsBeforeTakeDamageFalse);
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

                if (_hitCount >= _maxHitCount)
                {
                    Die();
                }
                else
                {
                    _animator.Play(EnemyArcherAnimatorController.States.TakeDamage);
                    StartCoroutine(PlayTakeDamageEffect());
                }
            }
        }
    }

    private void Die()
    {
        StartCoroutine(PlayDeath());
        StartCoroutine(FallDown());

        IsDied = true;
        Died?.Invoke(this);
    }

    private IEnumerator PlayTakeDamageEffect()
    {
        _hitEffect.gameObject.SetActive(true);
        _hitEffect.Play();

        yield return _takeDamageSeconds;

        _hitEffect.Stop();
        _hitEffect.gameObject.SetActive(false);
    }

    private IEnumerator PlayDeath()
    {
        _animator.Play(EnemyArcherAnimatorController.States.Death);
        _deathEffect.gameObject.SetActive(true);
        _deathEffect.Play();

        yield return _deathSeconds;

        _deathEffect.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private IEnumerator FallDown()
    {
        yield return new WaitForSeconds(0.4f);

        transform.DOMoveY(0, 1f);
    }

    private void OnLevelRestarted()
    {
        IsDied = false;
        gameObject.SetActive(true);

        _hitCount = 0;
    }
}
