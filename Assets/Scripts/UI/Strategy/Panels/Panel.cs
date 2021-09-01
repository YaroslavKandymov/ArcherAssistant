using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    protected ISceneLoader SceneLoader;
    protected IGameCloser GameCloser;
    protected IPanelCloser PanelCloser;
    protected IPanelOpener PanelOpener;

    protected abstract void InitBehaviors();
}
