using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryVisualization : MonoBehaviour
{
    private const float _step = 0.1f;
    private const int _countPositions = 50;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void Activate(bool value)
    {
        _lineRenderer.enabled = value;
    }

    public void Show(Vector3 startPosition, Vector3 force)
    {
        Activate(true);

        Vector3[] points = new Vector3[_countPositions];
        _lineRenderer.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * _step;

            points[i] = startPosition + force * time + Physics.gravity * time * time / 2f;

            if (points[i].y < 0)
            {
                _lineRenderer.positionCount = i + 1;
                break;
            }
        }

        _lineRenderer.SetPositions(points);
    }
}
