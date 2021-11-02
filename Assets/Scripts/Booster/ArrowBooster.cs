using System;
using UnityEngine;

public class ArrowBooster : MonoBehaviour
{
    [SerializeField] private int _coefficient;
    [SerializeField] private Arrow _arrowTemplate;

    public event Action<ArrowBooster> Taken;
    public event Action<int> ArrowCountIncreased;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Quiver quiver))
        {
            var newArrowsCount = quiver.ArrowsCount * _coefficient;
            var count = newArrowsCount - newArrowsCount / _coefficient;

            for (int i = 0; i < count; i++)
            {
                var newArrow = Instantiate(_arrowTemplate, transform.position, Quaternion.identity);
                newArrow.gameObject.SetActive(false);

                quiver.Add(newArrow);
            }

            ArrowCountIncreased?.Invoke(count);
            Taken?.Invoke(this);
        }
    }
}