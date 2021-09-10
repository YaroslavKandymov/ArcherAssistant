public interface ISceneReloader
{
    void Restart(ArrowSpawner spawner, ArcherAssistant[] assistants, Archer[] archers, EnemyArrowCollector collector);
}
