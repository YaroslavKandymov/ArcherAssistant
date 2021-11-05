using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Quiver))]
public abstract class ArcherAssistant : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Archer _archer;

    private Quiver _quiver;
    private PlayerArrowCollector _playerArrowCollector;

    public event Action<Arrow> ArrowTaken;
    public event Action ArrowsTransferStarted;

    private void Awake()
    {
        _quiver = GetComponent<Quiver>();
        _playerArrowCollector = GetComponent<PlayerArrowCollector>();

        transform.position = _startPosition;
        transform.localEulerAngles = _startRotation;
    }

    private void OnEnable()
    {
        _playerArrowCollector.ArrowGiven += OnArrowGiven;
        _playerArrowCollector.AllArrowsGiven += OnAllArrowsGiven;
    }

    private void OnDisable()
    {
        _playerArrowCollector.ArrowGiven -= OnArrowGiven;
        _playerArrowCollector.AllArrowsGiven -= OnAllArrowsGiven;
    }

    public void GiveAllArrows(Archer target)
    {
        if(target == null)
            throw new NullReferenceException(target.name);

        if(_quiver.ArrowsCount <= 0)
            return;

        ArrowsTransferStarted?.Invoke();
    }

    public void TakeArrow(Arrow arrow)
    {
        if (arrow == null)
            return;

        ArrowTaken?.Invoke(arrow);
        _quiver.Add(arrow);
    }

    public void Restart()
    {
        transform.position = _startPosition;
        transform.localEulerAngles = _startRotation;
    }

    private void OnArrowGiven(Arrow arrow)
    {
        _archer.TakeArrow(arrow);
    }

    private void OnAllArrowsGiven()
    {
        _quiver.Clear();
    }
}
