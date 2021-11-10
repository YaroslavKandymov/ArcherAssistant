using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerArrowsGiver : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _giveDelay;
    [SerializeField] private Transform _target;
    [SerializeField] private LosePanel _losePanel;

    private Coroutine _giveArrowsCoroutine;
    private ArcherAssistant _archerAssistant;
    private readonly Stack<Arrow> _arrows = new Stack<Arrow>();

    public event Action AllArrowsGiven;
    public event Action<Arrow> ArrowGiven;
    public event Action<Arrow> ArrowGone;

    private void Awake()
    {
        _archerAssistant = GetComponent<ArcherAssistant>();
    }

    private void OnEnable()
    {
        _archerAssistant.ArrowsTransferStarted += OnArrowsTransferStarted;
        _archerAssistant.ArrowsTransferStopped += OnArrowsTransferStopped;
        _losePanel.LevelRestarted += OnLevelRestarted;
    }

    private void OnDisable()
    {
        _archerAssistant.ArrowsTransferStarted -= OnArrowsTransferStarted;
        _archerAssistant.ArrowsTransferStopped -= OnArrowsTransferStopped;
        _losePanel.LevelRestarted -= OnLevelRestarted;
    }

    public void AddArrow(Arrow arrow)
    {
        _arrows.Push(arrow);
    }

    private void OnArrowsTransferStarted()
    {
        _giveArrowsCoroutine = StartCoroutine(GiveArrow(_target));
    }

    private void OnArrowsTransferStopped()
    {
        if(_giveArrowsCoroutine == null)
            return;

        StopCoroutine(_giveArrowsCoroutine);
    }

    private IEnumerator GiveArrow(Transform target)
    {
        while (_arrows.Count > 0)
        {
            var arrow = _arrows.Pop();
            ArrowGone?.Invoke(arrow);

            arrow.Transform.parent = null;
            arrow.transform.DOMove(target.position, _duration).OnComplete(() =>
            {
                arrow.gameObject.SetActive(false);
                ArrowGiven?.Invoke(arrow);
            });

            yield return new WaitForSeconds(_giveDelay);
        }

        AllArrowsGiven?.Invoke();
    }

    private void OnLevelRestarted()
    {
        while (_arrows.Count > 0)
        {
            _arrows.Pop();
        }
    }
}
