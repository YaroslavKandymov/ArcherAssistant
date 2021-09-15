using System.Collections.Generic;

public interface ISceneReloader
{
    void Restart(ArrowSpawner[] spawners, IEnumerable<ArcherAssistant> assistants, IEnumerable<Archer> archers, EnemyAssistantArrowCollector collector, IEnumerable<Quiver> quivers);
}
