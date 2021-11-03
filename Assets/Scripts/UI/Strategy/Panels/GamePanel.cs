using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GamePanel : Panel
{
    private void Awake()
    {
        InitBehaviors();
        PanelCloser.Close(this);
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new ClosePanelBehavior();
    }
}
