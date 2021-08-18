using System;
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

    public void GiveAllArrows()
    {
        if (Vector3.Distance(_archer.transform.position, transform.position) > _transmissionRadius)
            return;

        _archer.TakeArrows(_quiver.ArrowsCount);
        _quiver.ClearArrowList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Arrow arrow))
            if (arrow.ArrowStates == ArrowStates.Killer)
                Die();
    }

    public void TakeArrow()
    {
        _quiver.Add(1);
    }

    private void Die()
    {
        Debug.Log("Умер " + name);
        Died?.Invoke();
    }
}
