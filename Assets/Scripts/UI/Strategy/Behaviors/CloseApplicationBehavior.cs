using UnityEngine;

public class CloseApplicationBehavior : IGameCloser
{
    public void Close()
    {
        Application.Quit();
    }
}
