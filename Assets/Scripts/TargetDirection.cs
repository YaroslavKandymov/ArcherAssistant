using UnityEngine;

public class TargetDirection : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        var direction = (_target.transform.position - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(direction);
    }
}
