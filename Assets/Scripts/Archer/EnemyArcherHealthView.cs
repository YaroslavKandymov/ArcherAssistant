using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Slider))]
public class EnemyArcherHealthView : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private float _duration;
    [SerializeField] private LosePanel _losePanel;

    private Slider _slider;

    private void OnEnable()
    {
        _health.ArcherHealthChanged += OnArcherHealthChanged;
        _losePanel.SceneRestarted += OnSceneRestarted;
    }

    private void OnDisable()
    {
        _health.ArcherHealthChanged += OnArcherHealthChanged;
        _losePanel.SceneRestarted -= OnSceneRestarted;
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _health.AllEnemiesHealth;
        _slider.value = _slider.maxValue;
    }

    private void OnArcherHealthChanged()
    {
        _slider.DOValue(_slider.value - 1, _duration).SetEase(Ease.Linear);
    }

    private void OnSceneRestarted()
    {
        _slider.DOValue(_slider.maxValue, _duration).SetEase(Ease.Linear);
    }
}
