using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerShadow : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private ArcherAssistant _archerAssistant;
    [SerializeField] private EnemyRay[] _enemyRays;

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

        ActiveMeshes(false);
    }

    private void OnEnable()
    {
        foreach (var enemyRay in _enemyRays)
        {
            enemyRay.Stopped += OnStopped;
            enemyRay.Deactivated += OnDeactivated;
        }
    }

    private void OnDisable()
    {
        foreach (var enemyRay in _enemyRays)
        {
            enemyRay.Stopped -= OnStopped;
            enemyRay.Deactivated -= OnDeactivated;
        }
    }

    private void OnStopped()
    {
        _transform.position = _archerAssistant.transform.position;
        _transform.localEulerAngles = _archerAssistant.transform.localEulerAngles;
        ActiveMeshes(true);
        _animator.speed = 0;
        _mover.FreezeMoving(true);
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
