using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PausePanel : Panel
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Panel _gamePanel;

    private void Awake()
    {
        InitBehaviors();

        PanelCloser.Close(this, true);
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
        PanelCloser = new ClosePanelBehavior();
        GameCloser = new CloseApplicationBehavior();
    }

    private void OnContinueButtonClick()
    {
        PanelCloser.Close(this, true);

        PanelOpener.Open(_gamePanel);
    }

    private void OnExitButtonClick()
    {
        GameCloser.Close();
    }
}
