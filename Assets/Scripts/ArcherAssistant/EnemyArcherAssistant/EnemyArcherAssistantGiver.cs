using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyArcherAssistant))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Quiver))]
public class EnemyArcherAssistantGiver : MonoBehaviour
{
    [SerializeField] private Archer _archer;
    [SerializeField] private float _speed;
    [SerializeField] private float _transmissionDistance;

    private EnemyArcherAssistant _archerAssistant;
    private Animator _animator;
    private Quiver _quiver;

    private void Awake()
    {
        _archerAssistant = GetComponent<EnemyArcherAssistant>();
        _animator = GetComponent<Animator>();
        _quiver = GetComponent<Quiver>();
    }

    private void OnEnable()
    {
        _quiver.Fulled += OnFulled;
    }

    private void OnDisable()
    {
        _quiver.Fulled -= OnFulled;
    }

    private void OnFulled()
    {
        StartCoroutine(GiveArrow());
    }

    private IEnumerator GiveArrow()
    {
        while (true)
        {
            transform.LookAt(_archer.transform);

            _animator.Play(ArcherAssistantAnimatorController.States.RunForward);

            if (Vector3.Distance(transform.position, _archer.transform.position) <= _transmissionDistance)
            {
                _archerAssistant.GiveAllArrows(_archer);
                yield break;
            }

            yield return null;
        }
    }
}
