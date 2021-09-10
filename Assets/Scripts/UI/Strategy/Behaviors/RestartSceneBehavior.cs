using UnityEngine;

public class RestartSceneBehavior : ISceneReloader
{
    public void Restart(ArrowSpawner spawner, ArcherAssistant[] assistants, Archer[] archers, EnemyArrowCollector collector)
    {
        collector.Restart();

        var arrows = GameObject.FindObjectsOfType<Arrow>();

        foreach (var arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
        }

        spawner.Restart();

        foreach (var assistant in assistants)
        {
            assistant.RestartPosition();
        }

        foreach (var archer in archers)
        {
            archer.ResetRotation();
        }
    }
}
