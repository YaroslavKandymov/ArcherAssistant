using UnityEngine;
using DG.Tweening;

public class SizeChanger : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float ratio;

    public void Reduce()
    {
        transform.DOScale(transform.localScale * ratio, duration);
    }
}
