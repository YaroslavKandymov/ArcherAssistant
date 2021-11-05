using UnityEngine;

public class ArcherZone : MonoBehaviour
{
    [SerializeField] private Archer _archer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerArcherAssistant player))
        {
            player.GiveAllArrows(_archer);
        }
    }
}
