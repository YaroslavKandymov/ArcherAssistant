using UnityEngine;

public class ArcherZone : MonoBehaviour
{
    [SerializeField] private Archer _archer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerArcherAssistant player))
        {
            player.GiveArrows(_archer);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerArcherAssistant player))
        {
            player.StopGiveArrows();
        }
    }
}
