using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Quiver))]
public abstract class ArcherAssistant : MonoBehaviour
{
    private Quiver _quiver;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _quiver = GetComponent<Quiver>();
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

        if(arrows.Count <= 0)
            return;

        target.TakeArrows(arrows);

        _animator.Play(ArcherAssistantAnimatorController.States.GiveArrow);
    }

    public void TakeArrow(Arrow arrow)
    {
        if (arrow == null)
            throw new NullReferenceException(arrow.name);

        //if(_quiver.ArrowsCount + 1 > _quiver.Capacity)
          //  return;

        _animator.Play(ArcherAssistantAnimatorController.States.TakeArrow);
        arrow.gameObject.SetActive(false);
        _quiver.Add(arrow);
    }
}
