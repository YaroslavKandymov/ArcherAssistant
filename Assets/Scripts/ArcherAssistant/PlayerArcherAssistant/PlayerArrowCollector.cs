using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(ArcherAssistant))]
[RequireComponent(typeof(PlayerArrowsGiver))]
public class PlayerArrowCollector : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Transform _arrowsPlace;
    [SerializeField] private float _arrowVerticalOffset;
    [SerializeField] private float _arrowHorizontalOffset;
    [SerializeField] private Vector3 _targetScale;
    [SerializeField] private ParticleSystem _takeArrowParticleSystem;
    [SerializeField] private float _secondsToPlayParticle;
    [SerializeField] private LosePanel _panel;

    private ArcherAssistant _archerAssistant;
    private readonly Stack<Arrow> _arrows = new Stack<Arrow>();
    private WaitForSeconds _playParticleTime;
    private Coroutine _coroutine;
    private PlayerArrowsGiver _playerArrowsGiver;

    public event Action<Vector3> ArrowDeparted;

    private void Awake()
    {
        _archerAssistant = GetComponent<ArcherAssistant>();
        _takeArrowParticleSystem.gameObject.SetActive(false);
        _playParticleTime = new WaitForSeconds(_secondsToPlayParticle);
        _playerArrowsGiver = GetComponent<PlayerArrowsGiver>();
    }

    private void OnEnable()
    {
        _archerAssistant.ArrowTaken += OnArrowTaken;
        _archerAssistant.MaxArrowsCountReached += OnMaxArrowsCountReached;
        _playerArrowsGiver.ArrowGone += OnArrowGiven;
        _panel.LevelRestarted += OnLevelRestarted;
    }

    private void OnDisable()
    {
        _archerAssistant.ArrowTaken -= OnArrowTaken;
        _archerAssistant.MaxArrowsCountReached -= OnMaxArrowsCountReached;
        _playerArrowsGiver.ArrowGone -= OnArrowGiven;
        _panel.LevelRestarted -= OnLevelRestarted;
    }

    private void Update()
    {
        foreach (var arrow in _arrows)
        {
            arrow.Transform.DOLocalRotate(new Vector3(0, 90, 90), _duration);
        }
    }

    private void OnArrowTaken(Arrow arrow)
    {
        _playerArrowsGiver.AddArrow(arrow);
        _arrows.Push(arrow);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(PlayParticle(_takeArrowParticleSystem));

        arrow.Transform.parent = _arrowsPlace.transform;

        if (_arrows.Count <= 0)
        {
            arrow.transform.DOLocalMove(Vector3.zero, _duration);
        }
        else
        {
            Vector3 placementPosition = new Vector3(0,
                _arrows.Count * _arrowVerticalOffset, _arrows.Count * _arrowHorizontalOffset);

            arrow.Transform.DOLocalMove(placementPosition, _duration);
        }

        arrow.Transform.DOLocalRotate(new Vector3(0, 90, 90), _duration);
        arrow.Transform.DOScale(_targetScale, _duration);
    }

    private void OnArrowGiven(Arrow arrow)
    {
        _arrows.Pop();
    }

    private void OnMaxArrowsCountReached(Arrow arrow)
    {
        arrow.Transform.DOLocalMoveY(5, _duration).OnComplete(() => arrow.Transform.DOLocalMove(Vector3.zero, _duration));
        arrow.transform.DORotate(new Vector3(arrow.Transform.rotation.y + 360, arrow.Transform.rotation.y, arrow.Transform.rotation.z), _duration).SetRelative();
    }

    private IEnumerator PlayParticle(ParticleSystem particleSystem)
    {
        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();

        yield return _playParticleTime;

        particleSystem.Stop();
        particleSystem.gameObject.SetActive(false);
    }

    private void OnLevelRestarted()
    {
        while (_arrows.Count > 0)
        {
            var arrow = _arrows.Pop();
            Destroy(arrow);
        }
    }
}