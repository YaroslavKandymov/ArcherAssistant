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

    private ArcherAssistant _archerAssistant;
    private readonly Stack<Arrow> _arrows = new Stack<Arrow>();

    private void Awake()
    {
        _archerAssistant = GetComponent<ArcherAssistant>();
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
        if(_arrows.Contains(arrow))
            return;

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

            arrow.transform.DOMove(target.position, _duration / 2).OnComplete(() => arrow.gameObject.SetActive(false));
            arrow.Transform.parent = target;
        }
    }

    private IEnumerator TurnOffArrow(Arrow arrow)
    {
        yield return new WaitForSeconds(_duration);

        arrow.gameObject.SetActive(false);
    }
}
