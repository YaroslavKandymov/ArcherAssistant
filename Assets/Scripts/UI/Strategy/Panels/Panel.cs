using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    protected ISceneLoader SceneLoader;
    protected IGameCloser GameCloser;
    protected ICloserPanel CloserPanel;
    protected IPanelOpener PanelOpener;

    protected abstract void InitBehaviors();
}
