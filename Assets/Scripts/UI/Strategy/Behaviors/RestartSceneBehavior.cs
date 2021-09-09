public class RestartSceneBehavior : ISceneReloader
{
    public void Restart(ArrowSpawner spawner)
    {
        spawner.Restart();
    }
}
