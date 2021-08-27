using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArcherAssistant : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;

    private Animator _animator;

    public event Action Died;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Arrow arrow))
            if (arrow.ArrowState == ArrowStates.Killer)
                Die();
    }

    public void GiveAllArrows(Archer target)
    {
        if(target == null)
            throw new NullReferenceException(target.name);

        List<Arrow> arrows = new List<Arrow>();

        while (true)
        {
            Arrow newArrow = _quiver.TryGetArrow();

            if(newArrow == null)
                break;

            arrows.Add(newArrow);
        }

        target.TakeArrows(arrows);

        _animator.Play(ArcherAssistantAnimatorController.States.GiveArrow);
    }

    public void TakeArrow(Arrow arrow)
    {
        if (arrow == null)
            throw new NullReferenceException(arrow.name);

        _animator.Play(ArcherAssistantAnimatorController.States.TakeArrow);
        arrow.gameObject.SetActive(false);
        _quiver.Add(arrow);
    }

    private void Die()
    {
        _animator.SetTrigger(ArcherAssistantAnimatorController.Params.TakeDamage);
        Time.timeScale = 0;
        Died?.Invoke();
    }
}
