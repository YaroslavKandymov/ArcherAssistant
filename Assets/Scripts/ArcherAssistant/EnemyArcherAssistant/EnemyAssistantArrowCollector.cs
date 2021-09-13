using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class EnemyAssistantArrowCollector : MonoBehaviour
{
    [SerializeField] private EnemyMover[] _movers;

    private readonly Queue<Arrow> _arrows = new Queue<Arrow>();

    private void Update()
    {
        if (_arrows.Count <= 0)
            return;

        foreach (var mover in _movers)
            if(mover.CurrentArrow != null)
                return;
        
        var arrow = _arrows.Dequeue();
        var currentCollector = _movers.FirstOrDefault(c => c.CurrentArrow == null);

        currentCollector.Init(arrow);
    }

    public void Collect(Arrow arrow)
    {
        if (arrow == null)
            throw new NullReferenceException(arrow.name);

        _arrows.Enqueue(arrow);
    }

    public void Restart()
    {
        _arrows.Clear();
    }
}