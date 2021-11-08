using System;
using UnityEngine;

[RequireComponent(typeof(Quiver))]
[RequireComponent(typeof(PlayerArrowsGiver))]
public abstract class ArcherAssistant : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Archer _archer;
    [SerializeField] private int _maxArrowsCount;

    private Quiver _quiver;
    private PlayerArrowsGiver _playerArrowsGiver;

    public int MaxArrowsCount => _maxArrowsCount;

    public event Action<Arrow> ArrowTaken;
    public event Action ArrowsTransferStarted;
    public event Action ArrowsTransferStopped;
    public event Action<Arrow> MaxArrowsCountReached;

    private void Awake()
    {
        _quiver = GetComponent<Quiver>();
        _playerArrowsGiver = GetComponent<PlayerArrowsGiver>();

        transform.position = _startPosition;
        transform.localEulerAngles = _startRotation;
    }

    private void OnEnable()
    {
        _playerArrowsGiver.ArrowGiven += OnArrowGiven;
    }

    private void OnDisable()
    {
        _playerArrowsGiver.ArrowGiven -= OnArrowGiven;
    }

    public void GiveArrows(Archer target)
    {
        if(target == null)
            throw new NullReferenceException(target.name);

        if(_quiver.ArrowsCount <= 0)
            return;

        ArrowsTransferStarted?.Invoke();
    }

    public void StopGiveArrows()
    {
        ArrowsTransferStopped?.Invoke();
    }

    public void TakeArrow(Arrow arrow)
    {
        if (arrow == null)
            return;

        if (_quiver.ArrowsCount + 1 >= _maxArrowsCount)
        {
            MaxArrowsCountReached?.Invoke(arrow);
            return;
        }

        arrow.ActivateCollider(false);
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
        _quiver.TryGetArrow();
        _archer.TakeArrow(arrow);
    }
}
