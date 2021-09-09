using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : Panel
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private ArrowSpawner _spawner;
    [SerializeField] private PlayerArcherAssistant _playerArcherAssistant;
    [SerializeField] private EnemyArcherAssistant _enemyArcherAssistant;
    [SerializeField] private EnemyArrowCollector _collector;

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
        Reloader = new RestartSceneBehavior();
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;

        PanelCloser.Close(this);
        PanelOpener.Open(_gamePanel);

        ArcherAssistant[] assistants = { _playerArcherAssistant, _enemyArcherAssistant};
        Reloader.Restart(_spawner, assistants, _collector);
    }

    private void OnExitButtonClick()
    {
        GameCloser.Close();
    }
}
