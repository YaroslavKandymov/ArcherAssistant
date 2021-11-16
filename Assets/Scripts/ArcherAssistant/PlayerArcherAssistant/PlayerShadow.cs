using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerShadow : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private ArcherAssistant _archerAssistant;
    [SerializeField] private EnemyArcherHealth _enemyArcherHealth;

    private EnemyRay _enemyRay;
    private Animator _animator;
    private PlayerMover _mover;
    private Transform _transform;
    private MeshRenderer[] _meshRenderers;
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private void Awake()
    {
        transform.position = _startPosition;
        transform.localEulerAngles = _startRotation;
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _mover = GetComponent<PlayerMover>();
        _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        _skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        _enemyRay = _enemyArcherHealth.GetComponentInChildren<EnemyRay>();

        ActiveMeshes(false);
    }

    private void OnEnable()
    {
        _enemyRay.Stopped += OnStopped;
        _enemyRay.Deactivated += OnDeactivated;

        _enemyArcherHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemyRay.Stopped -= OnStopped;
        _enemyRay.Deactivated -= OnDeactivated;

        _enemyArcherHealth.Died -= OnDied;
    }

    private void OnStopped()
    {
        _transform.position = _archerAssistant.transform.position;
        _transform.localEulerAngles = _archerAssistant.transform.localEulerAngles;
        ActiveMeshes(true);
        _animator.speed = 0;
        _mover.FreezeMoving(true);
    }

    private void OnDied(EnemyArcherHealth health)
    {
        gameObject.SetActive(false);
    }

    private void OnDeactivated()
    {
        ActiveMeshes(false);
        _animator.speed = 1;
        _mover.FreezeMoving(false);
    }

    private void ActiveMeshes(bool flag)
    {
        foreach (var meshRenderer in _meshRenderers)
        {
            meshRenderer.enabled = flag;
        }

        _skinnedMeshRenderer.enabled = flag;
    }
}
