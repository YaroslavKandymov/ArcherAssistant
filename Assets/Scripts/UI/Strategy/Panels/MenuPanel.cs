using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : Panel
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;

    private const string GameSceneName = "Game";

    private void Awake()
    {
        InitBehaviors();
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    protected override void InitBehaviors()
    {
        SceneLoader = new LoadSceneBehavior();
        GameCloser = new CloseApplicationBehavior();
    }

    private void OnPlayButtonClick()
    {
        SceneLoader.Load(GameSceneName);
    }

    private void OnExitButtonClick()
    {
        GameCloser.Close();
    }
}
