using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GamePanel : Panel
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Quiver _quiver;
    [SerializeField] private Panel _pausePanel;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        InitBehaviors();
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        CloserPanel = new ClosePanelBehavior();
    }

    private void OnPauseButtonClick()
    {
        Time.timeScale = 0;

        var buttons = new List<Button> {_pauseButton};

        CloserPanel.Close(_canvasGroup, buttons);

        var newCanvasGroup = _pausePanel.GetComponent<CanvasGroup>();
        var newButtons = _pausePanel.GetComponentsInChildren<Button>();

        PanelOpener.Open(newCanvasGroup, newButtons);
    }
}
