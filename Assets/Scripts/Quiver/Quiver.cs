using System;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : ObjectPool<ArrowMover>
{
    [SerializeField] private int _startArrowsCount;
    [SerializeField] private ArrowMover _arrowTemplate;

    private readonly Stack<Arrow> _arrows = new Stack<Arrow>();

    public int ArrowsCount => _arrows.Count;

    public event Action ArrowsCountChanged;

    private void Start()
    {
        Initialize(_arrowTemplate);
        Add(_startArrowsCount);
    }

    public void Add(int count)
    {
        for(int i = 0; i < count; i++)
            _arrows.Push(new Arrow());

        ArrowsCountChanged?.Invoke();
    }

    public ArrowMover TryGetArrow(Transform instantiatePoint)
    {
        if (TryGetObject(out _arrowTemplate))
        {
            _arrowTemplate.transform.position = instantiatePoint.position;
            _arrowTemplate.gameObject.SetActive(true);
            ArrowsCountChanged?.Invoke();
            return _arrowTemplate;
        }

        return null;
    }

    public void ClearArrowList()
    {
        if (_arrows.Count <= 0)
            throw new ArgumentOutOfRangeException("Quiver have no arrows");


    }
}
