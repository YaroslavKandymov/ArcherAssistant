using System;
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
    private bool _isMoving;
    private Arrow _currentArrow;

    public event Action Stopped;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _offSeconds = new WaitForSeconds(_secondsBeforeOff);
        _onSeconds = new WaitForSeconds(_secondsBeforeOn);
        _lineRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_isMoving == true)
        {
            _lineRenderer.SetPosition(0, _startPosition.position);
            _lineRenderer.SetPosition(1, _target.position);
        }
    }

    public void Activate(Arrow arrow)
    {
        _currentArrow = arrow;
        _currentArrow.Stopped += OnStopped;
        StartCoroutine(TurnOnRay());
    }

    private void SetActive(bool switcher)
    {
        _lineRenderer.enabled = switcher;
    }

    private IEnumerator TurnOnRay()
    {
        yield return _onSeconds;

        SetActive(true);
        _isMoving = true;

        yield return _offSeconds;

        _isMoving = false;
        Stopped?.Invoke();
    }

    private void OnStopped()
    {
        SetActive(false);
        _currentArrow.Stopped -= OnStopped;
    }
}