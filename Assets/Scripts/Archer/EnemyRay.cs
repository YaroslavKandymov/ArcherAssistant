using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    [SerializeField] private PlayerArcherAssistant _target;
    [SerializeField] private float _secondsBeforeCast;
    [SerializeField] private ArcherShooter _archer;

    private float currentTime;
    private LineRenderer _lineRenderer;
    private bool _isActive;

    private void OnEnable()
    {
        _archer.ArrowsEnded += OnArrowsEnded;
    }

    private void OnDisable()
    {
        _archer.ArrowsEnded -= OnArrowsEnded;
    }

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_isActive)
        {
            Vector3 delta = (_target.transform.position - transform.position).normalized;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _target.transform.position);
        }
    }

    public void SwitchRay(bool active)
    {
        _isActive = active;
    }

    private void OnArrowsEnded()
    {
        _isActive = false;
    }
}