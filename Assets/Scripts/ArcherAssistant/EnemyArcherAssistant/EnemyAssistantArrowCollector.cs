using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAssistantArrowCollector : MonoBehaviour
{
    [SerializeField] private EnemyMover[] _movers;
    [SerializeField] private Ground _ground;
    [SerializeField] private ArrowsSpawner _spawner;

    private readonly Queue<Arrow> _arrows = new Queue<Arrow>();

    private void OnEnable()
    {
        _ground.ArrowLanded += OnArrowLanded;
        _spawner.ArrowSpawned += OnArrowSpawned;
    }

    private void OnDisable()
    {
        _ground.ArrowLanded -= OnArrowLanded;
        _spawner.ArrowSpawned -= OnArrowSpawned;
    }

    /*private void Update()
    {
        if(_arrows.Count <= 0)
            return;

        foreach (var mover in _movers)
        {
            if (mover.CurrentArrow != null)
            {
                continue;
            }
            else if (mover.gameObject.activeSelf == true)
            {
                if (_arrows.Count > 0)
                {
                    var arrow = _arrows.Dequeue();

                    mover.Init(arrow);
                }
            }
        }
    }*/

    public void Add(Arrow arrow)
    {
        if (arrow == null)
            throw new NullReferenceException(arrow.name);

        _arrows.Enqueue(arrow);
    }

    public void Restart()
    {
        _arrows.Clear();
    }

    private void OnArrowLanded(Arrow arrow)
    {
        Add(arrow);
        SpreadArrows();
    }

    private void OnArrowSpawned(Arrow arrow)
    {
        Add(arrow);
        SpreadArrows();
    }

    private void SpreadArrows()
    {
        if (_arrows.Count <= 0)
            return;

        foreach (var mover in _movers)
        {
            if (mover.CurrentArrow != null)
            {
                continue;
            }
            else if (mover.gameObject.activeSelf == true)
            {
                if (_arrows.Count > 0)
                {
                    var arrow = _arrows.Dequeue();

                    mover.Init(arrow);
                }
            }
        }
    }
}