using UnityEngine;

public class ArcherTargetShooter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private ArcherAssistant _target;

    private void Update()
    {
        Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }
}
