using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArcherAssistant : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Archer _archer;
    [SerializeField] private float _transmissionRadius;

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

    public void GiveAllArrows()
    {
        if (Vector3.Distance(_archer.transform.position, transform.position) > _transmissionRadius)
            return;

        List<Arrow> arrows = new List<Arrow>();

        while (true)
        {
            Arrow newArrow = _quiver.TryGetArrow();

            if(newArrow == null)
                break;

            arrows.Add(newArrow);
        }

        _archer.TakeArrows(arrows);
        _quiver.ClearArrowList();
    }

    public void TakeArrow(Arrow arrow)
    {
        _animator.SetTrigger(AnimatorArcherAssistantController.Params.TakeArrow);
        _quiver.Add(arrow);
    }

    private void Die()
    {
        _animator.SetTrigger(AnimatorArcherAssistantController.Params.TakeDamage);
        Time.timeScale = 0;
        Died?.Invoke();
    }
}
