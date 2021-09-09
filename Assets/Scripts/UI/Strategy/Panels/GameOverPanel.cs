using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : Panel
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Panel _gamePanel;

    private void Awake()
    {
        InitBehaviors();
        PanelCloser.Close(this);
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected override void InitBehaviors()
    {
        GameCloser = new CloseApplicationBehavior();
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new CloseBehavior();
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;

        PanelCloser.Close(this);
        PanelOpener.Open(_gamePanel);


    }

    private void OnExitButtonClick()
    {
        GameCloser.Close();
    }
}
