using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    [SerializeField] private PlayerArcherAssistant _target;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Vector3 delta = (_target.transform.position - transform.position).normalized;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _target.transform.position);
    }
}