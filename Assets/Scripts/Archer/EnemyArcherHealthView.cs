using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Slider))]
public class EnemyArcherHealthView : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private float _duration;

    private Slider _slider;

    private void OnEnable()
    {
        _health.ArcherHit += OnArcherHit;
    }

    private void OnDisable()
    {
        _health.ArcherHit += OnArcherHit;
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _health.AllEnemiesMaxHealth;
        _slider.value = _slider.maxValue;
    }

    private void OnArcherHit()
    {
        _slider.DOValue(_slider.value - 1, _duration).SetEase(Ease.Linear);
    }
}
