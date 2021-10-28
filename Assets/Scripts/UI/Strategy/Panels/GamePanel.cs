using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GamePanel : Panel
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Panel _pausePanel;

    private void Awake()
    {
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
        PanelCloser = new ClosePanelBehavior();
    }

    private void OnPauseButtonClick()
    {
        PanelCloser.Close(this);

        PanelOpener.Open(_pausePanel, true);
    }
}
