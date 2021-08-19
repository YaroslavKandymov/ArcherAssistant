using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _collectRange;

    private ArrowMover _arrowMover;

    public ArrowStates ArrowState;

    private void Awake()
    {
        _arrowMover = GetComponent<ArrowMover>();
    }

    public void Shoot(Transform[] targets)
    {
        _arrowMover.Shoot(targets);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArcherAssistant archerAssistant))
        {
            if (ArrowState == ArrowStates.NotKiller)
            {
                archerAssistant.TakeArrow(this);
                gameObject.SetActive(false);
            }
        }
    }
}
