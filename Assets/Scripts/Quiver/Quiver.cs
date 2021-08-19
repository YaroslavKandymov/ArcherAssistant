using System;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : ObjectPool<Arrow>
{
    [SerializeField] private int _startArrowsCount;
    [SerializeField] private Arrow _arrowTemplate;

    private readonly Stack<Arrow> _arrows = new Stack<Arrow>();

    public int ArrowsCount => _arrows.Count;

    public event Action ArrowsCountChanged;

    private void Start()
    {
        Initialize(_arrowTemplate);

        for (int i = 0; i < _startArrowsCount; i++)
        {
            if (TryGetObject(out _arrowTemplate))
            {
                Add(_arrowTemplate);
            }
        }
    }

    public void Add(Arrow arrow)
    {
        _arrows.Push(arrow);
        ArrowsCountChanged?.Invoke();
    }

    public void Add(IEnumerable<Arrow> arrows)
    {
        foreach (var arrow in arrows)
        {
            _arrows.Push(arrow);
        }

        ArrowsCountChanged?.Invoke();
    }

    public Arrow TryGetArrow()
    {
        if (TryGetObject(out _arrowTemplate))
        {
            if (_arrows.Count > 0)
            {
                _arrows.Pop();
                ArrowsCountChanged?.Invoke();
                return _arrowTemplate;
            }
        }

        return null;
    }

    public void ClearArrowList()
    {
        if (_arrows.Count <= 0)
            throw new ArgumentOutOfRangeException("Quiver have no arrows");

        while (_arrows.Count > 0)
            _arrows.Pop();
    }
}
