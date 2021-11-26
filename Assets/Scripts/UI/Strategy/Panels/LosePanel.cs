using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : Panel
{
    [SerializeField] private float _duration;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Panel _gamePanel;

    private Vector3 _restartButtonDefaultScale;

    public event Action LevelRestarted;

    private void Awake()
    {
        InitBehaviors();
        PanelCloser.Close(this);

        _restartButtonDefaultScale = _restartButton.transform.localScale;
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    protected override void InitBehaviors()
    {
        GameCloser = new CloseApplicationBehavior();
        PanelOpener = new OpenPanelBehavior();
        PanelCloser = new ClosePanelBehavior();
        Reloader = new RestartSceneBehavior();
    }

    private void OnRestartButtonClick()
    {
        _restartButton.transform.DOScale(_restartButton.transform.localScale * 0.8f, _duration).SetLoops(1, LoopType.Yoyo);
        PanelCloser.Close(this, true, _duration);

        StartCoroutine(OpenGamePanel());
    }

    private IEnumerator OpenGamePanel()
    {
        yield return new WaitForSeconds(_duration);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        PanelOpener.Open(_gamePanel, false);
        _restartButton.transform.DOScale(_restartButtonDefaultScale, _duration);
    }
}
