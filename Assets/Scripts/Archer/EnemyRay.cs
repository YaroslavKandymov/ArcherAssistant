using System.Collections;
using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _target;
    [SerializeField] private float _secondsBeforeOn;
    [SerializeField] private float _secondsBeforeOff;

    private WaitForSeconds _onSeconds;
    private WaitForSeconds _offSeconds;
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _offSeconds = new WaitForSeconds(_secondsBeforeOff);
        _onSeconds = new WaitForSeconds(_secondsBeforeOn);
        _lineRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        _lineRenderer.SetPosition(0, _startPosition.position);
        _lineRenderer.SetPosition(1, _target.position);
    }

    public void Activate()
    {
        StartCoroutine(TurnOnRay());
    }

    private IEnumerator TurnOnRay()
    {
        yield return _onSeconds;

        _lineRenderer.enabled = true;

        yield return _offSeconds;

        _lineRenderer.enabled = false;
    }
}