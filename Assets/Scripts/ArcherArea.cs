using UnityEngine;

public class ArcherArea : MonoBehaviour
{
    [SerializeField] private Archer _centerPoint;
    [SerializeField] private float _areaRadius;

    public bool EnterInsideArea(Vector3 targetPosition)
    {
        if (Vector3.Distance(_centerPoint.transform.position, targetPosition) <= _areaRadius)
            return true;

        return false;
    }
}
