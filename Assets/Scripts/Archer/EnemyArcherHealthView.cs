using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Slider))]
public class EnemyArcherHealthView : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private float _duration;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private EnemyArcherHealth[] _healths;
    [SerializeField] private Image[] _images;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _deadImage;

    private Slider _slider;

    private void OnEnable()
    {
        _health.ArcherHealthChanged += OnArcherHealthChanged;
        _losePanel.LevelRestarted += OnLevelRestarted;

        foreach (var enemyArcherHealth in _healths)
        {
            enemyArcherHealth.Died += OnDied;
        }
    }

    private void OnDisable()
    {
        _health.ArcherHealthChanged += OnArcherHealthChanged;
        _losePanel.LevelRestarted -= OnLevelRestarted;

        foreach (var enemyArcherHealth in _healths)
        {
            enemyArcherHealth.Died -= OnDied;
        }
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

    private void OnLevelRestarted()
    {
        _slider.DOValue(_slider.maxValue, _duration).SetEase(Ease.Linear);

        foreach (var image in _images)
        {
            image.sprite = _defaultSprite;
        }
    }

    private void OnDied(EnemyArcherHealth health)
    {
        var image = _images.FirstOrDefault(i => i.sprite != _deadImage);
        image.sprite = _deadImage;
    }
}
