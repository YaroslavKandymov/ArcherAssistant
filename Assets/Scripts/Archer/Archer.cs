using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Archer : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;

    public event Action ArrowsIncreased;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeArrows(IEnumerable<Arrow> arrows)
    {
        if(arrows == null)
            throw new NullReferenceException(arrows.ToString());

        _quiver.Add(arrows);
        ArrowsIncreased?.Invoke();
        _animator.Play(ArcherAnimatorController.States.TakeArrow);
    }
}
