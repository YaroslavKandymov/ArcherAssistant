using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Quiver))]
public abstract class ArcherAssistant : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;

    private Quiver _quiver;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _quiver = GetComponent<Quiver>();

        transform.position = _startPosition;
        transform.localEulerAngles = _startRotation;
    }

    public void GiveAllArrows(Archer target)
    {
        if(target == null)
            throw new NullReferenceException(target.name);

        if(_quiver.ArrowsCount <= 0)
            return;

        while (true)
        {
            Arrow newArrow = _quiver.TryGetArrow();

            if(newArrow == null)
                break;

            target.TakeArrow(newArrow);
        }

        _animator.Play(ArcherAssistantAnimatorController.States.GiveArrow);
    }

    public void TakeArrow(Arrow arrow)
    {
        if (arrow == null)
            throw new NullReferenceException(arrow.name);

        arrow.gameObject.SetActive(false);
        _quiver.Add(arrow);
    }

    public void Restart()
    {
        transform.position = _startPosition;
        transform.localEulerAngles = _startRotation;
    }
}
