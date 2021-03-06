using System;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : ObjectPool<Arrow>
{
    [SerializeField] private int _startArrowsCount;
    [SerializeField] private Arrow _arrowTemplate;

    private readonly Stack<Arrow> _arrows = new Stack<Arrow>();

    public int ArrowsCount => _arrows.Count;

    public event Action<int> ArrowsCountChanged;
    public event Action Fulled;
    public event Action Taken;
    public event Action ArrowsOver;

    private void Awake()
    {
        Initialize(_arrowTemplate);

        foreach (var arrow in Pool)
            if (ArrowsCount != _startArrowsCount)
                Add(arrow);

        ArrowsCountChanged?.Invoke(_arrows.Count);
    }

    public void Add(Arrow arrow)
    {
        if (arrow == null)
            throw new NullReferenceException(arrow.name);

        _arrows.Push(arrow);
        ArrowsCountChanged?.Invoke(_arrows.Count);

        if (_arrows.Count >= Capacity)
            Fulled?.Invoke();
    }

    public void Add(IEnumerable<Arrow> arrows)
    {
        if (arrows == null)
            throw new NullReferenceException(arrows.ToString());

        foreach (var arrow in arrows)
            Add(arrow);
    }

    public Arrow TryGetArrow()
    {
        if (_arrows.Count > 0)
        {
            var newArrow = _arrows.Pop();
            ArrowsCountChanged?.Invoke(_arrows.Count);
            return newArrow;
        }
        else
        {
            ArrowsOver?.Invoke();
        }

        Taken?.Invoke();

        return null;
    }

    public void Clear()
    {
        _arrows.Clear();
    }

    public void Restart()
    {
        _arrows.Clear();
        Taken?.Invoke();

        foreach (var arrow in Pool)
            if (ArrowsCount != _startArrowsCount)
                Add(arrow);

        ArrowsCountChanged?.Invoke(_arrows.Count);
    }
}
