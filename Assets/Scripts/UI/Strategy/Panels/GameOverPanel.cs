using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : Panel
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Panel _gamePanel;
    [SerializeField] private ArrowSpawner[] _spawners;
    [SerializeField] private ArcherAssistant[] _assistants;
    [SerializeField] private EnemyAssistantArrowCollector _collector;
    [SerializeField] private Archer[] _archers;
    [SerializeField] private Quiver[] _quivers;

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

        Reloader.Restart(_spawners, _assistants, _archers, _collector, _quivers);
    }

    private void OnExitButtonClick()
    {
        GameCloser.Close();
    }
}
