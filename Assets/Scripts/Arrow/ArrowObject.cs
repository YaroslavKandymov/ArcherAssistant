using UnityEngine;

[CreateAssetMenu(fileName = "Arrow", menuName = "New arrow", order = 51)]
public class ArrowObject : ScriptableObject
{
    [SerializeField] private float _collectRange;
    [SerializeField] private GameObject _prefab;

    public float CollectRange => _collectRange;
}
