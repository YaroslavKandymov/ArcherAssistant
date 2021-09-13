using System.Collections.Generic;

public interface ISceneReloader
{
    void Restart(ArrowSpawner spawner, IEnumerable<ArcherAssistant> assistants, IEnumerable<Archer> archers, EnemyArrowCollector collector, IEnumerable<Quiver> quivers);
}
