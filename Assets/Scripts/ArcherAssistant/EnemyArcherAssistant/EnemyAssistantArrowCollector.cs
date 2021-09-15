using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAssistantArrowCollector : MonoBehaviour
{
    [SerializeField] private EnemyMover[] _movers;
    [SerializeField] private Ground _ground;
    [SerializeField] private ArrowSpawner _spawner;
    [SerializeField] private EnemyAssistantArrowCollector _assistantArrowCollector;

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

    private void Update()
    {
        if(_arrows.Count <= 0)
            return;

        Collect();
    }

    private void OnArrowLanded(Arrow arrow)
    {
        _assistantArrowCollector.Add(arrow);
    }

    private void OnArrowSpawned(Arrow arrow)
    {
        _assistantArrowCollector.Add(arrow);
    }


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

    private void Collect()
    {
        for (int i = 0; i < _movers.Length; i++)
        {
            if (_movers[i].CurrentArrow != null)
            {
                continue;
            }
            else
            {
                _movers[i].Init(_arrows.Dequeue());
            }
        }
    }
}