using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Archer : MonoBehaviour
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Vector3 _startRotation;

    private Animator _animator;

    public event Action ArrowsIncreased;

    public Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        ResetRotation();
    }

    public void TakeArrows(IEnumerable<Arrow> arrows)
    {
        if(arrows == null)
            throw new NullReferenceException(arrows.ToString());

        _quiver.Add(arrows);
        ArrowsIncreased?.Invoke();
        _animator.Play(ArcherAnimatorController.States.TakeArrow);
    }

    public void ResetRotation()
    {
        transform.localEulerAngles = _startRotation;
    }
}
