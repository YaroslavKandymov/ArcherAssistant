using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : Panel
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _exitGameButton;
    [SerializeField] private Panel _startPanel;
    [SerializeField] private string _sceneName;
    [SerializeField] private float _duration;

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
        _nextLevelButton.transform.DOScale(_nextLevelButton.transform.localScale * 0.8f, _duration);

        PanelCloser.Close(this, false, _duration);

        StartCoroutine(LoadNextLevel());
    }

    private void OnExitGameButtonClick()
    {
        _exitGameButton.transform.DOScale(_exitGameButton.transform.localScale * 0.8f, _duration);
        GameCloser.Close();
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(_duration);

        SceneLoader.Load(_sceneName);
        PanelOpener.Open(_startPanel, true);
    }
}
