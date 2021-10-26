using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowView : ObjectPool<PlayerArrow>
{
    [SerializeField] private Quiver _quiver;
    [SerializeField] private PlayerArrow _arrowTemplate;
    [SerializeField] private Transform _arrowsPlace;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _seconds;
    [SerializeField] private float _arrowVerticalOffset;

    private readonly List<PlayerArrow> _arrows = new List<PlayerArrow>();
    private WaitForSeconds _secondsBeforeDisableParticleSystem;
    private Coroutine _coroutine;
    private PlayerArrow _oldArrow;

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
        _particleSystem.gameObject.SetActive(false);
        _secondsBeforeDisableParticleSystem = new WaitForSeconds(_seconds);
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
                    if (_oldArrow == null)
                    {
                        arrow.transform.position = _arrowsPlace.position;
                    }
                    else
                    {
                        Vector3 placementPosition = new Vector3(_oldArrow.transform.position.x,
                            _oldArrow.transform.position.y + _arrowVerticalOffset, _oldArrow.transform.position.z);

                        arrow.transform.position = placementPosition;
                    }

                    arrow.transform.localEulerAngles = new Vector3(0, 90, 90);
                    
                    arrow.gameObject.SetActive(true);
                    _arrows.Add(arrow);

                    _oldArrow = arrow;
                }
            }
        }

        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(PlayRaisingArrowEffect());
    }

    private void OnTaken()
    {
        foreach (var arrow in _arrows)
            arrow.gameObject.SetActive(false);

        _arrows.Clear();
        _oldArrow = null;
    }

    private IEnumerator PlayRaisingArrowEffect()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();

        yield return _secondsBeforeDisableParticleSystem;

        _particleSystem.gameObject.SetActive(false);
    }
}
