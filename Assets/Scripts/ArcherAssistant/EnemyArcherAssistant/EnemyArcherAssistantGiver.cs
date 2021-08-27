using System.Collections;
using UnityEngine;

public class EnemyArcherAssistantGiver : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Archer _archer;
    [SerializeField] private float _transmissionDistance;

    private Quiver _quiver;
    private EnemyArcherAssistant _archerAssistant;
    private Animator _animator;

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
            transform.position = Vector3.MoveTowards(transform.position, _archer.transform.position,
                _speed * Time.deltaTime);
            transform.LookAt(_archer.transform);

            _animator.Play(ArcherAssistantAnimatorController.States.Run);

            if (Vector3.Distance(transform.position, _archer.transform.position) <= _transmissionDistance)
            {
                _archerAssistant.GiveAllArrows(_archer);
                yield break;
            }

            yield return null;
        }
    }
}