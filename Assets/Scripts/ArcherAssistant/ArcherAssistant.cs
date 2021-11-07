using System;
using UnityEngine;

[RequireComponent(typeof(Quiver))]
[RequireComponent(typeof(PlayerArrowsGiver))]
public abstract class ArcherAssistant : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _startRotation;
    [SerializeField] private Archer _archer;

    private Quiver _quiver;
    private PlayerArrowsGiver _playerArrowsGiver;

    public event Action<Arrow> ArrowTaken;
    public event Action ArrowsTransferStarted;
    public event Action ArrowsTransferStopped;

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
        _playerArrowsGiver.AllArrowsGiven += OnAllArrowsGiven;
    }

    private void OnDisable()
    {
        _playerArrowsGiver.ArrowGiven -= OnArrowGiven;
        _playerArrowsGiver.AllArrowsGiven -= OnAllArrowsGiven;
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
