using System.Collections.Generic;
using UnityEngine;

public class RestartSceneBehavior : ISceneReloader
{
    public void Restart(IEnumerable<ArcherAssistant> assistants, IEnumerable<Archer> archers,
        IEnumerable<Quiver> quivers)
    {
        foreach (var assistant in assistants)
        {
            assistant.Restart();
        }

        foreach (var archer in archers)
        {
            archer.ResetTransform();
        }

        foreach (var quiver in quivers)
        {
            quiver.Restart();
        }
    }
}
