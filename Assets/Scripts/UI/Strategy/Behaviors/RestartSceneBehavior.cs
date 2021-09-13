using System.Collections.Generic;
using UnityEngine;

public class RestartSceneBehavior : ISceneReloader
{
    public void Restart(ArrowSpawner spawner, IEnumerable<ArcherAssistant> assistants, IEnumerable<Archer> archers, EnemyArrowCollector collector,
        IEnumerable<Quiver> quivers)
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

        foreach (var quiver in quivers)
        {
            quiver.Restart();
        }
    }
}
