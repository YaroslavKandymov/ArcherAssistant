using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PausePanel : Panel
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Panel _gamePanel;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        InitBehaviors();
    }

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(OnContinueButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(OnContinueButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        CloserPanel = new ClosePanelBehavior();
        GameCloser = new CloseApplicationBehavior();
    }

    private void OnContinueButtonClick()
    {
        var buttons = new List<Button> { _continueButton, _exitButton };

        CloserPanel.Close(_canvasGroup, buttons);

        var newCanvasGroup = _gamePanel.GetComponent<CanvasGroup>();
        var newButtons = _gamePanel.GetComponentsInChildren<Button>();

        PanelOpener.Open(newCanvasGroup, newButtons);

        Time.timeScale = 1;
    }

    private void OnExitButtonClick()
    {
        GameCloser.Close();
    }
}
