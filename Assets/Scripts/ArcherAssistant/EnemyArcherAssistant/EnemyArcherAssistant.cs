using UnityEngine;

public class EnemyArcherAssistant : ArcherAssistant
{
    [SerializeField] private Ground _ground;
    [SerializeField] private ArrowSpawner _spawner;
    [SerializeField] private EnemyAssistantArrowCollector _assistantArrowCollector;

    private void OnEnable()
    {
        _ground.ArrowLanded += OnArrowLanded;
        _spawner.ArrowSpawned += OnArrowSpawned;
    }

    private void OnDisable()
    {
        _ground.ArrowLanded -= OnArrowLanded;
        _spawner.ArrowSpawned -= OnArrowSpawned;
    }

    private void OnArrowLanded(Arrow arrow)
    {
        _assistantArrowCollector.Collect(arrow);
    }

    private void OnArrowSpawned(Arrow arrow)
    {
        _assistantArrowCollector.Collect(arrow);
    }
}
