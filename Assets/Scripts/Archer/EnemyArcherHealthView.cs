using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Slider))]
public class EnemyArcherHealthView : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private float _duration;
    [SerializeField] private float _zAngle;
    [SerializeField] private LosePanel _losePanel;
    [SerializeField] private EnemyArcherHealth[] _healths;
    [SerializeField] private Image[] _images;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _deadImage;
    [SerializeField] private Ease _ease;

    private Slider _slider;
    private Vector3 _rotateAngle;
    private Vector3 _defaultAngle;

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
        _rotateAngle = new Vector3(0, 0, _zAngle);
        _defaultAngle = Vector3.zero;
    }

    private void OnArcherHealthChanged()
    {
        _slider.DOValue(_slider.value - 1, _duration).SetEase(Ease.Linear);
        DOTween.Sequence()
            .Append(transform.DORotate(_rotateAngle, _duration / 4).SetEase(_ease))
            .Append(transform.DORotate(-_rotateAngle, _duration / 4).SetEase(_ease))
            .Append(transform.DORotate(_defaultAngle, _duration / 4).SetEase(_ease));
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
