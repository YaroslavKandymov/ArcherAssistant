using System.Collections.Generic;
using UnityEngine;

public class RestartSceneBehavior : ISceneReloader
{
    public void Restart(ArrowSpawner[] spawners, IEnumerable<ArcherAssistant> assistants, IEnumerable<Archer> archers, EnemyAssistantArrowCollector collector,
        IEnumerable<Quiver> quivers)
    {
        collector.Restart();

        var arrows = GameObject.FindObjectsOfType<Arrow>();

        foreach (var arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
        }

        foreach (var spawner in spawners)
        {
            spawner.Restart();
        }

        foreach (var assistant in assistants)
        {
            assistant.RestartPosition();
        }

        foreach (var archer in archers)
        {
            archer.ResetRotation();
        }

        foreach (var quiver in quivers)
        {
            quiver.Restart();
        }
    }
}
