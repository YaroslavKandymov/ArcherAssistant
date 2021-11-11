using UnityEngine.SceneManagement;

public class LoadSceneBehavior : ISceneLoader
{
    public void Load(string sceneName, Level level)
    {
        SceneManager.LoadScene(sceneName);
    }
}
