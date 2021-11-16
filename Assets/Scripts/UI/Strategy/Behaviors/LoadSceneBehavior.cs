using UnityEngine.SceneManagement;

public class LoadSceneBehavior : ISceneLoader
{
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
