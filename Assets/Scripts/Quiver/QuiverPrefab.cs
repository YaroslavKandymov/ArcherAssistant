using UnityEngine;

public class QuiverPrefab : MonoBehaviour
{
    [SerializeField] private int _arrowCount;

    public int ArrowCount => _arrowCount;
}
