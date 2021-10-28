using TMPro;
using UnityEngine;

public class ArcherTextViewPanel : Panel
{
    [SerializeField] private Archer _archer;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Panel _arrowCountPanel;

    private void Awake()
    {
        InitBehaviors();
        PanelOpener.Open(this);
    }

    private void OnEnable()
    {
        _quiver.ArrowsOver += OnArrowsOver;
        _archer.ArrowsIncreased += OnArrowsIncreased;
    }

    private void OnDisable()
    {
        _quiver.ArrowsOver += OnArrowsOver;
        _archer.ArrowsIncreased -= OnArrowsIncreased;
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new ClosePanelBehavior();
    }

    private void OnArrowsOver()
    {
        PanelCloser.Close(_arrowCountPanel);
        PanelOpener.Open(this);
    }

    private void OnArrowsIncreased()
    {
        PanelCloser.Close(this);
        PanelOpener.Open(_arrowCountPanel);
    }
}
