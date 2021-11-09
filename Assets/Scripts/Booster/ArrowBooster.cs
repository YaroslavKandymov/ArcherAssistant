using System;
using UnityEngine;

public class ArrowBooster : ObjectPool<Arrow>
{
    [SerializeField] private int _coefficient;
    [SerializeField] private Arrow _arrowTemplate;

    private int _spawnArrowsCount;

    public event Action<ArrowBooster> Taken;
    public event Action MaxArrowsCountReached;

    private void Awake()
    {
        Initialize(_arrowTemplate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArcherAssistant player))
        {
            var quiver = player.GetComponent<Quiver>();
            var newArrowsCount = quiver.ArrowsCount * _coefficient;

            if (newArrowsCount > player.MaxArrowsCount)
            {
                MaxArrowsCountReached?.Invoke();
                _spawnArrowsCount = player.MaxArrowsCount - quiver.ArrowsCount;
            }
            else
            {
                _spawnArrowsCount = newArrowsCount - newArrowsCount / _coefficient;
            }

            for (int i = 0; i < _spawnArrowsCount; i++)
            {
                if (TryGetObject(out Arrow arrow))
                {
                    arrow.ActivateCollider(false);
                    arrow.Transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                    arrow.gameObject.SetActive(true);

                    player.TakeArrow(arrow);
                }
            }

            Taken?.Invoke(this);
        }
    }
}