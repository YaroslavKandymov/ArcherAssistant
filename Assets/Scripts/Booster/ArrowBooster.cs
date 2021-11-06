using System;
using System.Collections;
using UnityEngine;

public class ArrowBooster : ObjectPool<Arrow>
{
    [SerializeField] private int _coefficient;
    [SerializeField] private Arrow _arrowTemplate;

    public int Coefficient => _coefficient;

    public event Action<ArrowBooster> Taken;
    public event Action<int> ArrowCountIncreased;

    private void Awake()
    {
        Initialize(_arrowTemplate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Quiver quiver))
        {
            var newArrowsCount = quiver.ArrowsCount * _coefficient;
            var count = newArrowsCount - newArrowsCount / _coefficient;

            for (int i = 0; i < count; i++)
            {
                if (TryGetObject(out Arrow arrow))
                {
                    arrow.Transform.position = transform.position;
                    arrow.gameObject.SetActive(true);

                    quiver.Add(arrow);
                }
            }

            ArrowCountIncreased?.Invoke(count);
            Taken?.Invoke(this);
        }
    }
}