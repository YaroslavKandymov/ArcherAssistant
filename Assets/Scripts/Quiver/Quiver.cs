using System;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : ObjectPool<Arrow>
{
    [SerializeField] private int _startArrowsCount;
    [SerializeField] private Arrow _arrowTemplate;
    [SerializeField] private EnemyArcherAssistant _enemyArcherAssistant;

    private readonly Stack<Arrow> _arrows = new Stack<Arrow>();

    public int ArrowsCount => _arrows.Count;

    public event Action ArrowsCountChanged;
    public event Action Fulled;

    private void Awake()
    {
        Initialize(_arrowTemplate);

        foreach (var arrow in Pool)
            if(ArrowsCount != _startArrowsCount) 
                Add(arrow);
    }

    public void Add(Arrow arrow)
    {
        if(arrow == null)
            throw new NullReferenceException(arrow.name);

        _arrows.Push(arrow);
        ArrowsCountChanged?.Invoke();

        if (_arrows.Count >= Capacity)
            Fulled?.Invoke();
    }

    public void Add(IEnumerable<Arrow> arrows)
    {
        if(arrows == null)
            throw new NullReferenceException(arrows.ToString());

        foreach (var arrow in arrows)
            Add(arrow);
    }

    public Arrow TryGetArrow()
    {
        Debug.Log("TryGetArrow()" + _arrows.Count + " " + gameObject.name);
        if (TryGetObject(out Arrow arrow))
        {
            Debug.Log("TryGetArrow()" + _arrows.Count + " " + gameObject.name);

            if (_arrows.Count > 0)
            {
                var newArrow = _arrows.Pop();
                ArrowsCountChanged?.Invoke();
                return newArrow;
            }
        }

        return null;
    }
}
