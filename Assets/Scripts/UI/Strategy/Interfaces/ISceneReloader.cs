using System.Collections.Generic;

public interface ISceneReloader
{
    void Restart(ArrowSpawner spawner, IEnumerable<ArcherAssistant> assistants, IEnumerable<Archer> archers, EnemyAssistantArrowCollector collector, IEnumerable<Quiver> quivers);
}
