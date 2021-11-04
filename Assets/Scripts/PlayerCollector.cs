using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Transform _arrowsPlace;
    [SerializeField] private float _arrowVerticalOffset;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private float _giveDelay;
    [SerializeField] private Transform _target;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _secondsToPlayParticle;

    private ArcherAssistant _archerAssistant;
    private readonly Stack<Arrow> _arrows = new Stack<Arrow>();
    private WaitForSeconds _playParticleTime;
    private Coroutine _coroutine;

    public event Action ArrowsGiven;

    private void Awake()
    {
        _archerAssistant = GetComponent<ArcherAssistant>();
        _particleSystem.gameObject.SetActive(false);
        _playParticleTime = new WaitForSeconds(_secondsToPlayParticle);
    }

    private void OnEnable()
    {
        _archerAssistant.ArrowTaken += OnArrowTaken;
        _archerAssistant.ArrowGiven += OnArrowGiven;
    }

    private void OnDisable()
    {
        _archerAssistant.ArrowTaken -= OnArrowTaken;
        _archerAssistant.ArrowGiven -= OnArrowGiven;
    }

    private void Update()
    {
        foreach (var arrow in _arrows)
        {
            arrow.transform.DOLocalRotate(new Vector3(0, 90, 90), _duration);
        }
    }

    private void OnArrowTaken(Arrow arrow)
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(PlayParticle());

        arrow.transform.parent = _arrowsPlace.transform;

        if (_arrows.Count <= 0)
        {
            arrow.transform.DOLocalMove(Vector3.zero, _duration);
        }
        else
        {
            Vector3 placementPosition = new Vector3(0,
                _arrows.Count * _arrowVerticalOffset, 0);

            arrow.transform.DOLocalMove(placementPosition, _duration);
        }

        arrow.transform.DOLocalRotate(new Vector3(0, 90, 90), _duration);
        arrow.transform.DOScale(_targetScale, _duration);
   
        _arrows.Push(arrow);
    }

    private void OnArrowGiven()
    {
        StartCoroutine(GiveArrow(_target));
    }

    private IEnumerator GiveArrow(Transform target)
    {
        while (_arrows.Count > 0)
        {
            var arrow = _arrows.Pop();

            yield return new WaitForSeconds(_giveDelay);

            arrow.Transform.parent = null;
            arrow.transform.DOMove(target.position, _duration).OnComplete(() => arrow.gameObject.SetActive(false));
        }

        ArrowsGiven?.Invoke();
    }

    private IEnumerator PlayParticle()
    {
        _particleSystem.gameObject.SetActive(true);
        _particleSystem.Play();

        yield return _playParticleTime;

        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);
    }
}
