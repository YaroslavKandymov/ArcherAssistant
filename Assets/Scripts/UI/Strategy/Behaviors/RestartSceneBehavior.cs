using System.Collections.Generic;
using UnityEngine;

public class RestartSceneBehavior : ISceneReloader
{
    public void Restart(IEnumerable<ArcherAssistant> assistants, IEnumerable<Archer> archers,
        IEnumerable<Quiver> quivers)
    {
        var arrows = GameObject.FindObjectsOfType<Arrow>();

        foreach (var arrow in arrows)
        {
            arrow.gameObject.SetActive(false);

            if(arrow.Transform.parent == null)
            {
                arrow.Destroy();
            }
        }

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
