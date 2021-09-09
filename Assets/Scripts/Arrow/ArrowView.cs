using System.Collections.Generic;
using UnityEngine;

public class ArrowView : ObjectPool<PlayerArrow>
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private PlayerArrow _arrowTemplate;
    [SerializeField] private Transform _arrowsPlace;

    private readonly List<PlayerArrow> _arrows = new List<PlayerArrow>();

    private void OnEnable()
    {
        _quiver.ArrowsCountChanged += OnArrowsCountChanged;
        _quiver.Taken += OnTaken;
    }

    private void OnDisable()
    {
        _quiver.ArrowsCountChanged -= OnArrowsCountChanged;
        _quiver.Taken -= OnTaken;
    }

    private void Start()
    {
        Initialize(_arrowTemplate);
    }

    private void OnArrowsCountChanged(int count)
    {
        if (_arrows.Count < count)
        {
            var addCount = count - _arrows.Count;

            for (int i = 0; i < addCount; i++)
            {
                if (TryGetObject(out PlayerArrow arrow))
                {
                    arrow.transform.position = _arrowsPlace.position;
                    arrow.transform.localEulerAngles =
                        new Vector3(Random.Range(-60, -120), Random.Range(70, 300), Random.Range(-80, 80));
                    
                    arrow.gameObject.SetActive(true);
                    _arrows.Add(arrow);
                }
            }
        }
    }

    private void OnTaken()
    {
        foreach (var arrow in _arrows)
        {
            arrow.gameObject.SetActive(false);
        }

        _arrows.Clear();
    }
}
