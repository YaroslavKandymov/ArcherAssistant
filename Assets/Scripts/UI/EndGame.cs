using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private ArcherAssistantHealth[] _archerAssistants;

    private void OnEnable()
    {
        foreach (var archerAssistant in _archerAssistants)
            archerAssistant.Died += OnDied;
    }

    private void OnDisable()
    {
        foreach (var archerAssistant in _archerAssistants)
            archerAssistant.Died -= OnDied;
    }

    private void OnDied(ArcherAssistantHealth archerAssistant)
    {
        if (archerAssistant.GetComponent<PlayerArcherAssistant>())
        {
            Debug.Log("Defeat");
        }
        else
        {
            Debug.Log("Victory");
        }
    }
}
