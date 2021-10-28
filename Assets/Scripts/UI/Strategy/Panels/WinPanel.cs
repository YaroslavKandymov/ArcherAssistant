using UnityEngine;
using UnityEngine.UI;

public class WinPanel : Panel
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Panel _startPanel;
    [SerializeField] private string _sceneName;

    private void Awake()
    {
        InitBehaviors();
        PanelCloser.Close(this);
    }

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
        _exitGameButton.onClick.AddListener(OnExitGameButtonClick);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
        _exitGameButton.onClick.RemoveListener(OnExitGameButtonClick);
    }

    protected override void InitBehaviors()
    {
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new ClosePanelBehavior();
        GameCloser = new CloseApplicationBehavior();
        SceneLoader = new LoadSceneBehavior();
    }

    private void OnNextLevelButtonClick()
    {
        PanelCloser.Close(this);
        SceneLoader.Load(_sceneName);
        PanelOpener.Open(_startPanel, true);
    }

    private void OnExitGameButtonClick()
    {
        GameCloser.Close();
    }
}
